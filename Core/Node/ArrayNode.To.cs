namespace Core.Node;

public partial class ArrayNode
{
    public override string ToString()
    {
        return ToString(false);
    }

    public override string ToString(bool indent)
    {
        if (indent)
        {
            return ToString(0);
        }

        return $"{{{string.Join(" ", _list.Select(child => child.ToString()))}}}";
    }

    public override string ToString(int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        var children = _list.Select(child => child.ToString(indentLevel + 1));
        var separator = "\n\t" + indent;

        return $"{{{separator}{string.Join(separator, children)}\n{indent}}}";
    }
}