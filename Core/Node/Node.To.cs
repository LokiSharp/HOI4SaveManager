namespace Core.Node;

public abstract partial class Node
{
    public abstract string ToString(bool indent);
    public abstract string ToString(int indentLevel);
}