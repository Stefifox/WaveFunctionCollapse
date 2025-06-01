using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace WaveFunctionCollapse
{
    class Program
    {
        private const int GridRows = 5;
        private const int GridColumns = 5;

        /// <summary>
        /// List of possible nodes.
        /// </summary>
        private static readonly List<Node> MyNodes = new List<Node>();

        /// <summary>
        /// List of nodes to process.
        /// </summary>
        private static readonly List<Node> NodesToProcess = new List<Node>();

        private static readonly Node[,] Grid = new Node[GridRows, GridColumns];

        private static Random random = new Random();

        public static void Main(string[] args)
        {
            InitNodes();
            InitGrid();

            var startNode = Nodes.All.Clone();
            startNode.row = 1;
            startNode.col = 2;
            startNode.IsCollapsed = true;
            Grid[1, 2] = startNode;

            CheckNeighbors(startNode.col, startNode.row);

            while (NodesToProcess.Count > 0)
            {
                var node = NodesToProcess.First();
                Collapse(node);
                NodesToProcess.Remove(node);
            }

            Extras.PrintGrid(Grid, GridRows, GridColumns);
        }

        private static void InitNodes()
        {
            MyNodes.AddRange([
                Nodes.Right, Nodes.Left, Nodes.Top, Nodes.Bottom, Nodes.LeftRight, Nodes.TopBottom
            ]);
        }

        private static void InitGrid()
        {
            for (var row = 0; row < GridRows; row++)
            {
                for (var col = 0; col < GridColumns; col++)
                {
                    var baseNode = Nodes.Void.Clone();
                    baseNode.row = row;
                    baseNode.col = col;
                    Grid[row, col] = baseNode;
                }
            }
        }

        private static void AddToProcess(Node node)
        {
            if (NodesToProcess.Contains(node))
                return;
            NodesToProcess.Add(node);
        }

        /// <summary>
        /// Checks if a neighbor node needs to be collapsed
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private static void CheckNeighbors(int col, int row)
        {
            var neighbors = GetNeighbors(col, row);

            foreach (var neighbor in neighbors.Values.Where(neighbor => !neighbor.IsCollapsed))
            {
                AddToProcess(neighbor);
            }
        }

        /// <summary>
        /// Returns a dictionary containing the neighbors of a given node.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Dictionary<string, Node> GetNeighbors(int col, int row)
        {
            var neighbors = new Dictionary<string, Node>();

            // Left Node
            if (col > 0)
                neighbors.Add("Left", Grid[row, col - 1]);
            // Right Node
            if (col < GridColumns - 1)
                neighbors.Add("Right", Grid[row, col + 1]);
            // Top Node
            if (row > 0)
                neighbors.Add("Top", Grid[row - 1, col]);
            // Bottom Node
            if (row < GridRows - 1)
                neighbors.Add("Bottom", Grid[row + 1, col]);

            return neighbors;
        }

        private static bool IsCompatible(Node node, Dictionary<string, Node> neighbors)
        {
            var isCompatible = true;

            foreach (var neighbor in neighbors)
            {
                var neighborNode = neighbor.Value;
                // isCompatible = CornerCompatible(neighbor.Key, node, neighborNode);
                if (neighborNode.IsCollapsed)
                {
                    switch (neighbor.Key)
                    {
                        case "Top":
                            if(neighborNode.row - 1 < 0 && node.Corners.Top == 1 )
                                isCompatible = false;
                            if (node.Corners.Top != 1 && neighborNode.Corners.Bottom == 2 ||
                                node.Corners.Top == 1 && neighborNode.Corners.Bottom != 2)
                                isCompatible = false;
                            break;
                        case "Bottom":
                            if (neighborNode.row + 1 > GridRows - 1 && node.Corners.Bottom == 2)
                                isCompatible = false;
                            if (node.Corners.Bottom != 2 && neighborNode.Corners.Top == 1 ||
                                node.Corners.Bottom == 2 && neighborNode.Corners.Top != 1)
                                isCompatible = false;
                            break;
                        case "Left":
                            if (neighborNode.col - 1 < 0 && node.Corners.Left == 3)
                                isCompatible = false;
                            if (node.Corners.Left != 3 && neighborNode.Corners.Right == 4 ||
                                node.Corners.Left == 3 && neighborNode.Corners.Right != 4)
                                isCompatible = false;
                            break;
                        case "Right":
                            if (neighborNode.col + 1 > GridColumns - 1 && node.Corners.Right == 4)
                                isCompatible = false;
                            if (node.Corners.Right != 4 && neighborNode.Corners.Left == 3 ||
                                node.Corners.Right == 4 && neighborNode.Corners.Left != 3)
                                isCompatible = false;
                            break;
                    }
                }
            }

            return isCompatible;
        }

       
        private static void Collapse(Node current)
        {
            var compatibleNodes = new List<Node>();

            var neighbors = GetNeighbors(current.col, current.row);

            foreach (var node in MyNodes)
            {
                if (IsCompatible(node, neighbors))
                    compatibleNodes.Add(node);
            }

            if (compatibleNodes.Count > 0)
            {
                var radomValue = random.Next(0, compatibleNodes.Count);
                var randomNode = compatibleNodes[radomValue].Clone();
                randomNode.row = current.row;
                randomNode.col = current.col;
                randomNode.IsCollapsed = true;
                Grid[current.row, current.col] = randomNode;
            }
            else
            {
                var voidNode = Nodes.Void.Clone();
                voidNode.row = current.row;
                voidNode.col = current.col;
                voidNode.IsCollapsed = true;
                Grid[current.row, current.col] = voidNode;
            }

            CheckNeighbors(current.col, current.row);
        }
    }
}