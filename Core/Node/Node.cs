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
        if (this is not ArrayNode node) throw new Exception();
        return node;
    }

    public ValueNode AsValue()
    {
        if (this is not ValueNode node) throw new Exception();
        return node;
    }

    public AssignmentNode AsAssignment()
    {
        if (this is not AssignmentNode node) throw new Exception();
        return node;
    }
}