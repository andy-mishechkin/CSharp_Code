using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace mProcessTool
{
    static class NtDll_dll
    {
        [DllImport("ntdll.dll")]
        public static extern NtStatus NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS InfoClass, IntPtr Info, uint Size, out uint Length);

        [DllImport("ntdll.dll")]
        public static extern int NtQueryObject(IntPtr objectHandle, OBJECT_INFORMATION_CLASS informationClass, IntPtr informationPtr, int informationLength, ref int returnLength);
    }

    static class Kernel32_dll
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle,
                                                  uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, DuplicateOptions dwOptions);

        [DllImport("kernel32.dll")]
        public static extern uint QueryDosDevice(string lpDeviceName, System.Text.StringBuilder lpTargetPath, uint ucchMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void SetLastError(int dwErrorCode);

        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr Arguments);

        [DllImport("kernel32.dll")]
        public static extern FileType GetFileType(IntPtr hFile);
    }

    class mProcessExec
    {
        private int processId;
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
                    Process tProcess = Process.GetProcessById(value);
                    processId = value;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("[!]Error: {0}", e.Message);
                    Environment.Exit(0);
                }
            }
        }

        public mProcessExec(int ProcessId)
        {
            this.ProcessId = ProcessId;
        }

        public ArrayList GetProcessHandles()
        {
            int ProcessId = this.processId;
            int SYSTEM_HANDLE_INFORMATION_Size;

            Debug.WriteLine("Current process ID: " + ProcessId);

            IntPtr HandlePtr = IntPtr.Zero;
            SYSTEM_INFORMATION_CLASS HandleInfoClass = SYSTEM_INFORMATION_CLASS.SystemHandleInformation;
            IntPtr HandleInfoPtr = NtQuerySystemInfoHelper(HandleInfoClass);

            long TotalHandles = (IntPtr.Size == 4 ? Marshal.ReadInt32(HandleInfoPtr) : Marshal.ReadInt64(HandleInfoPtr));
            ArrayList HandlesInfo = new ArrayList();
            int TotalProcessHandles = 0;

            IntPtr SourceProcessHandle = Kernel32_dll.OpenProcess(0x40, true, ProcessId);
            IntPtr TargetProcessHandle = (Process.GetCurrentProcess()).Handle;

            string ProcessName = (Process.GetProcessById(ProcessId)).ProcessName;

            int BufferOffset = IntPtr.Size;
            for (long i = 0; i < TotalHandles; i++)
            {
                SYSTEM_HANDLE_INFORMATION HandleInfoStructure = new SYSTEM_HANDLE_INFORMATION();
                SYSTEM_HANDLE_INFORMATION_Size = Marshal.SizeOf(HandleInfoStructure);
                if (i > 0)
                    BufferOffset += SYSTEM_HANDLE_INFORMATION_Size;

                HandlePtr = HandleInfoPtr + BufferOffset;
                if (Marshal.ReadInt32(HandlePtr) == ProcessId)
                {
                    uint HandleError = 0;

                    string ObjectType = null;
                    string ObjectName = null;
                    FileType fType = FileType.FileTypeUndefined;

                    TotalProcessHandles++;
                    HandleInfoStructure = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(HandlePtr, typeof(SYSTEM_HANDLE_INFORMATION));
                    
                    IntPtr Handle = new IntPtr(HandleInfoStructure.HandleValue);
                    IntPtr TargetHandle = DuplicateHandleHelper(SourceProcessHandle, Handle, TargetProcessHandle);
                    if (TargetHandle == IntPtr.Zero)
                        continue;
                    else
                    {
                        Debug.WriteLine("--------------------------------------");
                        ObjectType = GetObjectType(Handle, TargetHandle, SourceProcessHandle, TargetProcessHandle);
                        Debug.WriteLine("Object type: " + HandleInfoStructure.ObjectTypeNumber + " --> " + ObjectType);

                        if (ObjectType == "File")
                        {
                            fType = Kernel32_dll.GetFileType(Handle);
                            Debug.WriteLine("FileType: " + fType.ToString());
                        }
                        if (fType == FileType.FileTypeUnknown)
                            HandleError = GetLastSystemError();

                        if (HandleError == 0)
                            ObjectName = GetObjectName(Handle, TargetHandle, SourceProcessHandle, TargetProcessHandle);
                        else
                            ObjectName = "Undefined";

                        if (ObjectName.Contains("\\Device\\"))
                            ObjectName = ConvertToRegularFileName(ObjectName);
                    }

                    Debug.WriteLine("Object name: " + ObjectName);

                    Hashtable hashHandleInfo = new Hashtable();
                    hashHandleInfo.Add("ObjectTypeNumber", HandleInfoStructure.ObjectTypeNumber);
                    hashHandleInfo.Add("ObjectType", ObjectType);
                    hashHandleInfo.Add("ObjectName", ObjectName);
                    hashHandleInfo.Add("HandleFlag", GetHandleFlag(HandleInfoStructure.Flags));
                    hashHandleInfo.Add("KernelPointer", $"{HandleInfoStructure.Object_Pointer.ToInt64():X}");
                    hashHandleInfo.Add("AccessMask", $"{(HandleInfoStructure.GrantedAccess & 0xFFFF0000):X}");

                    HandlesInfo.Add(hashHandleInfo);
                }
            }
            Marshal.FreeHGlobal(HandleInfoPtr);
            return HandlesInfo;
        }

        private static IntPtr NtQuerySystemInfoHelper(SYSTEM_INFORMATION_CLASS infoClass, uint infoLength = 0)
        {
            while (true)
            {
                var infoPtr = Marshal.AllocHGlobal((int)infoLength);
                uint outInfoLength;

                var result = NtDll_dll.NtQuerySystemInformation(infoClass, infoPtr, infoLength, out outInfoLength);

                if (result == NtStatus.Success)
                {
                    Debug.WriteLine("NtStatus: " + result);
                    return infoPtr;
                }
                else
                {
                    Marshal.FreeHGlobal(infoPtr);
                    if (result != NtStatus.InfoLengthMismatch && result != NtStatus.BufferOverflow && result != NtStatus.BufferTooSmall)
                    {
                        Debug.WriteLine("NtStatus result code: " + result);
                        return IntPtr.Zero;
                    }
                    infoLength = Math.Max(infoLength, outInfoLength);
                }
            }
        }

        private static string GetHandleFlag(int FlagSwitch)
        {
            Hashtable FlagSwitches = new Hashtable();
            FlagSwitches.Add(0, "NONE");
            FlagSwitches.Add(1, "PROTECTED_FROM_CLOSE");
            FlagSwitches.Add(2, "INHERIT");

            string HandleSwitch = (string)FlagSwitches[FlagSwitch];

            return HandleSwitch;
        }

        private static string GetObjectType(IntPtr SourceHandle, IntPtr TargetHandle, IntPtr SourceProcessHandle, IntPtr TargetProcessHandle)
        {
            IntPtr pObjectTypeInfo = NtQueryObjectHelper(TargetHandle, OBJECT_INFORMATION_CLASS.ObjectTypeInformation);
            OBJECT_TYPE_INFORMATION ObjectTypeInfo = (OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(pObjectTypeInfo, typeof(OBJECT_TYPE_INFORMATION));
            Marshal.FreeHGlobal(pObjectTypeInfo);

            return  ObjectTypeInfo.TypeName;
        }

        private static string GetObjectName(IntPtr SourceHandle, IntPtr TargetHandle, IntPtr SourceProcessHandle, IntPtr TargetProcessHandle)
        {
            string ObjectName = "Undefined";

            IntPtr pObjectNameInfo = IntPtr.Zero;
            try
            {
                pObjectNameInfo = NtQueryObjectHelper(TargetHandle, OBJECT_INFORMATION_CLASS.ObjectNameInformation);
                OBJECT_NAME_INFORMATION ObjectNameInfo = (OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(pObjectNameInfo, typeof(OBJECT_NAME_INFORMATION));
                if (ObjectNameInfo.Name != null)
                    ObjectName = ObjectNameInfo.Name;
            }
            catch (Exception e)
            {
                Console.WriteLine("NtQueryObject exception: {0}", e.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(pObjectNameInfo);
            }
            return ObjectName;
        }

        private static IntPtr DuplicateHandleHelper(IntPtr SourceProcessHandle, IntPtr SourceHandle, IntPtr TargetProcessHandle)
        {
            IntPtr TargetHandle = IntPtr.Zero;
            bool DuplicateHandleResult = Kernel32_dll.DuplicateHandle(SourceProcessHandle, SourceHandle, TargetProcessHandle, out TargetHandle, 0, false, DuplicateOptions.DUPLICATE_SAME_ACCESS);

            return TargetHandle;
        }

        private static IntPtr NtQueryObjectHelper(IntPtr TargetHandle, OBJECT_INFORMATION_CLASS ObjectInformationClass)
        {
            int ReturnLength = 0;

            NtDll_dll.NtQueryObject(TargetHandle, ObjectInformationClass, IntPtr.Zero, 0, ref ReturnLength);
            IntPtr ObjectInformation = Marshal.AllocHGlobal(ReturnLength);

            int NtQueryObjectResult = NtDll_dll.NtQueryObject(TargetHandle, ObjectInformationClass, ObjectInformation, ReturnLength, ref ReturnLength);
            if (NtQueryObjectResult != (int)NtStatus.Success)
                ObjectInformation = IntPtr.Zero;

            return ObjectInformation;        
        }

        private static string ConvertToRegularFileName(string RawFileName)
        {

            string[] LogicalDrives = Environment.GetLogicalDrives();
            foreach (string logicalDrive in LogicalDrives)
            {
                StringBuilder targetPath = new StringBuilder(256);
                uint resQueryDosDevice = Kernel32_dll.QueryDosDevice(logicalDrive.Substring(0, 2), targetPath, 256);
                string TargetPath = targetPath.ToString();
                if (resQueryDosDevice == 0)
                    return TargetPath;

                if (RawFileName.StartsWith(TargetPath))
                {
                    RawFileName = RawFileName.Replace(TargetPath, logicalDrive.Substring(0, 2));
                    break;
                }
            }
            return RawFileName;
        }

        private static uint GetLastSystemError()
        {
            uint LastError = Kernel32_dll.GetLastError();

            IntPtr MsgBuf = IntPtr.Zero;
            Kernel32_dll.FormatMessage((0x00000100 | 0x00001000 | 0x00000200), IntPtr.Zero, LastError, 0, ref MsgBuf, 0, IntPtr.Zero);
            string ErrMsg = Marshal.PtrToStringAnsi(MsgBuf);
            Debug.WriteLine("Handle error: " + LastError + " -- > " + ErrMsg);
           
            return LastError;
        }
    }

    class Cli
    {
        private static string fileName;
        public static string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (!File.Exists(value))
                    throw new FileNotFoundException("[!]Error. File is not found.", FileName);
                else
                    fileName = value;
            }
        }

        public static void Main(string[] args)
        {
            string FilePath = null;
            string ProcessName = null;
            int ProcessId = 0;

            Debug.WriteLine("IntPtr size: " + IntPtr.Size);

            if (args.Length == 0)
            {
                Console.WriteLine("You must specify at least one argument argument [-File] | [-Id] |");
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string CurrentArg = args[i].ToUpper();
                if (CurrentArg == "-FILE")
                    FilePath = args[i + 1];
                else if (CurrentArg == "-ID")
                {
                    bool ValidProcessId = int.TryParse(args[i + 1], out ProcessId);
                    if (ValidProcessId == false)
                    {
                        Console.WriteLine("Wrong Process ID. Please specify the right value");
                        return;
                    }
                    else
                        ProcessId = Int32.Parse(args[i + 1]);
                }
                else if (CurrentArg == "-NAME")
                    ProcessName = args[i + 1];                
            }
            if(FilePath != null)
                Cli.FileName = FilePath;

            if ((FilePath != null) && (ProcessId == 0) && (ProcessName == null))
                GetFileHandle(Cli.FileName);
            else if((FilePath == null) && (ProcessId != 0) && (ProcessName == null))
                GetHandles(ProcessId);
            else if ((FilePath != null) && (ProcessId != 0))
                GetFileHandle(Cli.FileName, ProcessId);
            else if ((FilePath != null) && (ProcessName != null))
                GetFileHandle(Cli.FileName, ProcessName);
        }

        private static void GetHandles(int ProcessId)
        {
            mProcessExec mProcess = new mProcessExec(ProcessId);
            ArrayList Handles = mProcess.GetProcessHandles();
            ShowHandlesInfo(Handles);
        }

        private static void GetFileHandle(string FilePath)
        { 
            Process[] Processes = Process.GetProcesses();
            FindFileHandle(Processes, FilePath);
        }

        private static void GetFileHandle(string FilePath, string ProcessName)
        {
            Process[] Processes = Process.GetProcessesByName(ProcessName);
            if (Processes.Length > 0)
                FindFileHandle(Processes, FilePath);
            else
                Console.WriteLine("No any process found with name [{0}]", ProcessName);
        }

        private static void GetFileHandle(string FilePath, int ProcessId)
        {
            Process[] Processes = new Process[1];
            Processes[0] = Process.GetProcessById(ProcessId);
            FindFileHandle(Processes, FilePath);
        }

        private static void FindFileHandle(Process[] Processes, string FilePath)
        {
            mProcessExec mProcess = new mProcessExec(Processes[0].Id);

            for (int i=0; i<Processes.Length; i++)
            {
                Process cProcess = Processes[i];
                if(i>0)
                    mProcess.ProcessId = cProcess.Id;
                Debug.WriteLine("Current process: " + cProcess.ProcessName + ", Id: " + cProcess.Id);

                ArrayList Handles = mProcess.GetProcessHandles();
                foreach (Hashtable HandleInfo in Handles)
                {
                    string hObjectName = (string)HandleInfo["ObjectName"];
                    if ((string)HandleInfo["ObjectType"] == "File")
                    {
                        Console.WriteLine("Using file: {0}",hObjectName);
                        Debug.WriteLine("Using file: " + hObjectName);
                    }
                    if (hObjectName == FilePath)
                    {
                        Console.WriteLine("File {0} is busy by process {1}, {2}", FilePath, cProcess.ProcessName, cProcess.Id);
                        break;
                    }
                }
            }
            Console.WriteLine("No [{0}] in processes found", FilePath);
        }
        
        private static void ShowHandlesInfo(ArrayList HandlesInfo)
        {
            foreach(Hashtable HandleInfo in HandlesInfo)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Handle object type number: {0}", HandleInfo["ObjectTypeNumber"]);
                Console.WriteLine("Handle object type: {0}", HandleInfo["ObjectType"]);
                Console.WriteLine("Handle object name: {0}", HandleInfo["ObjectName"]);
                Console.WriteLine("Handle flag: {0}", HandleInfo["HandleFlag"]);
                Console.WriteLine("Kernel pointer: {0}", HandleInfo["KernelPointer"]);
                Console.WriteLine("Access mask: {0}", HandleInfo["AccessMask"]);
            }
        }
    }
}
