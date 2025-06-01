using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionCollapse
{

    public class Corners
    {
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }

    public class Node {

        public string Name { get; set; }
        public string Texture { get; set; }
        public bool IsCollapsed { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        
        public Corners Corners { get; set; }

        public Node Clone()
        {
            return (Node)MemberwiseClone();
        }
    }

}
