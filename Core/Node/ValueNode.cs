namespace Core.Node;

public partial class ValueNode(string value, bool isQuoted = false) : Node
{
    public string Value { get; } = value;
    public bool IsQuoted { get; } = isQuoted;
}