namespace Core.Node;

public partial class RealNode(double value) : Node
{
    public double Value { get; private set; } = value;

    public override string ToString()
    {
        return $"{Value}";
    }

    public override string ToString(bool indent) => ToString();
    public override string ToString(int indentLevel) => ToString();
}