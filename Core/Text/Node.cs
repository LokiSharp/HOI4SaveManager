namespace Core.Text;

public abstract class Node(NodeType type)
{
    public NodeType Type { get; private set; } = type;

    public abstract string ToString(bool indent);
    public abstract string ToString(int indentLevel);
}

public class StringNode(string value, bool isQuoted) : Node(NodeType.String)
{
    public string Value { get; private set; } = value;
    public bool IsQuoted { get; private set; } = isQuoted;

    public override string ToString()
    {
        return IsQuoted ? ToQuotedString() : ToUnquotedString();
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();

    public string ToQuotedString()
    {
        return $"\"{Value}\"";
    }

    public string ToUnquotedString()
    {
        return Value;
    }
};

public class IntNode(int value) : Node(NodeType.Int)
{
    public int Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
};

public class RealNode(double value) : Node(NodeType.Real)
{
    public double Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
};

public class OperatorNode(string value, OperatorType operatorType) : Node(NodeType.Operator)
{
    public string Value { get; private set; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
};

public class AssignmentNode() : Node(NodeType.Assignment)
{
    public StringNode Name { get; init; }
    public OperatorNode Operator { get; init; }
    public Node Value { get; init; }

    public override string ToString()
    {
        return ToString(false);
    }

    public override string ToString(bool indent)
    {
        var valueString = Value.ToString(indent);
        return $"{Name} {Operator.Value} {valueString}";
    }

    public override string ToString(int indentLevel)
    {
        var valueString = Value.ToString(indentLevel);
        return $"{Name.ToUnquotedString()} {Operator.Value} {valueString}";
    }
};

public class ArrayNode() : Node(NodeType.Array)
{
    public readonly HashSet<Node> ChildNodes = [];
    public bool HasChildNode() => ChildNodes.Count > 0;

    public override string ToString()
    {
        return ToString(false);
    }

    public override string ToString(bool indent)
    {
        if (indent)
        {
            return ToString(0);
        }

        return $"{{{string.Join(" ", ChildNodes.Select(child => child.ToString()))}}}";
    }

    public override string ToString(int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        var children = ChildNodes.Select(child => child.ToString(indentLevel + 1));
        var separator = "\n\t" + indent;

        return $"{{{separator}{string.Join(separator, children)}\n{indent}}}";
    }
};

public class RootNode : ArrayNode
{
    public override string ToString()
    {
        return ToString(false);
    }

    public override string ToString(bool indent)
    {
        if (indent)
        {
            return ToString(0);
        }

        return $"{string.Join(" ", ChildNodes.Select(child => child.ToString()))}";
    }

    public override string ToString(int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        var children = ChildNodes.Select(child => child.ToString(indentLevel));
        var separator = "\n" + indent;

        return $"{separator}{string.Join(separator, children)}{indent}";
    }
}