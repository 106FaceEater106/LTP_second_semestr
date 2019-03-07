using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		private static readonly List<char> Constants = 
			"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToList();
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			Constants.ForEach(x => vm.RegisterCommand(x, b => b.Memory[b.MemoryPointer] = (byte) x));
			vm.RegisterCommand('.', b => write((char) b.Memory[b.MemoryPointer]));
			vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte) read());
			vm.RegisterCommand('+', b => b.Memory[b.MemoryPointer] = b.Memory[b.MemoryPointer] < 255 
															? (byte) (b.Memory[b.MemoryPointer] + 1) 
															: (byte) 0);
			vm.RegisterCommand('-', b => b.Memory[b.MemoryPointer] = b.Memory[b.MemoryPointer] > 0 
															? (byte) (b.Memory[b.MemoryPointer] - 1) 
															: (byte) 255);
			vm.RegisterCommand('<', b => b.MemoryPointer = b.MemoryPointer > 0 
															? b.MemoryPointer - 1 
															: b.Memory.Length - 1);
			vm.RegisterCommand('>', b => b.MemoryPointer = b.MemoryPointer < b.Memory.Length - 1 
															? b.MemoryPointer + 1 
															: 0);
		}
	}
}
