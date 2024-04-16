namespace Core.Node;

public partial class ValueNode
{
    public override string ToString() => IsQuoted ? ToQuotedString() : ToUnquotedString();
    public override string ToStringIfWithIndent(bool indent) => ToString();
    public override string ToStringIfWithIndent(int indentLevel) => ToString();
    private string ToQuotedString() => $"\"{Value}\"";
    private string ToUnquotedString() => Value;
}