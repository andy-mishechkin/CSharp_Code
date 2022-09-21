using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;
 
namespace Detector
{
    public class DetectOpenFilesEx
    {
        private static Dictionary<byte, string> typeHandles = new Dictionary<byte, string>();
        private static Dictionary<string, string> _fileNamePrefixes = new Dictionary<string, string>();
 
        public DetectOpenFilesEx()
        {
            RefreshFileNamePrefixes();
        }
 
        public FileInfo[] GetOpenedFiles(Process targetProcess)
        {
            Process currentProcess = Process.GetCurrentProcess();
 
            var files = new List<FileInfo>();
            var handles = GetAllHandles();
 
            foreach (var handleEntry in handles)
            {
 
                if (handleEntry.ProcessId < 0)
                    throw new Exception("Process Id < 0");
 
                if (handleEntry.GrantedAccess == 0x0012019F)
                    continue;
 
                if (handleEntry.ProcessId != targetProcess.Id)
                    continue;
 
                string type;
                if (typeHandles.ContainsKey(handleEntry.ObjectTypeNumber))
                    type = typeHandles[handleEntry.ObjectTypeNumber];
                else
                {
                    type = GetHandleType(handleEntry.Handle, currentProcess, targetProcess);
                    typeHandles.Add(handleEntry.ObjectTypeNumber, type);
                }
 
                if (String.Equals("File", type, StringComparison.InvariantCultureIgnoreCase))
                {
                    string devicePath = GetFileNameFromHandle(handleEntry.Handle, currentProcess, targetProcess);
 
                    if (!string.IsNullOrEmpty(devicePath))
                    {
                        string goodName = ConvertDevicePathToDosPath(devicePath, false);
                        files.Add(new FileInfo(goodName));
                    }
                }
            }
            currentProcess.Close();
 
            return files.ToArray();
        }
 
        public SystemHandleEntry[] GetAllHandles()
        {
            IntPtr ptr = IntPtr.Zero;
            int length = 65536;
            SystemHandleInformation handles = new SystemHandleInformation();
            NtStatus ret;
            do
            {
                try
                {
                    RuntimeHelpers.PrepareConstrainedRegions();
                    try
                    { }
                    finally
                    {
                        ptr = Marshal.AllocHGlobal(length);
                    }
 
                    int returnLength;
                    ret = NativeMethods.NtQuerySystemInformation(
                        SystemInformationClass.SystemHandleInformation, ptr, length, out returnLength);
 
                    if (ret == NtStatus.InfoLengthMismatch)
                    {
                        length = ((returnLength + 0xffff) & ~0xffff);
                    }
                    else if (ret == NtStatus.Success)
                    {
                        {
                            Type typeHandleEntry = typeof(SystemHandleEntry);
 
                            int handleCount = Marshal.ReadIntPtr(ptr).ToInt32();
                            int sizeStruct = Marshal.SizeOf(typeof(SystemHandleEntry));
 
                            handles.NumberOfHandles = handleCount;
                            handles.handles = new SystemHandleEntry[handleCount];
 
                            Int64 pointer = ptr.ToInt32() + IntPtr.Size;
 
                            for (int i = 0; i < handleCount; i++)
                            {
                                handles.handles[i] = (SystemHandleEntry)Marshal.PtrToStructure(
                                    new IntPtr(pointer + sizeStruct * i),
                                    typeHandleEntry);
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            } while (ret == NtStatus.InfoLengthMismatch);
 
            return handles.handles;
        }
 
        private static void RefreshFileNamePrefixes()
        {
            const int MAX_PATH = 254;
            // Just create a new dictionary to avoid having to lock the existing one.
            var newPrefixes = new Dictionary<string, string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                var lpTargetPath = new StringBuilder(MAX_PATH);
 
                if (NativeMethods.QueryDosDevice(c + ":", lpTargetPath, MAX_PATH) > 2)
                {
                    newPrefixes.Add(lpTargetPath.ToString(), c + ":");
                }
            }
 
            _fileNamePrefixes = newPrefixes;
        }
 
        public string GetFileNameFromHandle(IntPtr fileHandle, Process currProc, Process targetProc)
        {
            SafeObjectHandle safeFileHandle = null;
 
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                bool duplOk = NativeMethods.DuplicateHandle(targetProc.Handle,
                                                            fileHandle, currProc.Handle, out safeFileHandle,
                                                            0, false, DuplicateHandleOptions.DUPLICATE_SAME_ACCESS);
 
                if (!duplOk || safeFileHandle == null)
                    return null;
 
                var nameInfo = NtQueryObject<ObjectNameInformation>(safeFileHandle.DangerousGetHandle(),
                                                        ObjectInformationClass.ObjectNameInformation);
 
                return nameInfo.Name;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                if (safeFileHandle != null)
                    safeFileHandle.Close();
            }
        }
 
 
        public string GetHandleType(IntPtr handle, Process currProc, Process targetProc)
        {
            SafeObjectHandle objectHandle = null;
            try
            {
                bool duplOk = NativeMethods.DuplicateHandle(targetProc.Handle,
                                                            handle, currProc.Handle, out objectHandle, 0,
                                                            false, DuplicateHandleOptions.DUPLICATE_SAME_ACCESS);
                if (duplOk)
                {
                    var typeInfo = NtQueryObject<ObjectTypeInformation>(
                        objectHandle.DangerousGetHandle(),
                        ObjectInformationClass.ObjectTypeInformation);
 
                    return typeInfo.TypeName;
                }
            }
            finally
            {
                if (objectHandle != null)
                    objectHandle.Close();
            }
 
            return "UNKNOWN";
        }
 
 
        private static T NtQueryObject<T>(IntPtr objectHandle, ObjectInformationClass typeInfo)
        {
            int leght = 512;
            IntPtr handleInfoPtr = IntPtr.Zero;
 
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                RuntimeHelpers.PrepareConstrainedRegions();
                try
                { }
                finally
                {
                    handleInfoPtr = Marshal.AllocHGlobal(leght);
                }
 
                NtStatus ret = NativeMethods.NtQueryObject(objectHandle, typeInfo, handleInfoPtr,
                    leght, out leght);
 
                if (ret == NtStatus.BufferOverflow || ret == NtStatus.InfoLengthMismatch)
                {
                    RuntimeHelpers.PrepareConstrainedRegions();
                    try
                    { }
                    finally
                    {
                        handleInfoPtr = Marshal.ReAllocHGlobal(handleInfoPtr, new IntPtr(leght));
                    }
 
                    ret = NativeMethods.NtQueryObject(objectHandle, typeInfo, handleInfoPtr,
                        leght, out leght);
                }
 
                if (ret < NtStatus.Error)
                {
                    return (T)Marshal.PtrToStructure(handleInfoPtr, typeof(T));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(handleInfoPtr);
            }
 
            return default(T);
        }
 
 
        private static string ConvertDevicePathToDosPath(string fileName, bool canonicalize)
        {
            bool alreadyCanonicalized = false;
 
            // If the path starts with "\SystemRoot", we can replace it with C:\ (or whatever it is).
            if (fileName.StartsWith("\\systemroot", StringComparison.OrdinalIgnoreCase))
            {
                fileName = Path.GetFullPath(string.Format("{0}\\..{1}", Environment.SystemDirectory, fileName.Substring(11)));
                alreadyCanonicalized = true;
            }
            // If the path starts with "\??\", we can remove it and we will have the path.
            else if (fileName.StartsWith("\\??\\"))
            {
                fileName = fileName.Substring(4);
            }
 
            // If the path still starts with a backslash, we probably need to 
            // resolve any native object name to a DOS drive letter.)
            if (fileName.StartsWith("\\"))
            {
                var prefixes = _fileNamePrefixes;
 
                foreach (var pair in prefixes)
                {
                    if (fileName.StartsWith(pair.Key + "\\"))
                    {
                        fileName = pair.Value + "\\" + fileName.Substring(pair.Key.Length + 1);
                        break;
                    }
 
                    if (fileName == pair.Key)
                    {
                        fileName = pair.Value;
                        break;
                    }
                }
            }
 
            if (canonicalize && !alreadyCanonicalized)
                fileName = Path.GetFullPath(fileName);
 
            return fileName;
        }
 
 
 
 
 
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        private sealed class SafeObjectHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            // ReSharper disable UnusedMember.Local
            private SafeObjectHandle()
                // ReSharper restore UnusedMember.Local
                : base(true)
            { }
 
            internal SafeObjectHandle(IntPtr preexistingHandle, bool ownsHandle)
                : base(ownsHandle)
            {
                SetHandle(preexistingHandle);
            }
 
            protected override bool ReleaseHandle()
            {
                return NativeMethods.CloseHandle(handle);
            }
        }
 
        #region enums
 
        private enum SystemInformationClass
        {
            SystemBasicInformation = 0,
            SystemPerformanceInformation = 2,
            SystemTimeOfDayInformation = 3,
            SystemProcessInformation = 5,
            SystemProcessorPerformanceInformation = 8,
            SystemHandleInformation = 16,
            SystemInterruptInformation = 23,
            SystemExceptionInformation = 33,
            SystemRegistryQuotaInformation = 37,
            SystemLookasideInformation = 45
        }
 
        private enum DuplicateHandleOptions
        {
            DUPLICATE_CLOSE_SOURCE = 0x1,
            DUPLICATE_SAME_ACCESS = 0x2
        }
 
        private enum ObjectInformationClass
        {
            ObjectBasicInformation = 0,
            ObjectNameInformation = 1,
            ObjectTypeInformation = 2,
            ObjectTypesInformation = 3,
            ObjectHandleFlagInformation = 4,
            ObjectSessionInformation = 5
        }
 
        private enum NtStatus : uint
        {
            // Success
            Success = 0x00000000,
 
            // Warning
            Warning = 0x80000000,
            GuardPageViolation = 0x80000001,
            DatatypeMisalignment = 0x80000002,
            Breakpoint = 0x80000003,
            SingleStep = 0x80000004,
            BufferOverflow = 0x80000005,
            NoMoreFiles = 0x80000006,
            HandlesClosed = 0x8000000a,
            PartialCopy = 0x8000000d,
            DeviceBusy = 0x80000011,
            InvalidEaName = 0x80000013,
            EaListInconsistent = 0x80000014,
            NoMoreEntries = 0x8000001a,
            LongJump = 0x80000026,
            DllMightBeInsecure = 0x8000002b,
 
            // Error
            Error = 0xc0000000,
            Unsuccessful = 0xc0000001,
            NotImplemented = 0xc0000002,
            InvalidInfoClass = 0xc0000003,
            InfoLengthMismatch = 0xc0000004,
        }
 
        #endregion
 
        #region structs
 
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemHandleEntry
        {
            public int ProcessId;
            public byte ObjectTypeNumber;
            public byte Flags;
            private short handleValue;
            public IntPtr ObjectPointer;
            public int GrantedAccess;
 
            public IntPtr Handle
            {
                get { return new IntPtr(handleValue); }
            }
        }
 
        [StructLayout(LayoutKind.Sequential)]
        private struct SystemHandleInformation
        {
            public int NumberOfHandles;
            public SystemHandleEntry[] handles;
        }
 
        [StructLayout(LayoutKind.Sequential)]
        private struct ObjectNameInformation
        {
            private UnicodeString name;
            public string Name
            { get { return name.Text; } }
        }
 
        [StructLayout(LayoutKind.Sequential)]
        private struct UnicodeString
        {
            public short Length;
            public short MaximumLength;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Text;
        }
 
        [StructLayout(LayoutKind.Sequential)]
        private struct ObjectTypeInformation
        {
            private UnicodeString name;
            public string TypeName
            { get { return name.Text; } }
 
            public int TotalNumberOfObjects;
            public int TotalNumberOfHandles;
            public int TotalPagedPoolUsage;
            public int TotalNonPagedPoolUsage;
            public int TotalNamePoolUsage;
            public int TotalHandleTableUsage;
            public int HighWaterNumberOfObjects;
            public int HighWaterNumberOfHandles;
            public int HighWaterPagedPoolUsage;
            public int HighWaterNonPagedPoolUsage;
            public int HighWaterNamePoolUsage;
            public int HighWaterHandleTableUsage;
            public int InvalidAttributes;
            public int GenericRead;
            public int GenericWrite;
            public int GenericExecute;
            public int GenericAll;
            public int ValidAccess;
            public byte SecurityRequired;
            public byte MaintainHandleCount;
            public ushort MaintainTypeList;
            public uint PoolType;
            public int PagedPoolUsage;
            public int NonPagedPoolUsage;
        }
 
 
        #endregion
 
        #region nativeMethods
 
        private static class NativeMethods
        {
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseHandle(
                [In] IntPtr hObject);
 
            [DllImport("ntdll.dll")]
            internal static extern NtStatus NtQuerySystemInformation(
                [In] SystemInformationClass SystemInformationClass,
                [In] IntPtr SystemInformation,
                [In] int SystemInformationLength,
                [Out] out int ReturnLength);
 
            [DllImport("ntdll.dll")]
            internal static extern NtStatus NtQueryObject(
                [In] IntPtr Handle,
                [In] ObjectInformationClass ObjectInformationClass,
                [In] IntPtr ObjectInformation,
                [In] int ObjectInformationLength,
                [Out] out int ReturnLength);
 
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DuplicateHandle(
                [In] IntPtr hSourceProcessHandle,
                [In] IntPtr hSourceHandle,
                [In] IntPtr hTargetProcessHandle,
                [Out] out SafeObjectHandle lpTargetHandle,
                [In] int dwDesiredAccess,
                [In, MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
                [In] DuplicateHandleOptions dwOptions);
 
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern int QueryDosDevice(
                [In] string lpDeviceName,
                [Out] StringBuilder lpTargetPath,
                [In] int ucchMax);
        }
 
        #endregion
    }
}