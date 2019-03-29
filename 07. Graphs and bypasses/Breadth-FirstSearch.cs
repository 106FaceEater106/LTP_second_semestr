using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
	public class BfsTask
	{
		private static readonly Point[] ArrayOfIndex =
		{ 
			new Point(1, 0),
			new Point(0,-1),
			new Point(0,1),
			new Point(-1,0), 
		};
		
		public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
		{
			var queue = new Queue<SinglyLinkedList<Point>>();
			var visited = new HashSet<Point> {start};
			queue.Enqueue(new SinglyLinkedList<Point>(start));
			while (queue.Count != 0)
			{
				var path = queue.Dequeue();
				foreach (var index in ArrayOfIndex)
				{
					var nextPath = new SinglyLinkedList<Point>(new Point {X = path.Value.X + index.X, Y = path.Value.Y + index.Y},
						path);
					if (!map.InBounds(nextPath.Value) || map.Dungeon[nextPath.Value.X, nextPath.Value.Y] != MapCell.Empty
					                                  || visited.Contains(nextPath.Value))
						continue;
					queue.Enqueue(nextPath);
					visited.Add(nextPath.Value);
					if (chests.Contains(nextPath.Value))
						yield return nextPath;
				}
			}
		}
	}
}
