using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace MegaTAE
{
    public class Memory
    {
        //Memory Stuff
        public static bool Is64Bit => IntPtr.Size == 8;

        public static IntPtr ProcessHandle { get; set; }

        public static Process Process { get; set; }

        public static IntPtr BaseAddress { get; set; }
        //

        public static IntPtr AttachProc(string procName)
        {
            var ZeroRt = new IntPtr(0);
            var processes = System.Diagnostics.Process.GetProcessesByName(procName);
            if (processes.Length > 0)
            {
                var Process = processes[0];
                BaseAddress = Process.MainModule.BaseAddress;
                ProcessHandle = Kernel32.OpenProcess(0x2 | 0x8 | 0x10 | 0x20 | 0x400, false, Process.Id);
                return ProcessHandle;
            }
            else
            {
                Console.WriteLine("Cant find process. Is it running?", "Process");
                return ZeroRt;
            }
        }

        // read address
        public static byte ReadInt8(IntPtr address)
        {
            var readBuffer = new byte[sizeof(byte)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)1, UIntPtr.Zero);
            var value = readBuffer[0];
            return value;
        }

        public static short ReadInt16(IntPtr address)
        {
            var readBuffer = new byte[sizeof(short)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)2, UIntPtr.Zero);
            var value = BitConverter.ToInt16(readBuffer, 0);
            return value;
        }

        public static int ReadInt32(IntPtr address)
        {
            var readBuffer = new byte[sizeof(int)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)readBuffer.Length, UIntPtr.Zero);
            var value = BitConverter.ToInt32(readBuffer, 0);
            return value;
        }

        public static long ReadInt64(IntPtr address)
        {
            var readBuffer = new byte[sizeof(long)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)readBuffer.Length, UIntPtr.Zero);
            var value = BitConverter.ToInt64(readBuffer, 0);
            return value;
        }

        public static float ReadFloat(IntPtr address)
        {
            var readBuffer = new byte[sizeof(float)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)readBuffer.Length, UIntPtr.Zero);
            var value = BitConverter.ToSingle(readBuffer, 0);
            return value;
        }

        public static double ReadDouble(IntPtr address)
        {
            var readBuffer = new byte[sizeof(double)];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)readBuffer.Length, UIntPtr.Zero);
            var value = BitConverter.ToDouble(readBuffer, 0);
            return value;
        }

        public static string ReadString(IntPtr address, int length, string encodingName)
        {
            var readBuffer = new byte[length];
            var success = Kernel32.ReadProcessMemory(ProcessHandle, address, readBuffer, (UIntPtr)readBuffer.Length, UIntPtr.Zero);
            var encodingType = System.Text.Encoding.GetEncoding(encodingName);
            string value = encodingType.GetString(readBuffer, 0, readBuffer.Length);

            return value;
        }

        //write to address
        public static bool WriteInt8(IntPtr address, byte value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)1, UIntPtr.Zero);
        }

        public static bool WriteInt16(IntPtr address, short value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)2, UIntPtr.Zero);
        }

        public static bool WriteInt32(IntPtr address, int value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)4, UIntPtr.Zero);
        }

        public static bool WriteInt64(IntPtr address, long value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)8, UIntPtr.Zero);
        }

        public static bool WriteFloat(IntPtr address, float value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)4, UIntPtr.Zero);
        }

        public static bool WriteDouble(IntPtr address, double value)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)8, UIntPtr.Zero);
        }

        public static bool WriteBytes(IntPtr address, Byte[] val)
        {
            return Kernel32.WriteProcessMemory(ProcessHandle, address, val, new UIntPtr((uint)val.Length), UIntPtr.Zero);
        }

        public static bool WriteUnicodeString(IntPtr address, string String)
        {
            byte[] val = Encoding.Unicode.GetBytes(String);
            return Kernel32.WriteProcessMemory(ProcessHandle, address, val, new UIntPtr((uint)val.Length), UIntPtr.Zero);
        }

        public static bool WriteASCIIString(IntPtr address, string String)
        {
            byte[] val = Encoding.ASCII.GetBytes(String);
            return Kernel32.WriteProcessMemory(ProcessHandle, address, val, new UIntPtr((uint)val.Length), UIntPtr.Zero);
        }
    }
}
