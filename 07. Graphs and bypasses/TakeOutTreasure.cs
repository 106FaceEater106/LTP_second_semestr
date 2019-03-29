using System;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathFromStartToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] {map.Exit})
                .FirstOrDefault()?.ToList();
            var pathsFromStart = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
            var pathsFromExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
            var sortPath = pathsFromStart
                .Join(pathsFromExit, x => x.Value, x => x.Value, Tuple.Create)
                .OrderBy(x => x.Item1.Length + x.Item2.Length)
                .Select(x => Enumerable
                    .Reverse(x.Item1.ToList())
                    .Concat(x.Item2.ToList().Skip(1)))
                .FirstOrDefault();
            var path = sortPath ?? (pathFromStartToExit == null ? null : Enumerable.Reverse(pathFromStartToExit));
            return path != null
                ? path.Zip(path.Skip(1), (x, y) => Walker
                        .ConvertOffsetToDirection(new Size(y) - new Size(x)))
                    .ToArray()
                : new MoveDirection[0];
        }
    }
}
