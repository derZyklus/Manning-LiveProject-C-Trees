using System.Text;

namespace nary_node4;

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

    public IEnumerable<NaryNode<T>> TraversePreorder()
    {
        var result = new List<NaryNode<T>>();

        result.Add(this);

        foreach (var child in Children)
            result.AddRange(child.TraversePreorder());

        return result;
    }

    public IEnumerable<NaryNode<T>> TraversePostorder()
    {
        var result = new List<NaryNode<T>>();

        foreach (var child in Children)
            result.AddRange(child.TraversePostorder());

        result.Add(this);

        return result;
    }

    public IEnumerable<NaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<NaryNode<T>>();
        var queue = new Queue<NaryNode<T>>();

        queue.Enqueue(this);

        while (queue.Any())
        {
            var currentNode = queue.Dequeue();

            result.Add(currentNode);

            foreach (var child in currentNode.Children) queue.Enqueue(child);
        }

        return result;
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