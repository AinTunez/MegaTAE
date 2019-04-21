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
using System.Diagnostics;
using System.Collections;
using Microsoft.VisualBasic;
using System.Runtime.Remoting;

namespace MegaTAE
{
    public partial class GUI : Form
    {

        public BND4 ANIBND;
        public string FilePath;
        public bool IsSekiro;
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

        public GUI()
        {
            InitializeComponent();
            UTIL.Init(this);
            ToggleConsole();
        }

        public void ReadAnimation ()
        {
            Timer animTimer = new Timer();
            animTimer.Tick += new EventHandler(checkAnim);
            animTimer.Interval = 16;
            animTimer.Start();
            
            void checkAnim(object sender, EventArgs e)
            {
                if (ANIBND == null) CurrentAnimBox.Text = "-1";
                else
                {
                    var aNum = GetCurrentAnimation();
                    if (aNum == null) return;
                    string curr = CurrentAnimBox.Text;
                    string anim = GetCurrentAnimation().ToString();
                    if (IsSekiro)
                    {
                        if (anim == "790010" || anim == "790040") return;
                        if (anim != CurrentAnimBox.Text) UTIL.WriteLog("PLAYER ANIM --> " + CurrentAnimBox.Text);
                        CurrentAnimBox.Text = anim;
                    } else
                    {
                        if (anim != CurrentAnimBox.Text) UTIL.WriteLog("PLAYER ANIM --> " + CurrentAnimBox.Text);
                        CurrentAnimBox.Text = anim;
                    }

                }
            }
        }

        private static bool IsValidAddress(Int64 address)
        {
            return (address >= 0x10000 && address < 0x000F000000000000);
        }

        internal long? ReadPointerChain(long baseAddress, long baseOffset, int[] chain)
        {
            long lpCurrent = Memory.ReadInt64(new IntPtr(baseAddress + baseOffset));
            foreach (int offset in chain)
            {
                if (!IsValidAddress(lpCurrent)) return null;
                lpCurrent = Memory.ReadInt64(new IntPtr(lpCurrent) + offset);
            }
            return lpCurrent;
        }

        private long? GetCurrentAnimation()
        {
            if (Memory.BaseAddress == IntPtr.Zero) return null;

            if (IsSekiro)
                return ReadPointerChain(Memory.BaseAddress.ToInt64(), 0x3B67DF0, new int[] { 0x88, 0x1FF8, 0x80, 0xC8 });
            else
                return ReadPointerChain(Memory.BaseAddress.ToInt64(), 0x4768E78, new int[] { 0x80, 0x1F90, 0x80, 0xC8 });

            ////read the chain
            //foreach (int offset in chain)
            //{
            //    if (!IsValidAddress(val.ToInt64())) return null;
            //    val = new IntPtr(Memory.ReadInt64(val));
            //    val = IntPtr.Add(val, offset);
            //}
            //if (IsValidAddress(val.ToInt64())) return Memory.ReadInt32(val);
            //return null;
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
            } catch (Exception ex)
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
                    Memory.AttachProc("sekiro");

                } else
                {
                    var t = new TAE3Handler(file);
                    if (t.IsValid) tae3_list.Add(new TAE3Handler(file));
                    Memory.AttachProc("DS3");
                }
            }



            void refreshList()
            {
                if (TaeListBox.InvokeRequired)
                {
                    TaeListBox.Invoke(new MethodInvoker(refreshList));
                } else
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
            } else
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
                var handler = (ANIM4Handler)AnimListBox.SelectedItem;
                AnimDataGrid.SelectedObject = handler;
                EventListBox.DataSource = handler.Events.Select(evt => new EVENT4Handler(evt)).ToList();
            } else
            {
                var handler = (ANIM3Handler)AnimListBox.SelectedItem;
                AnimDataGrid.SelectedObject = handler;
                EventListBox.DataSource = handler.Events.Select(evt => new EVENT3Handler(evt)).ToList();
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
                    ReadAnimation();
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
                        EventListBox.DataSource = (Anim as ANIM4Handler).Events.Select(evt => new EVENT4Handler(evt)).ToList();
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
                        EventListBox.DataSource = (Anim as ANIM3Handler).Events.Select(evt => new EVENT3Handler(evt)).ToList();
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
                    var anim4 = Anim as ANIM4Handler;
                    foreach (var group in anim4.EventGroups)
                    {
                        foreach (var index in group.Indices.ToList())
                        {
                            if (index > anim4.Events.Count - 1 || events.Contains(index))
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
                        if (!events.Contains(i)) anim4.EventGroups[0].Indices.Add(i);
                    }
                } else
                {
                    var anim3 = Anim as ANIM3Handler;
                    foreach (var group in anim3.EventGroups)
                    {
                        foreach (var index in group.Indices.ToList())
                        {
                            if (index > anim3.Events.Count - 1 || events.Contains(index))
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
                        if (!events.Contains(i)) anim3.EventGroups[0].Indices.Add(i);
                    }
                }

            } catch (Exception ex)
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
                EventListBox.DataSource = (Anim as ANIM4Handler).Events.Select(evt => new EVENT4Handler(evt)).ToList();
                while (index > EventListBox.Items.Count - 1) index--;
                if (index > -1) EventListBox.SelectedIndex = index;
            } else
            {
                Anim.Events.Remove(((EVENT3Handler)EventListBox.SelectedItem).Event);
                FixEventGroups();
                EventListBox.DataSource = (Anim as ANIM3Handler).Events.Select(evt => new EVENT3Handler(evt)).ToList();
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
            if (IsSekiro)
            {
                Memory.WriteInt8(Memory.BaseAddress + 0x3B67F7D, 1);

                var bytes = new byte[] { 0x49, 0xBE, 0x00, 0x8B, 0xA3, 0x40, 0x01, 0x00, 0x00, 0x00, 0x48, 0xA1, 0xF0, 0x7D, 0xB6, 0x43, 0x01, 0x00, 0x00, 0x00, 0x48, 0x8B, 0xC8, 0x48, 0x8D, 0x14, 0x25, 0, 0, 0, 0, 0x48, 0x83, 0xEC, 0x28, 0x41, 0xFF, 0xD6, 0x48, 0x83, 0xC4, 0x28, 0xC3 };
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
            else
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

        private void attachProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSekiro) Memory.AttachProc("sekiro");
            else Memory.AttachProc("DarkSoulsIII");
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleConsole();
        }

        private void ToggleConsole ()
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
            } else
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
