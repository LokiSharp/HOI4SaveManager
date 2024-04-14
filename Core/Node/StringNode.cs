namespace Core.Node;

public class StringNode(string value, bool isQuoted) : Node
{
    public string Value { get; private set; } = value;
    public bool IsQuoted { get; private set; } = isQuoted;

    public override string ToString()
    {
        return IsQuoted ? ToQuotedString() : ToUnquotedString();
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();

    public string ToQuotedString()
    {
        return $"\"{Value}\"";
    }

    public string ToUnquotedString()
    {
        return Value;
    }
}