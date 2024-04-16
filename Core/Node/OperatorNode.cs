namespace Core.Node;

public partial class OperatorNode(string value, OperatorType operatorType)
    : Node
{
    public string Value { get; private set; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;
}