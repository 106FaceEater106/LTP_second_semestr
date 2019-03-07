using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var bracketsIndex = SearchAllBrace(vm);
			vm.RegisterCommand('[', b =>
			{
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = bracketsIndex[b.InstructionPointer];
			});
			vm.RegisterCommand(']', b =>
			{
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = bracketsIndex[b.InstructionPointer];
			});
		}
		
		private static Dictionary<int, int> SearchAllBrace(IVirtualMachine vm)
		{
			var openBracketsIndexex = new Stack<int>();
			var bracketsIndexes = new Dictionary<int, int>();
			for (var index = 0; index < vm.Instructions.Length; index++)
			{
				switch (vm.Instructions[index])
				{
					case '[':
						openBracketsIndexex.Push(index);
						break;
					case ']':
						bracketsIndexes.Add(openBracketsIndexex.Peek(), index);
						bracketsIndexes.Add(index, openBracketsIndexex.Pop());
						break;
					default: 
						continue;
				}
			}
			
			return bracketsIndexes;
		}
	}
}
