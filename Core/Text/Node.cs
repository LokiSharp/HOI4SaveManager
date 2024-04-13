namespace Core.Text;

public abstract class Node(NodeType type)
{
    public NodeType Type { get; private set; } = type;
}

public class StringNode(string value) : Node(NodeType.String)
{
    public string Value { get; private set; } = value;

    public override string ToString()
    {
        return $"\"{Value}\"";
    }
};

public class IntNode(int value) : Node(NodeType.Int)
{
    public int Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }
};

public class RealNode(double value) : Node(NodeType.Real)
{
    public double Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }
};

public class OperatorNode(string value, OperatorType operatorType) : Node(NodeType.Operator)
{
    public string Value { get; private set; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;

    public override string ToString()
    {
        return $"{Value}";
    }
};

public class AssignmentNode() : Node(NodeType.Assignment)
{
    public Node Name { get; set; }
    public OperatorNode Operator { get; set; }
    public Node Value { get; set; }

    public override string ToString()
    {
        return $"{Name} {Operator.Value} {Value}";
    }
};

public class ArrayNode() : Node(NodeType.Array)
{
    public readonly HashSet<Node> ChildNodes = [];
    public bool HasChildNode() => ChildNodes.Count > 0;

    public override string ToString()
    {
        return $"{{\n{string.Join("\n", ChildNodes.Select(child => child.ToString()))}\n}}";
    }
};