using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private readonly byte[] bytes;
        private const int Prime = 16777619;
        private readonly int hash;
        public int Length => bytes.Length;
        public int this[int index] => bytes[index];
        public ReadonlyBytes(params byte[] bytes)
        {
            this.bytes = bytes ?? throw new ArgumentNullException();
            hash = CalculateHash();
        }
		
        private int CalculateHash()
        {
            return Convert.ToInt32(bytes.Aggregate(unchecked((int) 2166136261), (y, x) => y = (y << Prime) ^ x));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
		
        public IEnumerator<byte> GetEnumerator()
        {
            foreach (var item in bytes)
                yield return item;
        }

        public override bool Equals(object obj)
        {
            return obj is ReadonlyBytes && ((ReadonlyBytes) obj).Length == bytes.Length &&
                   !bytes.Where((x, i) => x != ((ReadonlyBytes) obj)[i]).Any();
        }
		
        public override int GetHashCode()
        {
            return hash;
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", bytes) + "]";
        }
    }
}
