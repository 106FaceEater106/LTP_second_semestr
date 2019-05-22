using System.Collections.Generic;
using System.Linq;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> track = new List<T>();
        
        public T this[int index]
        {
            get
            {
                lock (track)
                    return index < track.Count ? track[index] : null;
            }
            set
            {
                lock (track)
                {
                    track.RemoveRange(index, track.Count - index);
                    track.Add(value);
                }
            }
        }
        
        public T LastItem()
        {
            lock (track)
                return track.Count == 0 ? null : track[track.Count - 1];
        }
        
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (track)
                if (knownLastItem == null && track.Count == 0 || track.Last() == knownLastItem)
                    track.Add(item);
        }
        
        public int Count 
        {
            get
            {
                lock (track)
                    return track.Count;
            }
        }
    }
}
