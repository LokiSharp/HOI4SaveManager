namespace Core.Node;

public partial class AssignmentNode
{
    public override string ToString() => ToStringIfWithIndent(false);

    public override string ToStringIfWithIndent(bool indent)
    {
        var valueString = Value.ToStringIfWithIndent(indent);
        return $"{Name} {Operator.Value} {valueString}";
    }

    public override string ToStringIfWithIndent(int indentLevel)
    {
        var valueString = Value.ToStringIfWithIndent(indentLevel);
        return $"{Name.Value} {Operator.Value} {valueString}";
    }
}