using System.Text;

namespace nary_node2;

public class NaryNode<T>
{
    #region Properties

    public IList<NaryNode<T>> Children { get; }

    public T Value { get; set; }

    #endregion

    private const string Whitespace = "  ";

    public NaryNode(T value)
    {
        Value = value;
        Children = new List<NaryNode<T>>();
    }

    public void AddChild(NaryNode<T> child)
    {
        Children.Add(child);
    }

    public NaryNode<T> FindNode(T targetValue)
    {
        if (Value.Equals(targetValue))
            return this;

        return Children.Select(child => child.FindNode(targetValue))
            .FirstOrDefault(foundNode => foundNode != null);
    }

    public override string ToString()
    {
        return ToString("");
    }

    public string ToString(string spaces)
    {
        var sb = new StringBuilder();
        sb.Append($"{spaces}{Value}:\n");

        foreach (var child in Children) sb.Append($"{child.ToString(spaces + Whitespace)}");

        return sb.ToString();
    }
}