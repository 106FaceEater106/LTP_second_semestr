using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
 
namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        private int correctPathCost = int.MaxValue;
        private List<PathWithCost> correctPath = new List<PathWithCost>();
        private readonly Dictionary<Point, List<PathWithCost>> paths = new Dictionary<Point, List<PathWithCost>>();
        private readonly HashSet<Point> visited = new HashSet<Point>();
        private readonly List<PathWithCost> path = new List<PathWithCost>();
        public List<Point> FindPathToCompleteGoal(State state)
        {
            foreach (var e in new List<Point>(state.Chests) {state.Position})
                paths[e] = new DijkstraPathFinder()
                    .GetPathsByDijkstra(state, e, state.Chests.Where(x => e != x))
                    .Select(x => new PathWithCost(x.Cost, x.Path.Skip(1).ToArray()))
                    .ToList();
            FindBestPath(state.Position, 0, state.Energy);
            return correctPath.SelectMany(x => x.Path).ToList();
        }
 
        private void FindBestPath(Point position, int pathCost, int energy)
        {
            if (pathCost > energy) return;
            foreach (var nextPath in paths[position].Where(x => !visited.Contains(x.End)))
            {
                visited.Add(nextPath.End);
                path.Add(nextPath);
                FindBestPath(nextPath.End,pathCost + nextPath.Cost, energy);
                visited.Remove(nextPath.End);
                path.Remove(nextPath);
            }

            if (correctPath.Count >= path.Count && (correctPath.Count > path.Count || correctPathCost <= pathCost)) return;
            correctPath = new List<PathWithCost>(path);
            correctPathCost = pathCost;
        }
    }
}
