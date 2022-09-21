﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace mProcessTool
{
    public enum NtStatus : uint
    {
        Success = 0x00000000,
        Wait0 = 0x00000000,
        Wait1 = 0x00000001,
        Wait2 = 0x00000002,
        Wait3 = 0x00000003,
        Wait63 = 0x0000003f,
        Abandoned = 0x00000080,
        AbandonedWait0 = 0x00000080,
        AbandonedWait1 = 0x00000081,
        AbandonedWait2 = 0x00000082,
        AbandonedWait3 = 0x00000083,
        AbandonedWait63 = 0x000000bf,
        UserApc = 0x000000c0,
        KernelApc = 0x00000100,
        Alerted = 0x00000101,
        Timeout = 0x00000102,
        Pending = 0x00000103,
        Reparse = 0x00000104,
        MoreEntries = 0x00000105,
        NotAllAssigned = 0x00000106,
        SomeNotMapped = 0x00000107,
        OpLockBreakInProgress = 0x00000108,
        VolumeMounted = 0x00000109,
        RxActCommitted = 0x0000010a,
        NotifyCleanup = 0x0000010b,
        NotifyEnumDir = 0x0000010c,
        NoQuotasForAccount = 0x0000010d,
        PrimaryTransportConnectFailed = 0x0000010e,
        PageFaultTransition = 0x00000110,
        PageFaultDemandZero = 0x00000111,
        PageFaultCopyOnWrite = 0x00000112,
        PageFaultGuardPage = 0x00000113,
        PageFaultPagingFile = 0x00000114,
        CrashDump = 0x00000116,
        ReparseObject = 0x00000118,
        NothingToTerminate = 0x00000122,
        ProcessNotInJob = 0x00000123,
        ProcessInJob = 0x00000124,
        ProcessCloned = 0x00000129,
        FileLockedWithOnlyReaders = 0x0000012a,
        FileLockedWithWriters = 0x0000012b,

        // Informational
        Informational = 0x40000000,
        ObjectNameExists = 0x40000000,
        ThreadWasSuspended = 0x40000001,
        WorkingSetLimitRange = 0x40000002,
        ImageNotAtBase = 0x40000003,
        RegistryRecovered = 0x40000009,

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
        AccessViolation = 0xc0000005,
        InPageError = 0xc0000006,
        PagefileQuota = 0xc0000007,
        InvalidHandle = 0xc0000008,
        BadInitialStack = 0xc0000009,
        BadInitialPc = 0xc000000a,
        InvalidCid = 0xc000000b,
        TimerNotCanceled = 0xc000000c,
        InvalidParameter = 0xc000000d,
        NoSuchDevice = 0xc000000e,
        NoSuchFile = 0xc000000f,
        InvalidDeviceRequest = 0xc0000010,
        EndOfFile = 0xc0000011,
        WrongVolume = 0xc0000012,
        NoMediaInDevice = 0xc0000013,
        NoMemory = 0xc0000017,
        NotMappedView = 0xc0000019,
        UnableToFreeVm = 0xc000001a,
        UnableToDeleteSection = 0xc000001b,
        IllegalInstruction = 0xc000001d,
        AlreadyCommitted = 0xc0000021,
        AccessDenied = 0xc0000022,
        BufferTooSmall = 0xc0000023,
        ObjectTypeMismatch = 0xc0000024,
        NonContinuableException = 0xc0000025,
        BadStack = 0xc0000028,
        NotLocked = 0xc000002a,
        NotCommitted = 0xc000002d,
        InvalidParameterMix = 0xc0000030,
        ObjectNameInvalid = 0xc0000033,
        ObjectNameNotFound = 0xc0000034,
        ObjectNameCollision = 0xc0000035,
        ObjectPathInvalid = 0xc0000039,
        ObjectPathNotFound = 0xc000003a,
        ObjectPathSyntaxBad = 0xc000003b,
        DataOverrun = 0xc000003c,
        DataLate = 0xc000003d,
        DataError = 0xc000003e,
        CrcError = 0xc000003f,
        SectionTooBig = 0xc0000040,
        PortConnectionRefused = 0xc0000041,
        InvalidPortHandle = 0xc0000042,
        SharingViolation = 0xc0000043,
        QuotaExceeded = 0xc0000044,
        InvalidPageProtection = 0xc0000045,
        MutantNotOwned = 0xc0000046,
        SemaphoreLimitExceeded = 0xc0000047,
        PortAlreadySet = 0xc0000048,
        SectionNotImage = 0xc0000049,
        SuspendCountExceeded = 0xc000004a,
        ThreadIsTerminating = 0xc000004b,
        BadWorkingSetLimit = 0xc000004c,
        IncompatibleFileMap = 0xc000004d,
        SectionProtection = 0xc000004e,
        EasNotSupported = 0xc000004f,
        EaTooLarge = 0xc0000050,
        NonExistentEaEntry = 0xc0000051,
        NoEasOnFile = 0xc0000052,
        EaCorruptError = 0xc0000053,
        FileLockConflict = 0xc0000054,
        LockNotGranted = 0xc0000055,
        DeletePending = 0xc0000056,
        CtlFileNotSupported = 0xc0000057,
        UnknownRevision = 0xc0000058,
        RevisionMismatch = 0xc0000059,
        InvalidOwner = 0xc000005a,
        InvalidPrimaryGroup = 0xc000005b,
        NoImpersonationToken = 0xc000005c,
        CantDisableMandatory = 0xc000005d,
        NoLogonServers = 0xc000005e,
        NoSuchLogonSession = 0xc000005f,
        NoSuchPrivilege = 0xc0000060,
        PrivilegeNotHeld = 0xc0000061,
        InvalidAccountName = 0xc0000062,
        UserExists = 0xc0000063,
        NoSuchUser = 0xc0000064,
        GroupExists = 0xc0000065,
        NoSuchGroup = 0xc0000066,
        MemberInGroup = 0xc0000067,
        MemberNotInGroup = 0xc0000068,
        LastAdmin = 0xc0000069,
        WrongPassword = 0xc000006a,
        IllFormedPassword = 0xc000006b,
        PasswordRestriction = 0xc000006c,
        LogonFailure = 0xc000006d,
        AccountRestriction = 0xc000006e,
        InvalidLogonHours = 0xc000006f,
        InvalidWorkstation = 0xc0000070,
        PasswordExpired = 0xc0000071,
        AccountDisabled = 0xc0000072,
        NoneMapped = 0xc0000073,
        TooManyLuidsRequested = 0xc0000074,
        LuidsExhausted = 0xc0000075,
        InvalidSubAuthority = 0xc0000076,
        InvalidAcl = 0xc0000077,
        InvalidSid = 0xc0000078,
        InvalidSecurityDescr = 0xc0000079,
        ProcedureNotFound = 0xc000007a,
        InvalidImageFormat = 0xc000007b,
        NoToken = 0xc000007c,
        BadInheritanceAcl = 0xc000007d,
        RangeNotLocked = 0xc000007e,
        DiskFull = 0xc000007f,
        ServerDisabled = 0xc0000080,
        ServerNotDisabled = 0xc0000081,
        TooManyGuidsRequested = 0xc0000082,
        GuidsExhausted = 0xc0000083,
        InvalidIdAuthority = 0xc0000084,
        AgentsExhausted = 0xc0000085,
        InvalidVolumeLabel = 0xc0000086,
        SectionNotExtended = 0xc0000087,
        NotMappedData = 0xc0000088,
        ResourceDataNotFound = 0xc0000089,
        ResourceTypeNotFound = 0xc000008a,
        ResourceNameNotFound = 0xc000008b,
        ArrayBoundsExceeded = 0xc000008c,
        FloatDenormalOperand = 0xc000008d,
        FloatDivideByZero = 0xc000008e,
        FloatInexactResult = 0xc000008f,
        FloatInvalidOperation = 0xc0000090,
        FloatOverflow = 0xc0000091,
        FloatStackCheck = 0xc0000092,
        FloatUnderflow = 0xc0000093,
        IntegerDivideByZero = 0xc0000094,
        IntegerOverflow = 0xc0000095,
        PrivilegedInstruction = 0xc0000096,
        TooManyPagingFiles = 0xc0000097,
        FileInvalid = 0xc0000098,
        InstanceNotAvailable = 0xc00000ab,
        PipeNotAvailable = 0xc00000ac,
        InvalidPipeState = 0xc00000ad,
        PipeBusy = 0xc00000ae,
        IllegalFunction = 0xc00000af,
        PipeDisconnected = 0xc00000b0,
        PipeClosing = 0xc00000b1,
        PipeConnected = 0xc00000b2,
        PipeListening = 0xc00000b3,
        InvalidReadMode = 0xc00000b4,
        IoTimeout = 0xc00000b5,
        FileForcedClosed = 0xc00000b6,
        ProfilingNotStarted = 0xc00000b7,
        ProfilingNotStopped = 0xc00000b8,
        NotSameDevice = 0xc00000d4,
        FileRenamed = 0xc00000d5,
        CantWait = 0xc00000d8,
        PipeEmpty = 0xc00000d9,
        CantTerminateSelf = 0xc00000db,
        InternalError = 0xc00000e5,
        InvalidParameter1 = 0xc00000ef,
        InvalidParameter2 = 0xc00000f0,
        InvalidParameter3 = 0xc00000f1,
        InvalidParameter4 = 0xc00000f2,
        InvalidParameter5 = 0xc00000f3,
        InvalidParameter6 = 0xc00000f4,
        InvalidParameter7 = 0xc00000f5,
        InvalidParameter8 = 0xc00000f6,
        InvalidParameter9 = 0xc00000f7,
        InvalidParameter10 = 0xc00000f8,
        InvalidParameter11 = 0xc00000f9,
        InvalidParameter12 = 0xc00000fa,
        MappedFileSizeZero = 0xc000011e,
        TooManyOpenedFiles = 0xc000011f,
        Cancelled = 0xc0000120,
        CannotDelete = 0xc0000121,
        InvalidComputerName = 0xc0000122,
        FileDeleted = 0xc0000123,
        SpecialAccount = 0xc0000124,
        SpecialGroup = 0xc0000125,
        SpecialUser = 0xc0000126,
        MembersPrimaryGroup = 0xc0000127,
        FileClosed = 0xc0000128,
        TooManyThreads = 0xc0000129,
        ThreadNotInProcess = 0xc000012a,
        TokenAlreadyInUse = 0xc000012b,
        PagefileQuotaExceeded = 0xc000012c,
        CommitmentLimit = 0xc000012d,
        InvalidImageLeFormat = 0xc000012e,
        InvalidImageNotMz = 0xc000012f,
        InvalidImageProtect = 0xc0000130,
        InvalidImageWin16 = 0xc0000131,
        LogonServer = 0xc0000132,
        DifferenceAtDc = 0xc0000133,
        SynchronizationRequired = 0xc0000134,
        DllNotFound = 0xc0000135,
        IoPrivilegeFailed = 0xc0000137,
        OrdinalNotFound = 0xc0000138,
        EntryPointNotFound = 0xc0000139,
        ControlCExit = 0xc000013a,
        PortNotSet = 0xc0000353,
        DebuggerInactive = 0xc0000354,
        CallbackBypass = 0xc0000503,
        PortClosed = 0xc0000700,
        MessageLost = 0xc0000701,
        InvalidMessage = 0xc0000702,
        RequestCanceled = 0xc0000703,
        RecursiveDispatch = 0xc0000704,
        LpcReceiveBufferExpected = 0xc0000705,
        LpcInvalidConnectionUsage = 0xc0000706,
        LpcRequestsNotAllowed = 0xc0000707,
        ResourceInUse = 0xc0000708,
        ProcessIsProtected = 0xc0000712,
        VolumeDirty = 0xc0000806,
        FileCheckedOut = 0xc0000901,
        CheckOutRequired = 0xc0000902,
        BadFileType = 0xc0000903,
        FileTooLarge = 0xc0000904,
        FormsAuthRequired = 0xc0000905,
        VirusInfected = 0xc0000906,
        VirusDeleted = 0xc0000907,
        TransactionalConflict = 0xc0190001,
        InvalidTransaction = 0xc0190002,
        TransactionNotActive = 0xc0190003,
        TmInitializationFailed = 0xc0190004,
        RmNotActive = 0xc0190005,
        RmMetadataCorrupt = 0xc0190006,
        TransactionNotJoined = 0xc0190007,
        DirectoryNotRm = 0xc0190008,
        CouldNotResizeLog = 0xc0190009,
        TransactionsUnsupportedRemote = 0xc019000a,
        LogResizeInvalidSize = 0xc019000b,
        RemoteFileVersionMismatch = 0xc019000c,
        CrmProtocolAlreadyExists = 0xc019000f,
        TransactionPropagationFailed = 0xc0190010,
        CrmProtocolNotFound = 0xc0190011,
        TransactionSuperiorExists = 0xc0190012,
        TransactionRequestNotValid = 0xc0190013,
        TransactionNotRequested = 0xc0190014,
        TransactionAlreadyAborted = 0xc0190015,
        TransactionAlreadyCommitted = 0xc0190016,
        TransactionInvalidMarshallBuffer = 0xc0190017,
        CurrentTransactionNotValid = 0xc0190018,
        LogGrowthFailed = 0xc0190019,
        ObjectNoLongerExists = 0xc0190021,
        StreamMiniversionNotFound = 0xc0190022,
        StreamMiniversionNotValid = 0xc0190023,
        MiniversionInaccessibleFromSpecifiedTransaction = 0xc0190024,
        CantOpenMiniversionWithModifyIntent = 0xc0190025,
        CantCreateMoreStreamMiniversions = 0xc0190026,
        HandleNoLongerValid = 0xc0190028,
        NoTxfMetadata = 0xc0190029,
        LogCorruptionDetected = 0xc0190030,
        CantRecoverWithHandleOpen = 0xc0190031,
        RmDisconnected = 0xc0190032,
        EnlistmentNotSuperior = 0xc0190033,
        RecoveryNotNeeded = 0xc0190034,
        RmAlreadyStarted = 0xc0190035,
        FileIdentityNotPersistent = 0xc0190036,
        CantBreakTransactionalDependency = 0xc0190037,
        CantCrossRmBoundary = 0xc0190038,
        TxfDirNotEmpty = 0xc0190039,
        IndoubtTransactionsExist = 0xc019003a,
        TmVolatile = 0xc019003b,
        RollbackTimerExpired = 0xc019003c,
        TxfAttributeCorrupt = 0xc019003d,
        EfsNotAllowedInTransaction = 0xc019003e,
        TransactionalOpenNotAllowed = 0xc019003f,
        TransactedMappingUnsupportedRemote = 0xc0190040,
        TxfMetadataAlreadyPresent = 0xc0190041,
        TransactionScopeCallbacksNotSet = 0xc0190042,
        TransactionRequiredPromotion = 0xc0190043,
        CannotExecuteFileInTransaction = 0xc0190044,
        TransactionsNotFrozen = 0xc0190045,

        MaximumNtStatus = 0xffffffff
    }

    public enum SYSTEM_INFORMATION_CLASS
    {
        SystemBasicInformation = 0x0000,
        SystemProcessorInformation = 0x0001,
        SystemPerformanceInformation = 0x0002,
        SystemTimeOfDayInformation = 0x0003,
        SystemPathInformation = 0x0004,
        SystemProcessInformation = 0x0005,
        SystemCallCountInformation = 0x0006,
        SystemDeviceInformation = 0x0007,
        SystemProcessorPerformanceInformation = 0x0008,
        SystemFlagsInformation = 0x0009,
        SystemCallTimeInformation = 0x000A,
        SystemModuleInformation = 0x000B,
        SystemLocksInformation = 0x000C,
        SystemStackTraceInformation = 0x000D,
        SystemPagedPoolInformation = 0x000E,
        SystemNonPagedPoolInformation = 0x000F,
        SystemHandleInformation = 0x0010,
        SystemObjectInformation = 0x0011,
        SystemPageFileInformation = 0x0012,
        SystemVdmInstemulInformation = 0x0013,
        SystemVdmBopInformation = 0x0014,
        SystemFileCacheInformation = 0x0015,
        SystemPoolTagInformation = 0x0016,
        SystemInterruptInformation = 0x0017,
        SystemDpcBehaviorInformation = 0x0018,
        SystemFullMemoryInformation = 0x0019,
        SystemLoadGdiDriverInformation = 0x001A,
        SystemUnloadGdiDriverInformation = 0x001B,
        SystemTimeAdjustmentInformation = 0x001C,
        SystemSummaryMemoryInformation = 0x001D,
        SystemMirrorMemoryInformation = 0x001E,
        SystemPerformanceTraceInformation = 0x001F,
        SystemCrashDumpInformation = 0x0020,
        SystemExceptionInformation = 0x0021,
        SystemCrashDumpStateInformation = 0x0022,
        SystemKernelDebuggerInformation = 0x0023,
        SystemContextSwitchInformation = 0x0024,
        SystemRegistryQuotaInformation = 0x0025,
        SystemExtendServiceTableInformation = 0x0026,
        SystemPrioritySeperation = 0x0027,
        SystemVerifierAddDriverInformation = 0x0028,
        SystemVerifierRemoveDriverInformation = 0x0029,
        SystemProcessorIdleInformation = 0x002A,
        SystemLegacyDriverInformation = 0x002B,
        SystemCurrentTimeZoneInformation = 0x002C,
        SystemLookasideInformation = 0x002D,
        SystemTimeSlipNotification = 0x002E,
        SystemSessionCreate = 0x002F,
        SystemSessionDetach = 0x0030,
        SystemSessionInformation = 0x0031,
        SystemRangeStartInformation = 0x0032,
        SystemVerifierInformation = 0x0033,
        SystemVerifierThunkExtend = 0x0034,
        SystemSessionProcessInformation = 0x0035,
        SystemLoadGdiDriverInSystemSpace = 0x0036,
        SystemNumaProcessorMap = 0x0037,
        SystemPrefetcherInformation = 0x0038,
        SystemExtendedProcessInformation = 0x0039,
        SystemRecommendedSharedDataAlignment = 0x003A,
        SystemComPlusPackage = 0x003B,
        SystemNumaAvailableMemory = 0x003C,
        SystemProcessorPowerInformation = 0x003D,
        SystemEmulationBasicInformation = 0x003E,
        SystemEmulationProcessorInformation = 0x003F,
        SystemExtendedHandleInformation = 0x0040,
        SystemLostDelayedWriteInformation = 0x0041,
        SystemBigPoolInformation = 0x0042,
        SystemSessionPoolTagInformation = 0x0043,
        SystemSessionMappedViewInformation = 0x0044,
        SystemHotpatchInformation = 0x0045,
        SystemObjectSecurityMode = 0x0046,
        SystemWatchdogTimerHandler = 0x0047,
        SystemWatchdogTimerInformation = 0x0048,
        SystemLogicalProcessorInformation = 0x0049,
        SystemWow64SharedInformationObsolete = 0x004A,
        SystemRegisterFirmwareTableInformationHandler = 0x004B,
        SystemFirmwareTableInformation = 0x004C,
        SystemModuleInformationEx = 0x004D,
        SystemVerifierTriageInformation = 0x004E,
        SystemSuperfetchInformation = 0x004F,
        SystemMemoryListInformation = 0x0050, // SYSTEM_MEMORY_LIST_INFORMATION
        SystemFileCacheInformationEx = 0x0051,
        SystemThreadPriorityClientIdInformation = 0x0052,
        SystemProcessorIdleCycleTimeInformation = 0x0053,
        SystemVerifierCancellationInformation = 0x0054,
        SystemProcessorPowerInformationEx = 0x0055,
        SystemRefTraceInformation = 0x0056,
        SystemSpecialPoolInformation = 0x0057,
        SystemProcessIdInformation = 0x0058,
        SystemErrorPortInformation = 0x0059,
        SystemBootEnvironmentInformation = 0x005A,
        SystemHypervisorInformation = 0x005B,
        SystemVerifierInformationEx = 0x005C,
        SystemTimeZoneInformation = 0x005D,
        SystemImageFileExecutionOptionsInformation = 0x005E,
        SystemCoverageInformation = 0x005F,
        SystemPrefetchPatchInformation = 0x0060,
        SystemVerifierFaultsInformation = 0x0061,
        SystemSystemPartitionInformation = 0x0062,
        SystemSystemDiskInformation = 0x0063,
        SystemProcessorPerformanceDistribution = 0x0064,
        SystemNumaProximityNodeInformation = 0x0065,
        SystemDynamicTimeZoneInformation = 0x0066,
        SystemCodeIntegrityInformation = 0x0067,
        SystemProcessorMicrocodeUpdateInformation = 0x0068,
        SystemProcessorBrandString = 0x0069,
        SystemVirtualAddressInformation = 0x006A,
        SystemLogicalProcessorAndGroupInformation = 0x006B,
        SystemProcessorCycleTimeInformation = 0x006C,
        SystemStoreInformation = 0x006D,
        SystemRegistryAppendString = 0x006E,
        SystemAitSamplingValue = 0x006F,
        SystemVhdBootInformation = 0x0070,
        SystemCpuQuotaInformation = 0x0071,
        SystemNativeBasicInformation = 0x0072,
        SystemErrorPortTimeouts = 0x0073,
        SystemLowPriorityIoInformation = 0x0074,
        SystemBootEntropyInformation = 0x0075,
        SystemVerifierCountersInformation = 0x0076,
        SystemPagedPoolInformationEx = 0x0077,
        SystemSystemPtesInformationEx = 0x0078,
        SystemNodeDistanceInformation = 0x0079,
        SystemAcpiAuditInformation = 0x007A,
        SystemBasicPerformanceInformation = 0x007B,
        SystemQueryPerformanceCounterInformation = 0x007C,
        SystemSessionBigPoolInformation = 0x007D,
        SystemBootGraphicsInformation = 0x007E,
        SystemScrubPhysicalMemoryInformation = 0x007F,
        SystemBadPageInformation = 0x0080,
        SystemProcessorProfileControlArea = 0x0081,
        SystemCombinePhysicalMemoryInformation = 0x0082,
        SystemEntropyInterruptTimingInformation = 0x0083,
        SystemConsoleInformation = 0x0084,
        SystemPlatformBinaryInformation = 0x0085,
        SystemThrottleNotificationInformation = 0x0086,
        SystemHypervisorProcessorCountInformation = 0x0087,
        SystemDeviceDataInformation = 0x0088,
        SystemDeviceDataEnumerationInformation = 0x0089,
        SystemMemoryTopologyInformation = 0x008A,
        SystemMemoryChannelInformation = 0x008B,
        SystemBootLogoInformation = 0x008C,
        SystemProcessorPerformanceInformationEx = 0x008D,
        SystemSpare0 = 0x008E,
        SystemSecureBootPolicyInformation = 0x008F,
        SystemPageFileInformationEx = 0x0090,
        SystemSecureBootInformation = 0x0091,
        SystemEntropyInterruptTimingRawInformation = 0x0092,
        SystemPortableWorkspaceEfiLauncherInformation = 0x0093,
        SystemFullProcessInformation = 0x0094,
        MaxSystemInfoClass = 0x0095
    }

    public enum OBJECT_INFORMATION_CLASS
    {
        ObjectBasicInformation = 0,
        ObjectNameInformation = 1,
        ObjectTypeInformation = 2,
        ObjectAllTypesInformation = 3,
        ObjectHandleInformation = 4,
        ObjectSessionInformation = 5
    }

    enum FileType : uint
    {
        FileTypeChar = 0x0002,
        FileTypeDisk = 0x0001,
        FileTypePipe = 0x0003,
        FileTypeRemote = 0x8000,
        FileTypeUnknown = 0x0000,
        FileTypeUndefined = 0x1000
    }

    public enum DuplicateOptions : uint
    {
        DUPLICATE_CLOSE_SOURCE = (0x00000001), // Closes the source handle. This occurs regardless of any error status returned.
        DUPLICATE_SAME_ACCESS = (0x00000002)   //Ignores the dwDesiredAccess parameter. The duplicate handle has the same access as the source handle.
    }

    public struct OBJECT_NAME_INFORMATION
    {
        private UNICODE_STRING name;
        public string Name
        {
            get
            {
                return name.ToString();
            }
        }
    }

    public struct OBJECT_TYPE_INFORMATION
    {
        private UNICODE_STRING name;
        public string TypeName
        {
            get
            {
                return name.ToString();
            }
        }

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

    [StructLayout(LayoutKind.Sequential)]
    public struct UNICODE_STRING : IDisposable
    {
        public ushort Length;
        public ushort MaximumLength;
        private IntPtr buffer;

        public UNICODE_STRING(string s)
        {
            Length = (ushort)(s.Length * 2);
            MaximumLength = (ushort)(Length + 2);
            buffer = Marshal.StringToHGlobalUni(s);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(buffer);
            buffer = IntPtr.Zero;
        }

        public override string ToString()
        {
            return Marshal.PtrToStringUni(buffer);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_HANDLE_INFORMATION
    {
        public UInt32 ProcessId;
        public Byte ObjectTypeNumber;
        public Byte Flags;
        public UInt16 HandleValue;
        public IntPtr Object_Pointer;
        public UInt32 GrantedAccess;
    }
}
