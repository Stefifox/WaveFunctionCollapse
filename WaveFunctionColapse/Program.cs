﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveFunctionCollapse
{
    class Program
    {
        private const int GridRows = 20;
        private const int GridColumns = 20;

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

            var startNode = MyNodes[random.Next(0, MyNodes.Count)].Clone();
            startNode.Row = GridRows / 2;
            startNode.Col = GridColumns / 2;
            startNode.IsCollapsed = true;
            Grid[startNode.Row, startNode.Col] = startNode;

            CheckNeighbors(startNode.Col, startNode.Row);

            while (NodesToProcess.Count > 0)
            {
                var node = NodesToProcess.First();
                Collapse(node);
                NodesToProcess.Remove(node);
            }

            Extras.PrintGrid(Grid, GridRows, GridColumns);
        }

        /// <summary>
        /// Define the list of nodes to use.
        /// </summary>
        private static void InitNodes()
        {
            MyNodes.AddRange([
                //Nodes.All,
                Nodes.Right, Nodes.Left, Nodes.Top, Nodes.Bottom, Nodes.LeftRight, Nodes.TopBottom,
                Nodes.LeftRightTop, Nodes.LeftTopBottom, Nodes.RightTopBottom, Nodes.LeftBottom,
                Nodes.TopRight, Nodes.RightBottom, Nodes.TopLeft, Nodes.BottomLeftRight
            ]);
        }

        /// <summary>
        /// Initializes the grid.
        /// </summary>
        private static void InitGrid()
        {
            for (var row = 0; row < GridRows; row++)
            {
                for (var col = 0; col < GridColumns; col++)
                {
                    var baseNode = Nodes.Void.Clone();
                    baseNode.Row = row;
                    baseNode.Col = col;
                    baseNode.IsCollapsed = (row > 0 && col == 0) || (row == 0 && col > 0) || (row == GridRows -1  && col > 0) || (row > 0 && col == GridColumns - 1);
                    Grid[row, col] = baseNode;
                }
            }
        }

        /// <summary>
        /// Add a node to the list to process.
        /// </summary>
        /// <param name="node"></param>
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
            var neighbors = GetNeighborsDoors(col, row);

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
        
        /// <summary>
        /// Get neighbors of the current node in the door direction 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Dictionary<string, Node> GetNeighborsDoors(int col, int row)
        {
            var neighbors = new Dictionary<string, Node>();

            // Left Node
            if (col > 0)
                if(Grid[row, col].Corners.Left == 3)
                    neighbors.Add("Left", Grid[row, col - 1]);
            // Right Node
            if (col < GridColumns - 1)
                if(Grid[row, col].Corners.Right == 4)
                    neighbors.Add("Right", Grid[row, col + 1]);
            // Top Node
            if (row > 0)
                if(Grid[row, col].Corners.Top == 1)
                    neighbors.Add("Top", Grid[row - 1, col]);
            // Bottom Node
            if (row < GridRows - 1)
                if(Grid[row, col].Corners.Bottom == 2)
                    neighbors.Add("Bottom", Grid[row + 1, col]);

            return neighbors;
        }
        
        /// <summary>
        ///  Checks if a given node is compatible with the neighbors.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="neighbors"></param>
        /// <returns></returns>
        private static bool IsCompatible(Node node, Dictionary<string, Node> neighbors)
        {
            var isCompatible = true;

            foreach (var neighbor in neighbors)
            {
                var neighborNode = neighbor.Value;
                if (neighborNode.IsCollapsed)
                {
                    switch (neighbor.Key)
                    {
                        case "Top":
                            if(neighborNode.Row - 1 < 0 && node.Corners.Top == 1 )
                                isCompatible = false;
                            if (node.Corners.Top != 1 && neighborNode.Corners.Bottom == 2 ||
                                node.Corners.Top == 1 && neighborNode.Corners.Bottom != 2)
                                isCompatible = false;
                            break;
                        case "Bottom":
                            if (neighborNode.Row + 1 > GridRows - 1 && node.Corners.Bottom == 2)
                                isCompatible = false;
                            if (node.Corners.Bottom != 2 && neighborNode.Corners.Top == 1 ||
                                node.Corners.Bottom == 2 && neighborNode.Corners.Top != 1)
                                isCompatible = false;
                            break;
                        case "Left":
                            if (neighborNode.Col - 1 < 0 && node.Corners.Left == 3)
                                isCompatible = false;
                            if (node.Corners.Left != 3 && neighborNode.Corners.Right == 4 ||
                                node.Corners.Left == 3 && neighborNode.Corners.Right != 4)
                                isCompatible = false;
                            break;
                        case "Right":
                            if (neighborNode.Col + 1 > GridColumns - 1 && node.Corners.Right == 4)
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

        /// <summary>
        /// Collapse the current node.
        /// </summary>
        /// <param name="current"></param>
        private static void Collapse(Node current)
        {
            var compatibleNodes = new List<Node>();

            var neighbors = GetNeighbors(current.Col, current.Row);

            foreach (var node in MyNodes)
            {
                if (IsCompatible(node, neighbors))
                    compatibleNodes.Add(node);
            }

            if (compatibleNodes.Count > 0)
            {
                var radomValue = random.Next(0, compatibleNodes.Count);
                var randomNode = compatibleNodes[radomValue].Clone();
                randomNode.Row = current.Row;
                randomNode.Col = current.Col;
                randomNode.IsCollapsed = true;
                Grid[current.Row, current.Col] = randomNode;
            }
            else
            {
                var voidNode = Nodes.Void.Clone();
                voidNode.Row = current.Row;
                voidNode.Col = current.Col;
                voidNode.IsCollapsed = true;
                Grid[current.Row, current.Col] = voidNode;
            }

            CheckNeighbors(current.Col, current.Row);
        }
    }
}