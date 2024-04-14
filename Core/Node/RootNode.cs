namespace Core.Node;

public class RootNode() : ArrayNode()
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

        return $"{string.Join(" ", _list.Select(child => child.ToString()))}";
    }

    public override string ToString(int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        var children = _list.Select(child => child.ToString(indentLevel));
        var separator = "\n" + indent;

        return $"{separator}{string.Join(separator, children)}{indent}";
    }
}