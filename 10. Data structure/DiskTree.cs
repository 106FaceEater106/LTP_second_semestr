using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public static class DiskTreeTask
    {
        public static List<string> Solve(IEnumerable<string> input)
        {
            var root = new Directory("");
            foreach (var directory in input)
                directory
                    .Split('\\')
                    .Aggregate(root, (currentDirectory, item) => currentDirectory.Add(item));
            return GetDirectoryList(root, -1, new List<string>());
        }
        
        private static List<string> GetDirectoryList(Directory root, int whitespaceCount, List<string> list)
        {
            if (whitespaceCount >= 0)
                list.Add(new string(' ', whitespaceCount) + root.Name);
            whitespaceCount++;
            return root.Subdirectories.Values
                .OrderBy(x => x.Name, StringComparer.Ordinal)
                .Aggregate(list, (current, child) => GetDirectoryList(child, whitespaceCount, current));
        }
    }
    
    public class Directory
    {
        public readonly string Name;
        public readonly Dictionary<string, Directory> Subdirectories = new Dictionary<string, Directory>();
        public Directory(string name) => Name = name;
        public Directory Add(string subRoot) => Subdirectories
            .TryGetValue(subRoot, out var directory) ? directory 
            : Subdirectories[subRoot] = new Directory(subRoot);
    }
}
