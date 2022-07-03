using System.Text;

namespace nary_node1;

public class NaryNode<T>
{
    #region Properties

    public IList<NaryNode<T>> Children { get; }

    public T Value { get; set; }

    #endregion

    public NaryNode(T value)
    {
        Value = value;
        Children = new List<NaryNode<T>>();
    }

    public void AddChild(NaryNode<T> child)
    {
        Children.Add(child);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{Value}:");

        foreach (var child in Children) sb.Append($" {child.Value}");

        return sb.ToString();
    }
}