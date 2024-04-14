namespace Core.Node;

public class OperatorNode(string value, OperatorType operatorType)
    : Node
{
    public string Value { get; private set; } = value;
    public OperatorType OperatorType { get; private set; } = operatorType;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
}