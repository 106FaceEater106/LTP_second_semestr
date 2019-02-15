using System.Collections.Generic;
using System.Linq;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        private LimitedSizeStack<UndoItem<TItem>> stack;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stack = new LimitedSizeStack<UndoItem<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            AddToStack("add", item, Items.Count - 1);
        }

        public void RemoveItem(int index)
        {
            AddToStack("remove", Items.ElementAt(index), index);
            Items.RemoveAt(index);
        }

        public void AddToStack (string type, TItem item, int position)
        {
            var job = new UndoItem<TItem>(type, item, position);
            stack.Push(job);
        }

        public bool CanUndo()
        {
            return stack.Count > 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                var job = stack.Pop();
                if (job.Type == "add")
                    Items.RemoveAt(job.Position);
                else Items.Insert(job.Position, job.Item);
            }
        }
    }

    public class UndoItem<TItem>
    {
        public string Type;
        public TItem Item;
        public int Position;

        public UndoItem(string type, TItem item, int position)
        {
            Type = type;
            Item = item;
            Position = position;
        }
    }
}
