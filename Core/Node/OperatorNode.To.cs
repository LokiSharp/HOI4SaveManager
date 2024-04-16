namespace Core.Node;

public partial class OperatorNode
{
    public override string ToString() => $"{Value}";
    public override string ToStringIfWithIndent(bool indent) => ToString();
    public override string ToStringIfWithIndent(int indentLevel) => ToString();
}