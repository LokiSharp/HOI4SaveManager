namespace Core.Node;

public class IntNode(int value) : Node
{
    public int Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
}