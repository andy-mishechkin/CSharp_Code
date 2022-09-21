using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTablesTest
{
    class Program
    {
        static string GetKernelObject(int HandlerValue)
        {
            string KernelObject = null;
            Hashtable KernelObjects = new Hashtable();

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

            foreach (DictionaryEntry d in KernelObjects)
            {
                Console.WriteLine("Key type: {0}", d.Key.GetType());
                Console.WriteLine("Hashtable entry: {0}, {1}", d.Key, d.Value);
            }

            ICollection KernelObjectCodes = KernelObjects.Keys;
            foreach (int Code in KernelObjectCodes)
                Console.WriteLine("Handler object: {0}", KernelObjects[Code]);

            if (KernelObjects.ContainsKey(HandlerValue))
                KernelObject = (KernelObjects[HandlerValue]).ToString();

            return KernelObject;
        }

        static void Main(string[] args)
        {
            string KernelObject = GetKernelObject(0x31);
            Console.WriteLine("Kernel object: {0}", KernelObject);
        }
    }
}
