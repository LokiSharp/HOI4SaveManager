namespace Core.Node;

public class AssignmentNode : Node
{
    public ValueNode Name { get; init; }
    public OperatorNode Operator { get; init; }
    public Node Value { get; init; }

    public override string ToString()
    {
        return ToString(false);
    }

    public override string ToString(bool indent)
    {
        var valueString = Value.ToString(indent);
        return $"{Name} {Operator.Value} {valueString}";
    }

    public override string ToString(int indentLevel)
    {
        var valueString = Value.ToString(indentLevel);
        return $"{Name.Value} {Operator.Value} {valueString}";
    }
}