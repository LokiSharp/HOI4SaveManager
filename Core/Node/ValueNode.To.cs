namespace Core.Node;

public partial class ValueNode
{
    public override string ToString()
    {
        return IsQuoted ? ToQuotedString() : ToUnquotedString();
    }

    public override string ToStringIfWithIndent(bool indent) => ToString();
    public override string ToStringIfWithIndent(int indentLevel) => ToString();

    private string ToQuotedString()
    {
        return $"\"{Value}\"";
    }

    private string ToUnquotedString()
    {
        return Value;
    }
}