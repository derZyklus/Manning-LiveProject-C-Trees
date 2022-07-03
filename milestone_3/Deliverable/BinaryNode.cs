using System.Text;

namespace binary_node3;

public class BinaryNode<T>
{
    #region Properties

    public BinaryNode<T> LeftChild { get; private set; }
    public BinaryNode<T> RightChild { get; private set; }
    public T Value { get; set; }

    #endregion

    private const string Whitespace = "  ";

    public BinaryNode(T value)
    {
        Value = value;
        LeftChild = null;
        RightChild = null;
    }

    public void AddLeft(BinaryNode<T> leftChild)
    {
        LeftChild = leftChild;
    }

    public void AddRight(BinaryNode<T> rightChild)
    {
        RightChild = rightChild;
    }

    public BinaryNode<T> FindNode(T targetValue)
    {
        if (Value.Equals(targetValue))
            return this;

        if (LeftChild != null)
        {
            var resultFindLeft = LeftChild.FindNode(targetValue);
            if (resultFindLeft != null)
                return resultFindLeft;
        }

        if (RightChild != null)
        {
            var resultFindRight = RightChild.FindNode(targetValue);
            if (resultFindRight != null)
                return resultFindRight;
        }

        return null;
    }

    public override string ToString()
    {
        return ToString("");
    }

    public string ToString(string spaces)
    {
        var sb = new StringBuilder();
        sb.Append($"{spaces}{Value}:\n");

        if (LeftChild == null && RightChild == null) return sb.ToString();

        if (LeftChild == null)
            sb.Append($"{spaces}{Whitespace}null\n");
        else
            sb.Append(LeftChild.ToString(spaces + Whitespace));

        if (RightChild == null)
            sb.Append($"{spaces}{Whitespace}null\n");
        else
            sb.Append(RightChild.ToString(spaces + Whitespace));

        return sb.ToString();
    }
}