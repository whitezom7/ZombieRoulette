using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace ZombieRoulette

    


{
    public static class Constants
    {
        public const int C_MAPADDRESS = 0x02F67B6C;
    }

    public static class BlackOpsLibrary
    {
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int _HandleProcess, int _LpBaseAddress, byte[] _LpBuffer, int _DwSize, ref int _LpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int _HandleProcess, int _LpBaseAddress, byte[] _LpBuffer, int _DwSize, ref int _LpNumberOfBytesRead);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr _HandleWindow, int _Msg, int _WParam, int _LParam);

        public static Process m_BlackOpsProcess = null;
        public static IntPtr? m_ProcessHandle = IntPtr.Zero;

        public static int ReadInt(int _Address)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[24];
            ReadProcessMemory((int)m_ProcessHandle, _Address, buffer, buffer.Length, ref bytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }
        public static bool WriteInt(int _Address, int _Value)
        {
            int lpNumberOfBytesWritten = 0;
            byte[] bytes = BitConverter.GetBytes(_Value);
            WriteProcessMemory((int)m_ProcessHandle, _Address, bytes, 4, ref lpNumberOfBytesWritten);
            return (lpNumberOfBytesWritten != 0);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(zombie_map[2]);
            Console.WriteLine(IsT5Open());
            Console.Read();
            ChangeMap(0);
            RandomMap();
        }
        public string m_ProcessHandle = "";
        // Checks to see if the plutonium process is running indicating the bo1 process is running
        static bool IsT5Open()
        {
            return !(Process.GetProcessesByName("plutonium-bootstrapper-win32").Length == 0);

        }
        public static bool IsBO1Open()
        {
            return !(Process.GetProcessesByName("BlackOps").Length == 0);
            
        }

     

        // A dictionary of all the Black ops 1 zombies map
        static Dictionary<int, string> zombie_map = new Dictionary<int, string>()
            {
                {0, "zombie_theater"},
                {1, "zombie_pentagon"},
                {2, "zombietron"},
                {3, "zombie_cosmodrome"},
                {4, "zombie_coast"},
                {5, "zombie_temple"},
                {6, "zombie_moon"},
                {7, "zombie_cod5_prototype"},
                {8, "zombie_cod5_asylum"},
                {9, "zombie_cod5_sumpf"},
                {10, "zombie_cod5_factory"},
            };

        // This function has the ability to change the players map based on the switch case of the key provided
        static void ChangeMap(int zombie_map)
        {
            while (!IsT5Open() || !IsBO1Open())
            {
                Console.WriteLine("Black Ops 1 is not open. Waiting...");
                Thread.Sleep(10000); // Sleeps for 10 seconds before retrying
            }
            {
                switch (zombie_map)
                {
                    case 0:
                        Console.WriteLine($"Starting Kino");
                        break;
                    case 1:
                        Console.WriteLine("Starting Five");
                        break;
                    case 2:
                        Console.WriteLine("Starting Dead Ops Arcade");
                        break;
                    case 3:
                        Console.WriteLine("Starting Ascension");
                        break;
                    case 4:
                        Console.WriteLine("Starting Call of the Dead");
                        break;
                    case 5:
                        Console.WriteLine("Starting Shangri-La");
                        break;
                    case 6:
                        Console.WriteLine("Starting Moon");
                        break;
                    case 7:
                        Console.WriteLine("Starting Nacht der Untoten");
                        break;
                    case 8:
                        Console.WriteLine("Starting Verrückt");
                        break;
                    case 9:
                        Console.WriteLine("Starting Shi No Numa");
                        break;
                    case 10:
                        Console.WriteLine("Starting Der Riese");
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
        }
        static void RandomMap()
        {
            Random random = new Random();
            int rnd_map = random.Next(0, 10);

            ChangeMap(rnd_map);
        }
    }
}
