using System.Text;

namespace binary_node1;

public class BinaryNode<T>
{
    #region Properties

    public BinaryNode<T> LeftChild { get; private set; }
    public BinaryNode<T> RightChild { get; private set; }
    public T Value { get; set; }

    #endregion

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

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{Value}:");

        if (LeftChild == null)
            sb.Append(" null");
        else
            sb.Append($" {LeftChild.Value}");

        if (RightChild == null)
            sb.Append(" null");
        else
            sb.Append($" {RightChild.Value}");

        return sb.ToString();
    }
}