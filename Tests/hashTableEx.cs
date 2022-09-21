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