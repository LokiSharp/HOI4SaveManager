namespace Core.Node;

public abstract partial class Node
{
    public abstract string ToStringIfWithIndent(bool indent);
    public abstract string ToStringIfWithIndent(int indentLevel);
}