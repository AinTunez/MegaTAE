using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SoulsFormats;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using Microsoft.VisualBasic;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;

namespace MegaTAE
{
    public partial class GUI : Form
    {
        public BND4 ANIBND;
        public string FilePath;
        public bool IsSekiro;
        private List<TAE4.Event> SekiroCopyAll = new List<TAE4.Event>();
        private List<TAE3.Event> DS3CopyAll = new List<TAE3.Event>();
        private AnimQueueItem PrevAnimQueueItem = null;

        public byte[] MaskArray { get; set; } = new byte[32];
        public string MaskArrayTitle { get; set; } = "(null)";

        public PropertyInfo MaskArrayInfo;

        public dynamic TAE
        {
            get
            {
                if (IsSekiro) return (TAE4Handler)TaeListBox.SelectedItem;
                else return (TAE3Handler)TaeListBox.SelectedItem;
            }
        }

        public dynamic Anim
        {
            get
            {
                if (IsSekiro) return (ANIM4Handler)AnimListBox.SelectedItem;
                else return (ANIM3Handler)AnimListBox.SelectedItem;
            }
        }

        public ANIM4Handler ANIM4 => (ANIM4Handler)Anim;
        public ANIM3Handler ANIM3 => (ANIM3Handler)Anim;

        public EVENT4Handler EVENT4 => (EVENT4Handler)Event;
        public EVENT3Handler EVENT3 => (EVENT3Handler)Event;

        public dynamic Event
        {
            get
            {
                if (IsSekiro) return (EVENT4Handler)EventListBox.SelectedItem;
                else return (EVENT3Handler)EventListBox.SelectedItem;
            }
        }

        public dynamic EventField => EventDataGrid.SelectedObject == null ? null : EventDataGrid.SelectedGridItem;

        public dynamic CopiedEvent;

        public GUI()
        {
            InitializeComponent();
            UTIL.Init(this);
        }

        public void ReadAnimation()
        {


            Timer animTimer = new Timer();
            animTimer.Tick += new EventHandler(checkAnim);
            animTimer.Interval = 1;
            animTimer.Start();


            if (PrevAnimQueueItem == null) PrevAnimQueueItem = GetCurrentQueueItem();

            void checkAnim(object sender, EventArgs e)
            {
                ;

                if (Memory.BaseAddress == IntPtr.Zero)
                {
                    Console.WriteLine("No process.");
                }
                else
                {
                    var aNum = GetCurrentAnimationId();
                    if (aNum == null) return;
                    CurrentAnimBox.Text = aNum.ToString();
                }

            }
        }

        private static bool IsValidAddress(Int64 address)
        {
            return (address >= 0x10000 && address < 0x000F000000000000);
        }

        internal dynamic ReadPointerChain(bool isInt, long baseAddress, long baseOffset, int[] chain)
        {
            long lpCurrent = Memory.ReadInt64(new IntPtr(baseAddress + baseOffset));
            for (int i = 0; i < chain.Length - 1; i++)
            {
                if (!IsValidAddress(lpCurrent)) return null;
                lpCurrent = Memory.ReadInt64(new IntPtr(lpCurrent) + chain[i]);
            }
            if (isInt) return Memory.ReadInt32(new IntPtr(lpCurrent) + chain.Last());
            else return Memory.ReadFloat(new IntPtr(lpCurrent) + chain.Last());
        }

        internal float? ReadPointerChainAsFloat(long baseAddress, long baseOffset, int[] chain)
        {
            return ReadPointerChain(false, baseAddress, baseOffset, chain);
        }

        internal int? ReadPointerChainAsInt(long baseAddress, long baseOffset, int[] chain)
        {
            return ReadPointerChain(true, baseAddress, baseOffset, chain);
        }

        private AnimQueueItem GetCurrentQueueItem()
        {
            int animationId = ReadPointerChainAsInt(Memory.BaseAddress.ToInt64(), 0x4768E78, new int[] { 0x80, 0x1F90, 0x10, 0x20 }).Value;
            float time = ReadPointerChainAsFloat(Memory.BaseAddress.ToInt64(), 0x4768E78, new int[] { 0x80, 0x1F90, 0x10, 0x28 }).Value;
            return new AnimQueueItem(animationId, time);
        }

        private int? GetCurrentAnimationId()
        {
            if (Memory.BaseAddress == IntPtr.Zero) return null;
            if (IsSekiro)
                return ReadPointerChainAsInt(Memory.BaseAddress.ToInt64(), 0x3B67DF0, new int[] { 0x88, 0x1FF8, 0x80, 0xC8 });
            else
                return ReadPointerChainAsInt(Memory.BaseAddress.ToInt64(), 0x4768E78, new int[] { 0x80, 0x1F90, 0x10, 0x20 });
        }

        public void LoadANIBND(string path, bool isSekiro)
        {
            TaeListBox.DataSource = null;
            ANIBND = null;
            FilePath = null;
            IsSekiro = isSekiro;

            try
            {
                ANIBND = BND4.Read(path);
                FilePath = Path.GetFullPath(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UTIL.LogException("Error loading ANIBND", ex);
            }

            if (ANIBND == null || FilePath == null) return;

            if (!File.Exists(path + ".bak")) File.Copy(path, path + ".bak");
            var tae3_list = new List<TAE3Handler>();
            var tae4_list = new List<TAE4Handler>();

            foreach (var file in ANIBND.Files.Where(f => f.Name.EndsWith(".tae") && f.Bytes.Length > 0))
            {
                if (isSekiro)
                {
                    var t = new TAE4Handler(file);
                    if (t.IsValid) tae4_list.Add(new TAE4Handler(file));

                }
                else
                {
                    var t = new TAE3Handler(file);
                    if (t.IsValid) tae3_list.Add(new TAE3Handler(file));
                }
            }


            void refreshList()
            {
                if (TaeListBox.InvokeRequired)
                {
                    TaeListBox.Invoke(new MethodInvoker(refreshList));
                }
                else
                {
                    if (isSekiro) TaeListBox.DataSource = tae4_list;
                    else TaeListBox.DataSource = tae3_list;
                    if (TaeListBox.Items.Count > 0) TaeListBox.SelectedIndex = 0;
                }
            }

            refreshList();

        }

        private void TaeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (TaeListBox.SelectedItem == null)
            {
                AnimListBox.DataSource = null;
                return;
            }


            if (IsSekiro)
            {
                var handler = (TAE4Handler)TaeListBox.SelectedItem;
                if (!handler.IsValid)
                {
                    AnimListBox.DataSource = null;
                    AnimDataGrid.SelectedObject = null;
                    EventListBox.DataSource = null;
                    EventDataGrid.SelectedObject = null;
                    return;
                }
                else
                {
                    AnimListBox.DataSource = handler.TAE.Animations.Select(a => new ANIM4Handler(a)).ToList();
                    if (AnimListBox.Items.Count > 0) AnimListBox.SelectedIndex = 0;
                }
            }
            else
            {
                var handler = (TAE3Handler)TaeListBox.SelectedItem;
                if (!handler.IsValid)
                {
                    AnimListBox.DataSource = null;
                    AnimDataGrid.SelectedObject = null;
                    EventListBox.DataSource = null;
                    EventDataGrid.SelectedObject = null;
                    return;
                }
                else
                {
                    AnimListBox.DataSource = handler.TAE.Animations.Select(a => new ANIM3Handler(a)).ToList();
                    if (AnimListBox.Items.Count > 0) AnimListBox.SelectedIndex = 0;
                }
            }

        }

        private void AnimListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventListBox.DataSource = null;
            if (AnimListBox.SelectedItem == null)
            {
                AnimDataGrid.SelectedObject = null;
                return;
            }
            if (IsSekiro)
            {
                AnimDataGrid.SelectedObject = ANIM4;
                EventListBox.DataSource = ANIM4.Events.Select(evt => new EVENT4Handler(evt)).ToList();
            }
            else
            {
                AnimDataGrid.SelectedObject = ANIM3;
                EventListBox.DataSource = ANIM3.Events.Select(evt => new EVENT3Handler(evt)).ToList();
            }

        }

        private void EventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventListBox.SelectedItem == null)
            {
                EventDataGrid.SelectedObject = null;
                return;
            }

            if (IsSekiro) EventDataGrid.SelectedObject = ((EVENT4Handler)EventListBox.SelectedItem).Event;
            else EventDataGrid.SelectedObject = ((EVENT3Handler)EventListBox.SelectedItem).Event;

        }

        private void saveANIBNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ANIBND == null) return;

            try
            {
                TAE.Save();
                ANIBND.Write(FilePath);
                UTIL.WriteLog(Path.GetFileName(FilePath) + " saved.");
                forceInGameReloadToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                UTIL.LogException("Error saving ANIBND", ex);
            }
        }

        private void openANIBNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open ANIBND";
            ofd.Filter = "ANIBND|*.anibnd;*.anibnd.dcx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                GameChooser g = new GameChooser();
                g.StartPosition = FormStartPosition.CenterParent;
                if (g.ShowDialog() == DialogResult.OK)
                {
                    new Task(() => LoadANIBND(ofd.FileName, g.IsSekiro)).Start();
                }

            }
        }

        private void EventDataGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            TAE.Save();
        }

        private void AnimDataGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            TAE.Save();
        }

        private void addNewEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Anim == null) return;
            if (IsSekiro)
            {
                var chooser = new TAE4_TypeChooser();
                if (chooser.ShowDialog() == DialogResult.OK)
                {
                    Anim.Events.Add(chooser.Event);
                    while (Anim.EventGroups.Count - 1 < chooser.GroupIndex)
                    {
                        Anim.EventGroups.Add(new TAE4.EventGroup(chooser.Event.Type));
                    }
                    Anim.EventGroups[chooser.GroupIndex].Indices.Add(Anim.Events.IndexOf(chooser.Event));
                    FixEventGroups();
                    EventListBox.DataSource = ANIM4.Events.Select(evt => new EVENT4Handler(evt)).ToList();
                    EventListBox.SelectedIndex = EventListBox.Items.Count - 1;
                }
            }
            else
            {
                var chooser = new TAE3_TypeChooser();
                if (chooser.ShowDialog() == DialogResult.OK)
                {
                    Anim.Events.Add(chooser.Event);
                    while (Anim.EventGroups.Count - 1 < chooser.GroupIndex)
                    {
                        Anim.EventGroups.Add(new TAE3.EventGroup(chooser.Event.Type));
                    }
                    Anim.EventGroups[chooser.GroupIndex].Indices.Add(Anim.Events.IndexOf(chooser.Event));
                    FixEventGroups();
                    EventListBox.DataSource = ANIM3.Events.Select(evt => new EVENT3Handler(evt)).ToList();
                    EventListBox.SelectedIndex = EventListBox.Items.Count - 1;
                }
            }
        }


        private void FixEventGroups()
        {
            try
            {
                List<int> events = new List<int>();
                if (IsSekiro)
                {
                    foreach (var group in ANIM4.EventGroups)
                    {
                        foreach (var index in group.Indices.ToList())
                        {
                            if (index > ANIM4.Events.Count - 1 || events.Contains(index))
                            {
                                group.Indices.Remove(index);
                            }
                            else
                            {
                                events.Add(index);
                            }
                        }
                    }
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (!events.Contains(i)) ANIM4.EventGroups[0].Indices.Add(i);
                    }
                }
                else
                {
                    foreach (var group in ANIM3.EventGroups)
                    {
                        foreach (var index in group.Indices.ToList())
                        {
                            if (index > ANIM3.Events.Count - 1 || events.Contains(index))
                            {
                                group.Indices.Remove(index);
                            }
                            else
                            {
                                events.Add(index);
                            }
                        }
                    }
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (!events.Contains(i)) ANIM3.EventGroups[0].Indices.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                UTIL.LogException("Error fixing EventGroups", ex);
            }

        }

        private void deleteSelectedEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Anim == null || EventListBox.SelectedItem == null) return;
            int index = EventListBox.SelectedIndex;
            if (IsSekiro)
            {
                Anim.Events.Remove(((EVENT4Handler)EventListBox.SelectedItem).Event);
                FixEventGroups();
                EventListBox.DataSource = ANIM4.Events.Select(evt => new EVENT4Handler(evt)).ToList();
                while (index > EventListBox.Items.Count - 1) index--;
                if (index > -1) EventListBox.SelectedIndex = index;
            }
            else
            {
                Anim.Events.Remove(((EVENT3Handler)EventListBox.SelectedItem).Event);
                FixEventGroups();
                EventListBox.DataSource = ANIM3.Events.Select(evt => new EVENT3Handler(evt)).ToList();
                while (index > EventListBox.Items.Count - 1) index--;
                if (index > -1) EventListBox.SelectedIndex = index;
            }
        }

        private void ClearConsoleBtn_Click(object sender, EventArgs e)
        {
            ConsoleBox.Clear();
        }

        private void addNewToolAnimationStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TAE == null) return;
            var chooser = new AnimIdChooser();
            if (chooser.ShowDialog() == DialogResult.OK)
            {
                long id = Convert.ToInt64(chooser.animIdBox.Value);
                if (IsSekiro)
                {
                    var anim = new TAE4.Animation(id);
                    var animations = (TAE as TAE4Handler).TAE.Animations;
                    animations.Add(anim);
                    animations = animations.OrderBy(a => a.ID).ToList();
                    AnimListBox.DataSource = animations.Select(a => new ANIM4Handler(a)).ToList();
                    AnimListBox.SelectedItem = animations.First(a => a.ID == id);
                }
                else
                {
                    var anim = new TAE3.Animation(id);
                    var animations = (TAE as TAE3Handler).TAE.Animations;
                    animations.Add(anim);
                    animations = animations.OrderBy(a => a.ID).ToList();
                    AnimListBox.DataSource = animations.Select(a => new ANIM3Handler(a)).ToList();
                    AnimListBox.SelectedItem = animations.First(a => a.ID == id);
                }
            }
        }

        //Memory 
        private void forceInGameReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (IsSekiro) Memory.AttachProc("sekiro");
            else Memory.AttachProc("DarkSoulsIII");

            if (Memory.BaseAddress != IntPtr.Zero) ReloadFile();
            else return;
        }

        private void ReloadFile()
        {
            if (IsSekiro) //needs check if in main menu
            {
                if (Memory.ReadInt8(Memory.BaseAddress + 0x3905262) == 0)
                {
                    Memory.WriteInt8(Memory.BaseAddress + 0x3B68FBD, 1);

                    var bytes = new byte[]
                    {
                    0x49, 0xBE, 0x90, 0x91, 0xA3, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140A39190
                    0x48, 0xA1, 0x30, 0x8E, 0xB6, 0x43, 0x01, 0x00, 0x00, 0x00, //mov rax,[143B68E30]
                    0x48, 0x8B, 0xC8, //mov rcx,rax
                    0x48, 0x8D, 0x14, 0x25, 0, 0, 0, 0, //lea rdx,[alloc]
                    0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                    0x41, 0xFF, 0xD6, //call r14
                    0x48, 0x83, 0xC4, 0x28, //add rsp,28
                    0xC3 //ret
                    };

                    var bytes2 = new byte[3];
                    var bytjmp = 0x1B;

                    var buffer = 512;
                    var stringBuffer = 128;
                    var address = Kernel32.VirtualAllocEx(Memory.ProcessHandle, IntPtr.Zero, buffer, 0x1000 | 0x2000, 0X40);
                    var stringAddress = Kernel32.VirtualAllocEx(Memory.ProcessHandle, IntPtr.Zero, stringBuffer, 0x1000 | 0x2000, 0X40);

                    string chrId = Path.GetFileName(FilePath).Substring(0, 5);
                    byte[] CharacterString = Encoding.Unicode.GetBytes(chrId);

                    Memory.WriteBytes(stringAddress, CharacterString);

                    bytes2 = BitConverter.GetBytes((int)stringAddress);
                    Array.Copy(bytes2, 0, bytes, bytjmp, bytes2.Length);

                    if (address != IntPtr.Zero)
                    {
                        if (Memory.WriteBytes(address, bytes))
                        {
                            var threadHandle = Kernel32.CreateRemoteThread(Memory.ProcessHandle, IntPtr.Zero, 0, address, IntPtr.Zero, 0, out var threadId);
                            if (threadHandle != IntPtr.Zero)
                            {
                                Kernel32.WaitForSingleObject(threadHandle, 30000);
                            }
                        }
                        Kernel32.VirtualFreeEx(Memory.ProcessHandle, address, buffer, 2);
                        Kernel32.VirtualFreeEx(Memory.ProcessHandle, stringAddress, stringBuffer, 2);
                    }
                }
            }
            else
            {
                if (Memory.ReadInt8(Memory.BaseAddress + 0x474C2F0) == 0)
                {
                    Memory.WriteInt8(Memory.BaseAddress + 0x4768F7F, 1);

                    var bytes = new byte[] { 0x48, 0xA1, 0x78, 0x8E, 0x76, 0x44, 0x01, 0x00, 0x00, 0x00, 0x48, 0x8B, 0xC8, 0x49, 0xBE, 0x10, 0x1E, 0x8D, 0x40, 0x01, 0x00, 0x00, 0x00, 0x48, 0x8D, 0x14, 0x25, 0, 0, 0, 0, 0x48, 0x83, 0xEC, 0x28, 0x41, 0xFF, 0xD6, 0x48, 0x83, 0xC4, 0x28, 0xC3 };
                    var bytes2 = new byte[3];
                    var bytjmp = 0x1B;

                    var buffer = 512;
                    var stringBuffer = 128;
                    var address = Kernel32.VirtualAllocEx(Memory.ProcessHandle, IntPtr.Zero, buffer, 0x1000 | 0x2000, 0X40);
                    var stringAddress = Kernel32.VirtualAllocEx(Memory.ProcessHandle, IntPtr.Zero, stringBuffer, 0x1000 | 0x2000, 0X40);

                    string chrId = Path.GetFileName(FilePath).Substring(0, 5);
                    byte[] CharacterString = Encoding.Unicode.GetBytes(chrId);

                    Memory.WriteBytes(stringAddress, CharacterString);

                    bytes2 = BitConverter.GetBytes((int)stringAddress);
                    Array.Copy(bytes2, 0, bytes, bytjmp, bytes2.Length);

                    if (address != IntPtr.Zero)
                    {
                        if (Memory.WriteBytes(address, bytes))
                        {
                            var threadHandle = Kernel32.CreateRemoteThread(Memory.ProcessHandle, IntPtr.Zero, 0, address, IntPtr.Zero, 0, out var threadId);
                            if (threadHandle != IntPtr.Zero)
                            {
                                Kernel32.WaitForSingleObject(threadHandle, 30000);
                            }
                        }
                        Kernel32.VirtualFreeEx(Memory.ProcessHandle, address, buffer, 2);
                        Kernel32.VirtualFreeEx(Memory.ProcessHandle, stringAddress, stringBuffer, 2);
                    }
                }
            }
        }

        private void attachProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSekiro) Memory.AttachProc("sekiro");
            else Memory.AttachProc("DarkSoulsIII");
            ReadAnimation();
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleConsole();
        }

        private void ToggleConsole()
        {
            if (Width != 640)
            {
                Width = 640;
            }
            else
            {
                Width = 1094;
            }
        }

        private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ANIBND == null) return;
            if (File.Exists(FilePath + ".bak"))
            {
                File.Copy(FilePath + ".bak", FilePath, true);
                LoadANIBND(FilePath, IsSekiro);
                forceInGameReloadToolStripMenuItem_Click(sender, e);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Anim == null) return;
            if (DialogResult.Yes == MessageBox.Show("Really delete selected animation?", "Confirm", MessageBoxButtons.YesNo))
            {
                int index = AnimListBox.SelectedIndex;
                TAE.TAE.Animations.Remove(Anim.Animation);
                if (IsSekiro)
                {
                    AnimListBox.DataSource = (TAE as TAE4Handler).TAE.Animations.Select(a => new ANIM4Handler(a)).ToList();
                }
                else
                {
                    AnimListBox.DataSource = (TAE as TAE3Handler).TAE.Animations.Select(a => new ANIM3Handler(a)).ToList();
                }
                AnimListBox.SelectedIndex = index - 1;
            }
        }

        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Event == null) return;
            if (IsSekiro)
            {
                CopiedEvent = (Event as EVENT4Handler).Event;
            }
            else
            {
                CopiedEvent = (Event as EVENT3Handler).Event;
            }
        }

        private void pasteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopiedEvent == null || Anim == null) return;
            if (IsSekiro)
            {
                var a = Anim as ANIM4Handler;
                a.Events.Add(CopiedEvent.Clone());
                EventListBox.DataSource = a.Events.Select(evt => new EVENT4Handler(evt)).ToList();
            }
            else
            {
                var a = Anim as ANIM3Handler;
                a.Events.Add(CopiedEvent.Clone());
                EventListBox.DataSource = a.Events.Select(evt => new EVENT3Handler(evt)).ToList();
            }
            EventListBox.SelectedIndex = EventListBox.Items.Count - 1;
        }

        private bool ConfirmClose()
        {
            return MessageBox.Show("Really exit?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !ConfirmClose();
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Anim == null) return;
            if (IsSekiro) SekiroCopyAll = ANIM4.Events.Select(evt => evt.Clone()).ToList();
            else DS3CopyAll = ANIM3.Events.Select(evt => evt.Clone()).ToList();
        }

        private void pasteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Anim == null) return;
            if (IsSekiro) ANIM4.Events.AddRange(SekiroCopyAll);
            else ANIM3.Events.AddRange(DS3CopyAll);
            AnimListBox_SelectedIndexChanged(sender, e);
        }

        private void ClearAnimDataBtn_Click(object sender, EventArgs e)
        {
        }

        private void EventDataGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {

        }

        private void EditArrayBtn_Click(object sender, EventArgs e)
        {
            var editor = new ArrayEditor(MaskArray, MaskArrayTitle);
            editor.ShowDialog();
            MaskArrayInfo.SetValue(Event.Event, editor.OutArray);
            MaskArray = MaskArrayInfo.GetValue(Event.Event);
        }

        private void EventDataGrid_SelectedObjectsChanged(object sender, EventArgs e)
        {
            if (Event == null)
            {
                EditArrayBtn.Text = "Edit Array";
                EditArrayBtn.Enabled = false;
            } else
            {
                var props = Event.Event.GetType().GetProperties() as PropertyInfo[];
                foreach (var prop in props)
                {
                    if (prop.PropertyType.ToString() == "System.Byte[]")
                    {
                        MaskArrayInfo = prop;
                        MaskArray = MaskArrayInfo.GetValue(Event.Event);
                        MaskArrayTitle = MaskArrayInfo.Name;
                        EditArrayBtn.Text = "Edit " + MaskArrayTitle;
                        EditArrayBtn.Enabled = true;
                        return;
                    }
                }
                EditArrayBtn.Text = "Edit Array";
                EditArrayBtn.Enabled = false;
            }
        }
    }

    public static class UTIL
    {
        public static GUI gui;

        public static void Init(GUI g) => gui = g;

        private delegate void SafeCallDelegate(string text);

        public static void WriteLog(string text)
        {
            var c = gui.ConsoleBox;
            if (c.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteLog);
                c.Invoke(d, new object[] { text });
            }
            else
            {
                c.Text += text + Environment.NewLine;
                c.SelectionStart = c.TextLength;
                c.ScrollToCaret();
            }
        }

        public static void InitByteArray(byte[] array, int length)
        {
            for (int i = 0; i < length; i++)
            {
                array[i] = 0;
            }
        }

        public static void LogException(string message, Exception ex)
        {
            var sb = new StringBuilder(Environment.NewLine);
            sb.AppendLine("* " + message + " *" + Environment.NewLine + ex.Message);

            var trace = ex.StackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < Math.Min(trace.Count(), 6); i++)
            {
                var line = trace[i];
                int sfIndex = line.LastIndexOf("SoulsFormats");
                int edIndex = line.LastIndexOf("TAE3-Editor");
                if (sfIndex > -1) sb.AppendLine("  at " + line.Substring(sfIndex));
                else if (edIndex > -1) sb.AppendLine("  at " + line.Substring(edIndex));
                else sb.AppendLine("  " + line);
            }
            WriteLog(sb.ToString());
        }
    }

    public class AnimQueueItem
    {
        public int AnimId;
        public float Time;

        public AnimQueueItem(int a, float t)
        {
            AnimId = a;
            Time = t;
        }
    }

    public class ControlWriter : TextWriter
    {
        private Control textbox;

        private delegate void SafeCallDelegate(string text);

        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            if (textbox.InvokeRequired)
            {
                var d = new SafeCallDelegate(Write);
                textbox.Invoke(d, new object[] { value });
            }
            else
            {
                textbox.Text += value;

            }
        }

        public override void Write(string value)
        {
            if (textbox.InvokeRequired)
            {
                var d = new SafeCallDelegate(Write);
                textbox.Invoke(d, new object[] { value });
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }

    public class TAE3Handler
    {
        public BinderFile File { get; }
        public TAE3 TAE { get; set; }
        public bool IsValid => TAE != null;

        public TAE3Handler(BinderFile file)
        {
            File = file;
            try
            {
                Load();
                UTIL.WriteLog(Path.GetFileName(file.Name) + " loaded.");
            }
            catch (Exception ex)
            {
                UTIL.LogException(Path.GetFileName(File.Name) + " failed to load.", ex);
            }
        }

        public void Save()
        {
            File.Bytes = TAE.Write();
        }

        public void Load()
        {
            TAE = TAE3.Read(File.Bytes);
        }

        override public string ToString() => System.IO.Path.GetFileName(File.Name);
    }

    public class TAE4Handler
    {
        public BinderFile File { get; }
        public TAE4 TAE { get; set; }
        public bool IsValid => TAE != null;

        public TAE4Handler(BinderFile file)
        {
            File = file;
            try
            {
                Load();
                UTIL.WriteLog(Path.GetFileName(file.Name) + " loaded.");
            }
            catch (Exception ex)
            {
                UTIL.LogException(Path.GetFileName(File.Name) + " failed to load.", ex);
            }
        }

        public void Save()
        {
            File.Bytes = TAE.Write();
        }

        public void Load()
        {
            TAE = TAE4.Read(File.Bytes);
        }

        override public string ToString() => System.IO.Path.GetFileName(File.Name);
    }

    public class ANIM3Handler
    {

        [Browsable(false)]
        public TAE3.Animation Animation { get; }

        [Browsable(false)]
        public List<TAE3.Event> Events => Animation.Events;
        [Browsable(false)]
        public List<TAE3.EventGroup> EventGroups => Animation.EventGroups;


        public long ID
        {
            get => Animation.ID;
            set => Animation.ID = value;
        }

        public int Unk28
        {
            get => Animation.Unk28;
            set => Animation.Unk28 = value;
        }

        public bool AnimFileReference
        {
            get => Animation.AnimFileReference;
            set => Animation.AnimFileReference = value;
        }

        public string AnimFileName
        {
            get => Animation.AnimFileName;
            set => Animation.AnimFileName = value;
        }

        public int AnimFileUnk18
        {
            get => Animation.AnimFileUnk18;
            set => Animation.AnimFileUnk18 = value;
        }

        public int AnimFileUnk20
        {
            get => Animation.AnimFileUnk20;
            set => Animation.AnimFileUnk20 = value;
        }

        public ANIM3Handler(TAE3.Animation anim)
        {
            Animation = anim;
        }

        override public string ToString() => ID.ToString();

    }

    public class ANIM4Handler
    {

        [Browsable(false)]
        public TAE4.Animation Animation { get; }

        [Browsable(false)]
        public List<TAE4.Event> Events => Animation.Events;
        [Browsable(false)]
        public List<TAE4.EventGroup> EventGroups => Animation.EventGroups;


        public long ID
        {
            get => Animation.ID;
            set => Animation.ID = value;
        }

        public int Unk28
        {
            get => Animation.Unk28;
            set => Animation.Unk28 = value;
        }

        public bool AnimFileReference
        {
            get => Animation.AnimFileReference;
            set => Animation.AnimFileReference = value;
        }

        public string AnimFileName
        {
            get => Animation.AnimFileName;
            set => Animation.AnimFileName = value;
        }

        public int AnimFileUnk18
        {
            get => Animation.AnimFileUnk18;
            set => Animation.AnimFileUnk18 = value;
        }

        public int AnimFileUnk20
        {
            get => Animation.AnimFileUnk20;
            set => Animation.AnimFileUnk20 = value;
        }

        public ANIM4Handler(TAE4.Animation anim)
        {
            Animation = anim;
        }

        override public string ToString() => ID.ToString();

    }

    public class EVENT3Handler
    {
        [Browsable(false)]
        public TAE3.Event Event;

        public float StartTime => Event.StartTime;
        public float EndTime => Event.EndTime;

        public EVENT3Handler(TAE3.Event evt) => Event = evt;

        public override string ToString() => Event.ToString();
    }

    public class EVENT4Handler
    {
        [Browsable(false)]
        public TAE4.Event Event;

        public float StartTime => Event.StartTime;
        public float EndTime => Event.EndTime;

        public EVENT4Handler(TAE4.Event evt) => Event = evt;

        public override string ToString() => Event.ToString();
    }
}
