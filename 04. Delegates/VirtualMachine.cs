using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		private readonly Dictionary<char, Action<IVirtualMachine>> commads = new Dictionary<char, Action<IVirtualMachine>>();
		public int Length => Memory.Length;
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute) => commads.Add(symbol, execute);
		public VirtualMachine(string program, int memorySize) 
		{
			Instructions = program;
			Memory = new byte[memorySize];
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (commads.ContainsKey(Instructions[InstructionPointer]))
					commads[Instructions[InstructionPointer]](this);
				InstructionPointer++;
			}
		}
	}
}
