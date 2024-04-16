namespace Core.Node;

public partial class AssignmentNode : Node
{
    public ValueNode Name { get; init; }
    public OperatorNode Operator { get; init; }
    public Node Value { get; init; }
}