using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> clones = new List<Clone> { new Clone() };

        public string Execute(string query)
		{
			var splitQuery = query.Split();
            var cloneID = int.Parse(splitQuery[1]) - 1;
            switch (splitQuery[0])
            {
                case "learn":
                        clones[cloneID].Learn(splitQuery[2]);
                        return null;
                case "rollback":
                        clones[cloneID].Rollback();
                        return null;
                case "relearn":
                        clones[cloneID].Relearn();
                        return null;
                case "clone":
                        clones.Add(new Clone(clones[cloneID]));
                        return null;
                case "check":
                        return clones[cloneID].Check();
                default:
                    throw new ArgumentException();
            }
        }

        public class Clone
        {
            private readonly Stack learnedProgram;
            private Stack undoProgram;

            public Clone()
            {
                learnedProgram = new Stack();
                undoProgram = new Stack();
            }

            public Clone(Clone mother)
            {
                learnedProgram = new Stack(mother.learnedProgram);
                undoProgram = new Stack(mother.undoProgram);
            }

            public void Learn(string programID)
            {
                undoProgram = new Stack();
                learnedProgram.Push(programID);
            }

            public void Rollback()
            {
                undoProgram.Push(learnedProgram.Pop());
            }

            public void Relearn()
            {
                learnedProgram.Push(undoProgram.Pop());
            }

            public string Check()
            {
                return learnedProgram.BottomElement == null ? "basic" : learnedProgram.Peek();
            }
        }
    }

    public class Stack
    {
        public StacksElements BottomElement;

        public Stack(Stack stack)
        {
            BottomElement = stack.BottomElement;
        }

        public Stack()
        {
            BottomElement = null;
        }

        public void Push(string item)
        {
            BottomElement = new StacksElements(item, BottomElement);
        }

        public string Pop()
        {
            var previousBottom = BottomElement.Value;
            BottomElement = BottomElement.Previous;
            return previousBottom;
        }
       
        public string Peek()
        {
            return BottomElement.Value;
        }
    }

    public class StacksElements
    {
        public readonly string Value;
        public readonly StacksElements Previous;

        public StacksElements(string value, StacksElements previous)
        {
            Value = value;
            Previous = previous;
        }
    }
}
