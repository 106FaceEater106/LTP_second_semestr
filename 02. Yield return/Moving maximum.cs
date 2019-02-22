using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var potentialMax = new LinkedList<DataPoint>();
            var pointInWindow = new Queue<DataPoint>();
            foreach (var item in data)
            {
                pointInWindow.Enqueue(item);
                if (pointInWindow.Count > windowWidth)
                {
                    if (potentialMax.First.Value.OriginalY <= pointInWindow.Peek().OriginalY)
                        potentialMax.RemoveFirst();
                    pointInWindow.Dequeue();
                }
                    
                while (potentialMax.Count > 0 &&
                       potentialMax.Last.Value.OriginalY <= item.OriginalY)
                    potentialMax.RemoveLast();
                potentialMax.AddLast(item);
                item.MaxY = potentialMax.First.Value.OriginalY;
                yield return item;
            }
        }
    }
}

