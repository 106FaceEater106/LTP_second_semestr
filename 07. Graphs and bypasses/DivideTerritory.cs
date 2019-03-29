using System;
using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
	public class RivalsTask
	{
		private static readonly Point[] ArrayOfIndex =
		{
			new Point(1, 0),
			new Point(0, -1),
			new Point(0, 1),
			new Point(-1, 0),
		};

		public static IEnumerable<OwnedLocation> AssignOwners(Map map)
		{
			var queue = new Queue<Tuple<Point,int>>();
			var visited = new HashSet<Point>();
			var ways = new Dictionary<Point, int>();
			InitializePlayers(map, queue, ways, visited);
			while (queue.Count != 0)
			{
				var player = queue.Dequeue();
				var playerPoint = player.Item1;
				var playerIndex = player.Item2;
				yield return new OwnedLocation(playerIndex, playerPoint, ways[playerPoint]);
				foreach (var index in ArrayOfIndex)
				{
					var nextPoint = new Point {X = playerPoint.X + index.X, Y = playerPoint.Y + index.Y};
					if (!map.InBounds(nextPoint) || map.Maze[nextPoint.X, nextPoint.Y] != MapCell.Empty
					                             || visited.Contains(nextPoint)) 
						continue;
					queue.Enqueue(Tuple.Create(nextPoint, playerIndex));
					visited.Add(nextPoint);
					ways.Add(nextPoint, ways[playerPoint] + 1);
				}
			}
		}

		private static void InitializePlayers(Map map, Queue<Tuple<Point,int>> queue, 
											  IDictionary<Point, int> ways, ISet<Point> visited)
		{
			for (var i = 0; i < map.Players.Length; i++)
			{
				queue.Enqueue(Tuple.Create(map.Players[i], i));
				ways.Add(map.Players[i], 0);
				visited.Add(map.Players[i]);
			}
		}
	}
}

