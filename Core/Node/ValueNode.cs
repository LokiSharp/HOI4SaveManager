namespace Core.Node;

public class ValueNode(string value, bool isQuoted = false) : Node
{
    public string Value { get; private set; } = value;
    public bool IsQuoted { get; private set; } = isQuoted;

    public override string ToString()
    {
        return IsQuoted ? ToQuotedString() : ToUnquotedString();
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();

    private string ToQuotedString()
    {
        return $"\"{Value}\"";
    }

    private string ToUnquotedString()
    {
        return Value;
    }
};