private static string GetWindowsVersion()
{
	OperatingSystem OSVersion = Environment.OSVersion;
	return OSVersion.Version.ToString();
}

private static string GetOldObjectType(int ObjectTypeNumber, string WindowsVersion)
{
	string ObjectName = null;

	if (WindowsVersion.Contains("10.0"))
		ObjectName = Enum.GetName(typeof(Windows10_Kernel_Objects), ObjectTypeNumber);
	else if (WindowsVersion.Contains("6.2"))
		ObjectName = Enum.GetName(typeof(Windows8_Kernel_Objects), ObjectTypeNumber);
	else if (WindowsVersion.Contains("6.1"))
		ObjectName = Enum.GetName(typeof(Windows7_Kernel_Objects), ObjectTypeNumber);
	else if (WindowsVersion.Contains("6.0"))
		ObjectName = Enum.GetName(typeof(WindowsVista_Kernel_Objects), ObjectTypeNumber);

	return ObjectName;
}

public enum Windows10_Kernel_Objects
{
	Directory = 0x03,
	SymbolicLink = 0x04,
	Token = 0x05,
	Process = 0x07,
	Thread = 0x08,
	Event = 0x0D,
	Mutant = 0x0E,
	Semaphore = 0x10,
	Timer = 0x11,
	IRTimer = 0x12,
	WindowStation = 0x15,
	Desktop = 0x16,
	Composition = 0x17,
	RawInputManager = 0x18,
	TpWorkerFactory = 0x19,
	IoCompletion = 0x1E,
	WaitCompletionPacket = 0x1F,
	File = 0x20,
	TmTm = 0x21,
	TmTx = 0x22,
	TmRm = 0x23,
	TmEn = 0x24,
	Section = 0x25,
	Session = 0x26,
	Partition = 0x27,
	Key = 0x28,
	ALPCPort = 0x29,
	EtwRegistration = 0x2C,
	DmaDomain = 0x2F,
	FilterConnectionPort = 0x31
}

public enum Windows8_Kernel_Objects
{
	Type = 0x02,
	Directory = 0x03,
	SymbolicLink = 0x04,
	Token = 0x05,
	Job = 0x06,
	Process = 0x07,
	Thread = 0x08,
	UserApcReserve = 0x09,
	IoCompletionReserve = 0x0A,
	DebugObject = 0x0B,
	Event = 0x0C,
	EventPair = 0x0D,
	Mutant = 0x0E,
	Callback = 0x0F,
	Semaphore = 0x10,
	Timer = 0x11,
	IRTimer = 0x12,
	Profile = 0x13,
	KeyedEvent = 0x14,
	WindowStation = 0x15,
	Desktop = 0x16,
	CompositionSurface = 0x17,
	TpWorkerFactory = 0x18,
	Adapter = 0x19,
	Controller = 0x1A,
	Device = 0x1B,
	Driver = 0x1C,
	IoCompletion = 0x1D,
	WaitCompletionPacket = 0x1E,
	File = 0x1F,
	TmTm = 0x20,
	TmTx = 0x21,
	TmRm = 0x22,
	TmEn = 0x23,
	Section = 0x24,
	Session = 0x25,
	Key = 0x26,
	ALPCPort = 0x27,
	PowerRequest = 0x28,
	WmiGuid = 0x29,
	EtwRegistration = 0x2A,
	EtwConsumer = 0x2B,
	FilterConnectionPort = 0x2C,
	FilterCommunicationPort = 0x2D,
	PcwObject = 0x2E,
	DxgkSharedResource = 0x2F,
	DxgkSharedSyncObject = 0x30
}

public enum Windows7_Kernel_Objects
{
	Type = 0x02,
	Directory = 0x03,
	SymbolicLink = 0x04,
	Token = 0x05,
	Job = 0x06,
	Process = 0x07,
	Thread = 0x08,
	UserApcReserve = 0x09,
	IoCompletionReserve = 0x0A,
	DebugObject = 0x0B,
	Event = 0x0C,
	EventPair = 0x0D,
	Mutant = 0x0E,
	Callback = 0x0F,
	Semaphore = 0x10,
	Timer = 0x11,
	Profile = 0x12,
	KeyedEvent = 0x13,
	WindowStation = 0x14,
	Desktop = 0x15,
	TpWorkerFactory = 0x16,
	Adapter = 0x17,
	Controller = 0x18,
	Device = 0x19,
	Driver = 0x1a,
	IoCompletion = 0x1b,
	File = 0x1c,
	TmTm = 0x1d,
	TmTx = 0x1e,
	TmRm = 0x1f,
	TmEn = 0x20,
	Section = 0x21,
	Session = 0x22,
	Key = 0x23,
	ALPCPort = 0x24,
	PowerRequest = 0x25,
	WmiGuid = 0x26,
	EtwRegistration = 0x27,
	EtwConsumer = 0x28,
	FilterConnectionPort = 0x29,
	FilterCommunicationPort = 0x2a,
	PcwObject = 0x2b
}

public enum WindowsVista_Kernel_Objects
{
	Type = 0x01,
	Directory = 0x02,
	SymbolicLink = 0x03,
	Token = 0x04,
	Job = 0x05,
	Process = 0x06,
	Thread = 0x07,
	DebugObject = 0x08,
	Event = 0x09,
	EventPair = 0x0A,
	Mutant = 0x0B,
	Callback = 0x0C,
	Semaphore = 0x0D,
	Timer = 0x0E,
	Profile = 0x0F,
	KeyedEvent = 0x10,
	WindowsStation = 0x11,
	Desktop = 0x12,
	TpWorkerFactory = 0x13,
	Adapter = 0x14,
	Contoller = 0x15,
	Device = 0x16,
	Driver = 0x17,
	IoCompletion = 0x18,
	File = 0x19,
	TmTm = 0x1a,
	TmTx = 0x1b,
	TmRm = 0x1c,
	TmEn = 0x1d,
	Section = 0x1e,
	Session = 0x1f,
	Key = 0x20,
	ALPCPort = 0x21,
	WmiGuid = 0x22,
	EtwRegistration = 0x23,
	FilterConnectionPort = 0x24,
	FilterCommunicationPort = 0x25
}