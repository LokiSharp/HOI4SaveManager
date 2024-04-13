namespace Core.Text;

public abstract class Node(NodeType type)
{
    public readonly HashSet<Node> ChildNodes = [];
    public NodeType Type { get; private set; } = type;

    public bool HasChildNode() => ChildNodes.Count > 0;
}

public class StringNode(string value) : Node(NodeType.String)
{
    public string Value { get; private set; } = value;
};

public class IntNode(int value) : Node(NodeType.Int)
{
    public int Value { get; private set; } = value;
};

public class RealNode(double value) : Node(NodeType.Real)
{
    public double Value { get; private set; } = value;
};

public class OperatorNode(string value, OperatorType operatorType) : Node(NodeType.Operator)
{
    public string Value { get; private set; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;
};

public class ObjectNode() : Node(NodeType.Object);

public class ArrayNode() : Node(NodeType.Array);