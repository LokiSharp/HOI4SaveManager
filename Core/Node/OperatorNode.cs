namespace Core.Node;

public partial class OperatorNode(string value, OperatorType operatorType)
    : Node
{
    public string Value { get; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;
}