namespace WaveFunctionCollapse;

public class Nodes
{
    public static Node Void = new Node()
    {
        Name = "Void",
        Texture = "void.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 0,
            Left = 0,
            Right = 0
        }
    };

    public static Node All = new Node()
    {
        Name = "All",
        Texture = "room0000.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4
        }
    };

    public static Node Right = new Node()
    {
        Name = "Right",
        Texture = "room0001.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 0,
            Left = 0,
            Right = 4
        }
    };

    public static Node Left = new Node()
    {
        Name = "Left",
        Texture = "room0002.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 0,
            Left = 3,
            Right = 0
        }
    };

    public static Node Top = new Node()
    {
        Name = "Top",
        Texture = "room0003.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 0,
            Left = 0,
            Right = 0
        }
    };

    public static Node Bottom = new Node()
    {
        Name = "Bottom",
        Texture = "room0004.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 2,
            Left = 0,
            Right = 0
        }
    };

    public static Node LeftRight = new Node()
    {
        Name = "Left Right",
        Texture = "room0005.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 0,
            Left = 3,
            Right = 4
        }
    };


    public static Node TopBottom = new Node()
    {
        Name = "Top Bottom",
        Texture = "room0006.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 2,
            Left = 0,
            Right = 0
        }
    };
    
    public static Node LeftRightTop = new Node()
    {
        Name = "Left Right Top",
        Texture = "room0007.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 0,
            Left = 3,
            Right = 4
        }
    };
    
    public static Node LeftTopBottom = new Node()
    {
        Name = "Left Top Bottom",
        Texture = "room0008.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 0
        }
    };
    
    public static Node RightTopBottom = new Node()
    {
        Name = "Right Top Bottom",
        Texture = "room0009.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 2,
            Left = 0,
            Right = 4
        }
    };
    
   public static Node LeftBottom = new Node()
    {
        Name = "LeftBottom",
        Texture = "room0010.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 2,
            Left = 3,
            Right = 4
        }
    };
   
    public static Node TopRight = new Node()
    {
        Name = "TopRight",
        Texture = "room0011.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 0,
            Left = 0,
            Right = 4
        }
    };
    
    public static Node RightBottom = new Node()
    {
        Name = "RightBottom",
        Texture = "room0012.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 2,
            Left = 0,
            Right = 4
        }
    };
    
    public static Node TopLeft = new Node()
    {
        Name = "TopLeft",
        Texture = "room0013.png",
        Corners = new Corners()
        {
            Top = 1,
            Bottom = 0,
            Left = 3,
            Right = 0
        }
    };
    
    public static Node BottomLeftRight = new Node()
    {
        Name = "BottomLeftRight",
        Texture = "room0015.png",
        Corners = new Corners()
        {
            Top = 0,
            Bottom = 2,
            Left = 3,
            Right = 4
        }
    };
    
}