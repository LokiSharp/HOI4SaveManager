namespace Core.Node;

public abstract partial class Node
{
    public Node? Parent { get; internal set; }

    public Node? Root
    {
        get
        {
            var parent = Parent;
            if (parent == null)
            {
                return this;
            }

            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }

            return parent;
        }
    }

    internal void AssignParent(Node parent)
    {
        if (Parent != null)
        {
            throw new Exception();
        }

        var p = parent;
        while (p != null)
        {
            if (p == this)
            {
                throw new Exception();
            }

            p = p.Parent;
        }

        Parent = parent;
    }

    public ArrayNode AsArray()
    {
        var array = this as ArrayNode;

        if (array is null) throw new Exception();

        return array;
    }
}