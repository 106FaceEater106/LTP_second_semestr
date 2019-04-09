using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            if (state.Chests.Count < state.Goal) return new List<Point>();
            var collectedChests = 0;
            var path = new List<Point>();
            while (collectedChests < state.Goal)
            {
                var shortPath = new DijkstraPathFinder()
                    .GetPathsByDijkstra(state, state.Position, state.Chests)
                    .FirstOrDefault();
                if (shortPath == null) return new List<Point>();
                path.AddRange(shortPath.Path.Skip(1));
                state.Position = shortPath.End;
                state.Energy-= shortPath.Cost;
                if (state.Energy < 0) return new List<Point>();
                state.Chests.Remove(shortPath.End);
                collectedChests++;
            }

            return path;
        }
    }
}
