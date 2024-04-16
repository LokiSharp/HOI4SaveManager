using System.Collections;

namespace Core.Node;

public sealed partial class ArrayNode : IList<Node?>
{
    internal List<Node?> List => _list ?? [];

    public IEnumerator<Node?> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Node? item)
    {
        item?.AssignParent(this);
        List.Add(item);
    }

    public void Clear()
    {
        var list = _list;

        if (list is null) return;
        foreach (var t in list)
        {
            DetachParent(t);
        }

        list.Clear();
    }

    public bool Contains(Node? item)
    {
        return List.Contains(item);
    }

    public void CopyTo(Node?[] array, int arrayIndex)
    {
        List.CopyTo(array, arrayIndex);
    }

    public bool Remove(Node? item)
    {
        if (!List.Remove(item)) return false;
        DetachParent(item);
        return true;
    }

    public int Count => List.Count;
    public bool IsReadOnly => false;

    public int IndexOf(Node? item)
    {
        return List.IndexOf(item);
    }

    public void Insert(int index, Node? item)
    {
        item?.AssignParent(this);
        List.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        var item = List[index];
        List.RemoveAt(index);
        DetachParent(item);
    }

    public Node? this[int index]
    {
        get => GetItem(index);
        set => SetItem(index, value);
    }

    private static void DetachParent(Node? item)
    {
        if (item != null)
        {
            item.Parent = null;
        }
    }
}