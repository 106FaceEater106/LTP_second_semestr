using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
    public class DijkstraData
    {
        public Point Previous;
        public readonly int Difficulty;
        public DijkstraData(Point previous, int difficulty)
        {
            Previous = previous;
            Difficulty = difficulty;
        }
    }
    
    public class DijkstraPathFinder
    {
        private static Dictionary<Point, DijkstraData> track;
        private static readonly Point[] ArrayOfIndex =
        { 
            new Point(1, 0),
            new Point(0,-1),
            new Point(0,1),
            new Point(-1,0), 
        };

        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
            IEnumerable<Point> targets)
        {
            var chests = new HashSet<Point>(targets);
            var notVisited = new HashSet<Point> {start};
            track = new Dictionary<Point, DijkstraData> {[start] = new DijkstraData(new Point(-1, -1), 0)};
            while (true)
            {
                var toOpen = GetPointForOpening(notVisited);
                if (chests.Contains(toOpen))
                {
                    chests.Remove(toOpen);
                    yield return new PathWithCost(track[toOpen].Difficulty, GetPaths(toOpen).Reverse().ToArray());    
                }
                
                if (chests.Count == 0 || notVisited.Count == 0) yield break;
                CorrectTrack(toOpen, state, notVisited);
            }
        }

        private static void CorrectTrack(Point toOpen, State state, HashSet<Point> notVisited)
        {
            foreach (var e in ArrayOfIndex.Select(x => new Point(toOpen.X + x.X, toOpen.Y + x.Y))
                .Where(x => state.InsideMap(x) && !state.IsWallAt(x)))
            {
                var currentPrice = track[toOpen].Difficulty + state.CellCost[e.X, e.Y];
                if (!track.ContainsKey(e))
                    notVisited.Add(e);
                if (!track.ContainsKey(e) || track[e].Difficulty > currentPrice)
                    track[e] = new DijkstraData(toOpen, currentPrice);
            }

            notVisited.Remove(toOpen);
        }

        private static Point GetPointForOpening(IEnumerable<Point> notVisited)
        {
            var toOpen = new Point(-1, -1);
            var minDifficulty = double.PositiveInfinity;
            foreach (var e in notVisited.Where(track.ContainsKey))
            {
                if (track[e].Difficulty > minDifficulty) continue;
                minDifficulty = track[e].Difficulty;
                toOpen = e;
            }

            return toOpen;
        }

        private static IEnumerable<Point> GetPaths(Point end)
        {
            while (end != new Point(-1, -1))
            {
                yield return end;
                end = track[end].Previous;   
            }
        }
    }
}
