namespace Core.Node;

public partial class ArrayNode
{
    public override string ToString() => ToStringIfWithIndent(false);

    public override string ToStringIfWithIndent(bool indent)
    {
        return indent ? ToStringIfWithIndent(0) : $"{{{string.Join(" ", _list.Select(child => child.ToString()))}}}";
    }

    public override string ToStringIfWithIndent(int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        var children = _list.Select(child => child.ToStringIfWithIndent(indentLevel + 1));
        var separator = "\n\t" + indent;

        return $"{{{separator}{string.Join(separator, children)}\n{indent}}}";
    }
}