namespace Core.Node;

public partial class OperatorNode
{
    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToStringIfWithIndent(bool indent) => ToString();
    public override string ToStringIfWithIndent(int indentLevel) => ToString();
}