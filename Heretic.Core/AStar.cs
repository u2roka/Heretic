using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Heretic.Core
{
    internal class AStar
    {
        private List<List<Node>> grid;
        
        private List<Point> npcPositions;
        public List<Point> NPCPositions
        {
            set
            {
                npcPositions = value;
            }
        }
        
        private int GridCols
        {
            get
            {
                return grid[0].Count;
            }
        }
        
        private int GridRows
        {
            get
            {
                return grid.Count;
            }
        }

        public AStar(List<List<Node>> grid)
        {
            this.grid = grid;
        }

        public Stack<Node> FindPath(Vector2 start, Vector2 goal)
        {
            Node begin = new Node(start, true);
            Node end = new Node(goal, false);

            Stack<Node> path = new Stack<Node>();
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            List<Node> adjacencies;
            Node current = begin;

            openList.Add(begin);

            while (openList.Count != 0 && !closedList.Exists(x => x.Indices == end.Indices))
            {
                current = openList[0];
                openList.Remove(current);
                closedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

                foreach (Node next in adjacencies)
                {
                    if (!closedList.Contains(next) && next.Walkable)
                    {
                        if (!openList.Contains(next))
                        {
                            if (!npcPositions.Contains(next.ToPoint()))
                            {
                                next.Parent = current;
                                next.DistanceToTarget = MathF.Abs(next.Indices.X - end.Indices.X) + MathF.Abs(next.Indices.Y - end.Indices.Y);
                                next.Cost = next.Weight + next.Parent.Cost;
                                openList.Add(next);
                                openList.Sort();
                            }
                        }
                    }
                }
            }

            if (!closedList.Exists(x => x.Indices == end.Indices))
            {
                path.Push(end);
            }
            else
            {
                Node node = closedList[closedList.IndexOf(current)];
                if (node == null) return null;
                do
                {
                    path.Push(node);
                    node = node.Parent;
                } while (node != begin && node != null);
            }

            return path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> result = new List<Node>();

            int row = (int)n.Indices.Y;
            int col = (int)n.Indices.X;

            if (row + 1 < GridRows)
            {
                result.Add(grid[row + 1][col]);
            }
            if (row - 1 >= 0)
            {
                result.Add(grid[row - 1][col]);
            }
            if (col - 1 >= 0)
            {
                result.Add(grid[row][col - 1]);
            }
            if (col + 1 < GridCols)
            {
                result.Add(grid[row][col + 1]);
            }

            return result;
        }
    }

    public class Node : IComparable<Node>
    {
        private Node parent;
        public Node Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        private Vector2 indices;
        public Vector2 Indices
        {
            get
            {
                return indices;
            }
        }

        private float distanceToTarget;
        public float DistanceToTarget
        {
            set
            {
                distanceToTarget = value;
            }
        }

        private float cost;
        public float Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        
        private float weight;
        public float Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }
        
        public float F
        {
            get
            {
                if (distanceToTarget != -1 && cost != -1)
                    return distanceToTarget + cost;
                else
                    return -1;
            }
        }

        public bool Walkable;

        public Node(Vector2 indices, bool walkable, float weight = 1)
        {
            parent = null;
            this.indices = indices;
            distanceToTarget = -1;
            cost = 1;
            this.weight = weight;
            Walkable = walkable;
        }

        public int CompareTo(Node other)
        {
            return F.CompareTo(other.F);
        }

        public Point ToPoint()
        {
            return indices.ToPoint();
        }
    }
}
