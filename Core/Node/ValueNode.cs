namespace Core.Node;

public partial class ValueNode(string value, bool isQuoted = false) : Node
{
    public string Value { get; private set; } = value;
    public bool IsQuoted { get; private set; } = isQuoted;
};