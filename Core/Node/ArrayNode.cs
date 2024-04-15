namespace Core.Node;

public partial class ArrayNode : Node
{
    private List<Node?>? _list;

    public ArrayNode(params Node?[] items)
    {
        InitializeFromArray(items);
    }

    private void InitializeFromArray(Node?[] items)
    {
        var list = new List<Node?>(items);

        foreach (var item in items)
        {
            item?.AssignParent(this);
        }

        _list = list;
    }

    internal Node? GetItem(int index)
    {
        return List[index];
    }

    internal void SetItem(int index, Node? value)
    {
        value?.AssignParent(this);
        DetachParent(List[index]);
        List[index] = value;
    }
}