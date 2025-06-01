using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace WaveFunctionCollapse
{
    class Program
    {
        private const int GridHeight = 5;
        private const int GridWidth = 5;

        /// <summary>
        /// List of possible nodes.
        /// </summary>
        private static readonly List<Node> Nodes = new List<Node>();

        /// <summary>
        /// List of nodes to process.
        /// </summary>
        private static readonly List<Node> NodesToProcess = new List<Node>();

        private static readonly Node[,] Grid = new Node[GridHeight, GridWidth];

        private static Random random = new Random();

        public static void Main(string[] args)
        {
            InitNodes();
            InitGrid();

            int randomStartX = random.Next(0, GridHeight);
            int randomStartY = random.Next(0, GridWidth);
            int randomNode = random.Next(0, Nodes.Count);
            Console.WriteLine($"Random Node at [{randomStartX}, {randomStartY}]: {Nodes[randomNode].Name}");

            var randomNodeClone = Nodes[randomNode].Clone();
            randomNodeClone.IsCollapsed = true;
            randomNodeClone.x = randomStartX;
            randomNodeClone.y = randomStartY;
            Grid[randomStartX, randomStartY] = randomNodeClone;
            CheckNeighbors(randomStartX, randomStartY);

            while (NodesToProcess.Count > 0)
            {
                var currentNode = NodesToProcess.First();
                Collapse(currentNode);
                NodesToProcess.Remove(currentNode);
            }

            Extras.PrintGrid(Grid, GridHeight, GridWidth);
        }

        private static void InitNodes()
        {
            var room0 = new Node()
            {
                Name = "Tutti",
                Texture = "room0000.png",
                Corners = new Corners()
                {
                    Top = 1,
                    Bottom = 1,
                    Left = 1,
                    Right = 1
                }
            };

            var room1 = new Node()
            {
                Name = "Destra",
                Texture = "room0001.png",
                Corners = new Corners()
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 0,
                    Right = 1
                }
            };

            var room2 = new Node()
            {
                Name = "Sinistra",
                Texture = "room0002.png",
                Corners = new Corners()
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 1,
                    Right = 0
                }
            };

            var room3 = new Node()
            {
                Name = "Alto",
                Texture = "room0003.png",
                Corners = new Corners()
                {
                    Top = 1,
                    Bottom = 0,
                    Left = 0,
                    Right = 0
                }
            };

            var room4 = new Node()
            {
                Name = "Basso",
                Texture = "room0004.png",
                Corners = new Corners()
                {
                    Top = 0,
                    Bottom = 1,
                    Left = 0,
                    Right = 0
                }
            };

            var room5 = new Node()
            {
                Name = "Left Right",
                Texture = "room0005.png",
                Corners = new Corners()
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 1,
                    Right = 1
                }
            };


            var room6 = new Node()
            {
                Name = "Top Bottom",
                Texture = "room0006.png",
                Corners = new Corners()
                {
                    Top = 1,
                    Bottom = 1,
                    Left = 0,
                    Right = 0
                }
            };

            Nodes.Add(room0);
            Nodes.Add(room1);
            Nodes.Add(room2);
            Nodes.Add(room3);
            Nodes.Add(room4);
            Nodes.Add(room5);
            Nodes.Add(room6);
        }

        private static void InitGrid()
        {
            var baseNode = new Node()
            {
                Name = "void",
                IsCollapsed = false
            };

            for (var x = 0; x < GridWidth; x++)
            {
                for (var y = 0; y < GridHeight; y++)
                {
                    var clone = baseNode.Clone();
                    clone.x = x;
                    clone.y = y;
                    clone.Texture = "room0014.png";
                    Grid[x, y] = clone;
                    
                    Console.WriteLine($"Generated at [{x}, {y}]");
                }
            }
        }

        private static void AddToProcess(Node node)
        {
            if (NodesToProcess.Contains(node))
                return;
            NodesToProcess.Add(node);
        }

        private static void CheckNeighbors(int x, int y)
        {
            if (x > 0)
                if (!Grid[x - 1, y].IsCollapsed)
                    AddToProcess(Grid[x - 1, y]);
            if (x < GridHeight - 1)
                if (!Grid[x + 1, y].IsCollapsed)
                    AddToProcess(Grid[x + 1, y]);
            if (y > 0)
                if (!Grid[x, y - 1].IsCollapsed)
                    AddToProcess(Grid[x, y - 1]);
            if (y < GridWidth - 1)
                if (!Grid[x, y + 1].IsCollapsed)
                    AddToProcess(Grid[x, y + 1]);
        }

        private static Dictionary<string, Node> GetNeighbors(int x, int y)
        {
            var result = new Dictionary<string, Node>();
            if (x > 0)
                if(Grid[x - 1, y].IsCollapsed) result.Add("Top", Grid[x - 1, y]);
            if (x < GridHeight - 1)
                if( Grid[x + 1, y].IsCollapsed) result.Add("Bottom", Grid[x + 1, y]);
            if (y > 0)
                if(Grid[x, y - 1].IsCollapsed) result.Add("Left", Grid[x, y - 1]);
            if (y < GridWidth - 1)
                if(Grid[x, y + 1].IsCollapsed) result.Add("Right", Grid[x, y + 1]);
            return result;
        }

        private static bool IsCompatible(Node node, Dictionary<string, Node> nodes)
        {
            var result = true;
            
            foreach (var neighbor in nodes)
            {
                switch (neighbor.Key)
                {
                    case "Top":
                        if(node.Corners.Top != 1)
                }
            }
    
            return result;

        }

        private static void Collapse(Node current)
        {
            var avaiableNodes = Nodes.ToList();
            var compatibleNodes = new List<Node>();
            var x = current.x;
            var y = current.y;
            
            var neighbors = GetNeighbors(x, y);

            foreach (var node in avaiableNodes)
            {
                if(IsCompatible(node, neighbors))
                    compatibleNodes.Add(node);
            }

            if (compatibleNodes.Count > 0)
            {
                var randomNode = random.Next(0, compatibleNodes.Count);
                var node = compatibleNodes[randomNode].Clone();
                node.x = x;
                node.y = y;
                node.IsCollapsed = true;
                Grid[x, y] = node;
                Console.WriteLine($"Collapsed at [{x}, {y}]: {node.Name}");
            }
            else
            {
                var node = Grid[x, y].Clone();
                node.x = x;
                node.y = y;
                node.IsCollapsed = true;
                node.Corners = new Corners()
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 0,
                    Right = 0
                };
                node.Texture = "room0014.png";
                Grid[x, y] = node;
                Console.WriteLine($"Collapsed at [{x}, {y}]: {node.Name}");
            }
           
            CheckNeighbors(x, y);
        }
    }
}