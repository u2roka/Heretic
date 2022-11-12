using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Heretic.Core
{
    internal class PathFinding
    {
        private AStar aStar;

        public PathFinding(Map map)
        {
            var nodes = new List<List<Node>>();
            for (int y = 0; y < map.WorldMap.GetLength(0); y++)
            {
                var row = new List<Node>();
                for (int x = 0; x < map.WorldMap.GetLength(1); x++)
                {
                    row.Add(new Node(new Vector2(x, y), map.WorldMap[y, x] == 0));
                }
                nodes.Add(row);
            }

            aStar = new AStar(nodes);
        }

        public Vector2 GetPath(Vector2 start, Vector2 goal)
        {
            var path = aStar.FindPath(start, goal);

            return path.Pop().Indices;
        }

        public void Update(List<Point> npcPositions)
        {
            aStar.NPCPositions = npcPositions;
        }
    }
}
