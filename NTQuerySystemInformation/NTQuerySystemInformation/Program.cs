using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace mProcessTool
{
    static class NtDll_dll
    {
        [DllImport("ntdll.dll")]
        public static extern NtStatus NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS InfoClass, IntPtr Info, uint Size, out uint Length);

        [DllImport("ntdll.dll")]
        public static extern int NtQueryObject(IntPtr objectHandle, OBJECT_INFORMATION_CLASS informationClass, IntPtr informationPtr, int informationLength, ref int returnLength);
    }

    class mProcessTool
    {
        private string fileName;
        private int processId;
        private string processName;
        private bool closeHandle;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (!File.Exists(FileName))
                    throw new FileNotFoundException("[!]Error. File is not found.", FileName);
                else
                    fileName = FileName;
            }
        }
        
        public int ProcessId
        {
            get
            {
                return processId;
            }
            set
            {
                try
                {
                    Process tProcess = Process.GetProcessById(ProcessId);
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine("[!]Error: {0}", e.Message);
                    Environment.Exit(0);
                }
            }
        }

        public string ProcessName
        {
            get
            {
                return processName;
            }
            set
            {
                Process[] Processes = Process.GetProcessesByName(ProcessName);
                if (Processes.Length == 0)
                    throw new RankException("No process with name " + ProcessName);
            }
        }
        
        public bool CloseHandle
        {
            get { return closeHandle; }
            set { closeHandle = CloseHandle; }
        }

        public mProcessTool(string FileName) : this(FileName, null, false) { }
        public mProcessTool(int ProcessId) : this(null, ProcessId, false) { }
        public mProcessTool(string FileName, int ProcessId) : this(FileName, ProcessId, false) { }
        public mProcessTool(string FileName, string ProcessName) : this(FileName, ProcessName, false) { }
        public mProcessTool(string FileName, int ProcessId, bool CloseHandle)
        {
            if(FileName != null)
                this.FileName = FileName;
            this.ProcessId = ProcessId;
            this.CloseHandle = CloseHandle;
        }
        
        public mProcessTool(string FileName, string ProcessName, bool CloseHandle)
        {
            this.FileName = FileName;
            if (ProcessName != null)
                this.ProcessName = ProcessName;
            this.CloseHandle = CloseHandle;
        }

        private static IntPtr NtQuerySystemInfoHelper(SYSTEM_INFORMATION_CLASS infoClass, uint infoLength = 0)
        {
            while(true)
            {
                var infoPtr = Marshal.AllocHGlobal((int)infoLength);
                uint outInfoLength;

                var result = NtDll_dll.NtQuerySystemInformation(infoClass, infoPtr, infoLength, out outInfoLength);

                if (result == NtStatus.Success)
                {
                    Debug.Print("NtStatus: {0}", result);
                    return infoPtr;
                }
                else
                {
                    Marshal.FreeHGlobal(infoPtr);
                    if (result != NtStatus.InfoLengthMismatch && result != NtStatus.BufferOverflow && result != NtStatus.BufferTooSmall)
                    {
                        Debug.Print("NtStatus result code: {0}", result);
                        return IntPtr.Zero;
                    }
                    infoLength = Math.Max(infoLength, outInfoLength);
                }
            }
        }        

        static bool Is64Bits()
        {
            //Console.WriteLine("IntPtr size: {0}", IntPtr.Size);
            return IntPtr.Size == 8 ? true : false;
        }

        static string GetWindowsVersion()
        {
            OperatingSystem OSVersion = Environment.OSVersion;
            return OSVersion.Version.ToString();
        }

        static string GetKernelObject(int HandlerValue)
        {
            string KernelObject = null;
            Hashtable KernelObjects = new Hashtable();

            string WindowsVersion = GetWindowsVersion();
            if(WindowsVersion.Contains("10.0"))
            {
                //Windows 10
                KernelObjects.Add(0x03, "Directory");
                KernelObjects.Add(0x04, "SymbolicLink");
                KernelObjects.Add(0x05, "Token");
                KernelObjects.Add(0x08, "Process");
                KernelObjects.Add(0x0D, "Event");
                KernelObjects.Add(0x0E, "Mutant");
                KernelObjects.Add(0x10, "Semaphore");
                KernelObjects.Add(0x11, "Timer");
                KernelObjects.Add(0x12, "IRTimer");
                KernelObjects.Add(0x15, "WindowsStation");
                KernelObjects.Add(0x16, "Desktop");
                KernelObjects.Add(0x17, "Composition");
                KernelObjects.Add(0x18, "RawInputManager");
                KernelObjects.Add(0x19, "TpWorkerFactory");
                KernelObjects.Add(0x1E, "IoCompletion");
                KernelObjects.Add(0x1F, "WaitCompletionPacket");
                KernelObjects.Add(0x20, "File");
                KernelObjects.Add(0x21, "TmTm");
                KernelObjects.Add(0x22, "TmTx");
                KernelObjects.Add(0x23, "TmRm");
                KernelObjects.Add(0x24, "TmEn");
                KernelObjects.Add(0x25, "Section");
                KernelObjects.Add(0x26, "Session");
                KernelObjects.Add(0x27, "Partition");
                KernelObjects.Add(0x28, "Key");
                KernelObjects.Add(0x29, "ALPC Port");
                KernelObjects.Add(0x2C, "EtwRegistration");
                KernelObjects.Add(0x2F, "DmaDomain");
                KernelObjects.Add(0x31, "FilterConnectionPort");
            }
            else if(WindowsVersion.Contains("6.2"))
            {
                //Windows 8 and Windows Server 2012
                KernelObjects.Add(0x02, "Type");
                KernelObjects.Add(0x03, "Directory");
                KernelObjects.Add(0x04, "SymbolicLink");
                KernelObjects.Add(0x05, "Token");
                KernelObjects.Add(0x06, "Job");
                KernelObjects.Add(0x07, "Process");
                KernelObjects.Add(0x08, "Thread");
                KernelObjects.Add(0x0D, "Event");
                KernelObjects.Add(0x0E, "Mutant");
                KernelObjects.Add(0x0F, "Callback");
                KernelObjects.Add(0x10, "Semaphore");
                KernelObjects.Add(0x11, "Timer");
                KernelObjects.Add(0x12, "IRTimer");
                KernelObjects.Add(0x13, "Profile");
                KernelObjects.Add(0x14, "KeyedEvent");
                KernelObjects.Add(0x15, "WindowsStation");
                KernelObjects.Add(0x16, "Desktop");
                KernelObjects.Add(0x17, "CompositionSurface");
                KernelObjects.Add(0x18, "TpWorkerFactory");
                KernelObjects.Add(0x19, "Adapter");
                KernelObjects.Add(0x1A, "Controller");
                KernelObjects.Add(0x1B, "Device");
                KernelObjects.Add(0x1C, "Driver");
                KernelObjects.Add(0x1D, "IoCompletion");
                KernelObjects.Add(0x1E, "WaitCompletionPacket");
                KernelObjects.Add(0x1F, "File");
                KernelObjects.Add(0x20, "TmTm");
                KernelObjects.Add(0x21, "TmTx");
                KernelObjects.Add(0x22, "TmRm");
                KernelObjects.Add(0x23, "TmEn");
                KernelObjects.Add(0x24, "Section");
                KernelObjects.Add(0x25, "Session");
                KernelObjects.Add(0x26, "Key");
                KernelObjects.Add(0x27, "Partition");
                KernelObjects.Add(0x28, "PowerRequest");
                KernelObjects.Add(0x29, "WmiGuid");
                KernelObjects.Add(0x2A, "EtwRegistration");
                KernelObjects.Add(0x2B, "EtwConsumer");
                KernelObjects.Add(0x2C, "FilterConnectionPort");
                KernelObjects.Add(0x2D, "FilterCommunicationPort");
                KernelObjects.Add(0x2E, "PcwObject");
                KernelObjects.Add(0x2F, "DxgkSharedResource");
                KernelObjects.Add(0x30, "DxgkSharedSyncObject");
            }
            else if(WindowsVersion.Contains("6.1"))
            {
                //Windows 7 and Windows Server 2008 R2
                KernelObjects.Add(0x02, "Type");
                KernelObjects.Add(0x03, "Directory");
                KernelObjects.Add(0x04, "SymbolicLink");
                KernelObjects.Add(0x05, "Token");
                KernelObjects.Add(0x06, "Job");
                KernelObjects.Add(0x07, "Process");
                KernelObjects.Add(0x08, "Thread");
                KernelObjects.Add(0x09, "UserApcReserve");
                KernelObjects.Add(0x0A, "IoCompletionReserve");
                KernelObjects.Add(0x0B, "DebugObject");
                KernelObjects.Add(0x0C, "Event");
                KernelObjects.Add(0x0D, "EventPair");
                KernelObjects.Add(0x0E, "Mutant");
                KernelObjects.Add(0x0F, "Callback");
                KernelObjects.Add(0x10, "Semaphore");
                KernelObjects.Add(0x11, "Timer");
                KernelObjects.Add(0x12, "Profile");
                KernelObjects.Add(0x13, "KeyedEvent");
                KernelObjects.Add(0x14, "WindowsStation");
                KernelObjects.Add(0x15, "Desktop");
                KernelObjects.Add(0x16, "TpWorkerFactory");
                KernelObjects.Add(0x17, "Adapter");
                KernelObjects.Add(0x18, "Controller");
                KernelObjects.Add(0x19, "Device");
                KernelObjects.Add(0x1A, "Driver");
                KernelObjects.Add(0x1B, "IoCompletion");
                KernelObjects.Add(0x1C, "File");
                KernelObjects.Add(0x1D, "TmTm");
                KernelObjects.Add(0x1E, "TmTx");
                KernelObjects.Add(0x1F, "TmRm");
                KernelObjects.Add(0x20, "TmEn");
                KernelObjects.Add(0x21, "Section");
                KernelObjects.Add(0x22, "Session");
                KernelObjects.Add(0x23, "Key");
                KernelObjects.Add(0x24, "ALPC Port");
                KernelObjects.Add(0x25, "PowerRequest");
                KernelObjects.Add(0x26, "WmiGuid");
                KernelObjects.Add(0x27, "EtwRegistration");
                KernelObjects.Add(0x28, "EtwConsumer");
                KernelObjects.Add(0x29, "FilterConnectionPort");
                KernelObjects.Add(0x2A, "FilterCommunicationPort");
                KernelObjects.Add(0x2B, "PcwObject");
            }
            else if(WindowsVersion.Contains("6.0"))
            {
                //Windows Vista and Windows Server 2008
                KernelObjects.Add(0x01, "Type");
                KernelObjects.Add(0x02, "Directory");
                KernelObjects.Add(0x03, "SymbolicLink");
                KernelObjects.Add(0x04, "Token");
                KernelObjects.Add(0x05, "Job");
                KernelObjects.Add(0x06, "Process");
                KernelObjects.Add(0x07, "Thread");
                KernelObjects.Add(0x08, "DebugObject"); 
                KernelObjects.Add(0x09, "Event");
                KernelObjects.Add(0x0A, "EventPair");
                KernelObjects.Add(0x0B, "Mutant");
                KernelObjects.Add(0x0C, "Callback");
                KernelObjects.Add(0x0D, "Semaphore");
                KernelObjects.Add(0x0E, "Timer");
                KernelObjects.Add(0x0F, "Profile");
                KernelObjects.Add(0x10, "KeyedEvent");
                KernelObjects.Add(0x11, "WindowsStation");
                KernelObjects.Add(0x12, "Desktop");
                KernelObjects.Add(0x13, "TpWorkerFactory");
                KernelObjects.Add(0x14, "Adapter");
                KernelObjects.Add(0x15, "Controller");
                KernelObjects.Add(0x16, "Device");
                KernelObjects.Add(0x17, "Driver");
                KernelObjects.Add(0x18, "IoCompletion");
                KernelObjects.Add(0x19, "File");
                KernelObjects.Add(0x1a, "TmTm");
                KernelObjects.Add(0x1B, "TmTx");
                KernelObjects.Add(0x1C, "TmRm");
                KernelObjects.Add(0x1D, "TmEn");
                KernelObjects.Add(0x1E, "Section");
                KernelObjects.Add(0x1F, "Session");
                KernelObjects.Add(0x20, "Key");
                KernelObjects.Add(0x21, "ALPC Port");
                KernelObjects.Add(0x22, "WmiGuid");
                KernelObjects.Add(0x23, "EtwRegistration");
                KernelObjects.Add(0x24, "FilterConnectionPort");
                KernelObjects.Add(0x25, "FilterCommunicationPort");
            }

            if (KernelObjects.ContainsKey(HandlerValue))
                KernelObject = (KernelObjects[HandlerValue]).ToString();
            else
                KernelObject = "undefined";

            return KernelObject;
        }

        static void ShowTable(DataTable Table, string ProcessId)
        {
            BindingSource Source = new BindingSource();
            Source.DataSource = Table;

            DataGridView OutGrid = new DataGridView();
            OutGrid.DataSource = Source;
            OutGrid.ReadOnly = true;
            OutGrid.AutoSize = true;
            OutGrid.ColumnHeadersVisible = true;
            OutGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            OutGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            Form tableForm = new Form();
            tableForm.Size = new Size(500, 768);
            tableForm.AutoScroll = true;
            tableForm.Text = "Handlers of Process " + ProcessId;
            tableForm.Controls.Add(OutGrid);
            tableForm.ShowDialog();
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You must specify at least one argument argument [-File] | [-Id] | [-Name]");
                return;
            }

            int ProcessId = 0;
            string ProcessName;
            switch(args[0])
            {
                case "-File":
                    
                break;
                case "-id":
                    ProcessId = Int32.Parse(args[1]);
                    Process tProcess = Process.GetProcessById(ProcessId);
                    ProcessName = tProcess.ProcessName;
                break;
            }

            IntPtr ipHandle = IntPtr.Zero;
            SYSTEM_INFORMATION_CLASS HandleInfoClass = SYSTEM_INFORMATION_CLASS.SystemHandleInformation;
            SYSTEM_HANDLE_INFORMATION StrHandleInformation;

            IntPtr HandleInfoPtr = NtQuerySystemInfoHelper(HandleInfoClass);

            long TotalHandlers = 0;
            if (Is64Bits())
            {
                TotalHandlers = Marshal.ReadInt64(HandleInfoPtr);
                ipHandle = new IntPtr(HandleInfoPtr.ToInt64() + 8);
            }
            else
            {
                TotalHandlers = Marshal.ReadInt32(HandleInfoPtr);
                ipHandle = new IntPtr(HandleInfoPtr.ToInt32() + 4);
            }
            Console.WriteLine("Total handlers: {0}", TotalHandlers);

            DataTable hTable = new DataTable();
            hTable.Columns.Add("Kernel Object Value");
            hTable.Columns.Add("Kernel Object Number");
            hTable.Columns.Add("Kernel Object Type");

            int TotalProcessHandlers = 0;
            for (long i=0; i < TotalHandlers; i++)
            {
                StrHandleInformation = new SYSTEM_HANDLE_INFORMATION();

                if (!Is64Bits())
                {
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(StrHandleInformation));
                }
                Console.WriteLine(ipHandle);

                StrHandleInformation = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, typeof(SYSTEM_HANDLE_INFORMATION));

                if (Is64Bits())
                {
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(StrHandleInformation) + 8);
                }
                if (StrHandleInformation.ProcessId != ProcessId)
                    continue;
                TotalProcessHandlers++;
                hTable.Rows.Add(StrHandleInformation.HandleValue, StrHandleInformation.ObjectTypeNumber, GetKernelObject(StrHandleInformation.ObjectTypeNumber));
            }
            Marshal.FreeHGlobal(HandleInfoPtr);
            Console.WriteLine("Process ID: {0}, Process name: {1} TotalHandlers: {2}", ProcessId, ProcessName, TotalProcessHandlers);
            string TableHeader = ProcessName + " - " + ProcessId.ToString(); 
            ShowTable(hTable, TableHeader);
        }
    }
}
