using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Xsl;

namespace binary_node5;

public class BinaryNode<T>
{
    #region Properties

    public Point Center { get; private set; }
    public BinaryNode<T> LeftChild { get; private set; }
    public BinaryNode<T> RightChild { get; private set; }
    public Rect SubtreeBounds { get; private set; }
    public T Value { get; set; }

    #endregion

    private const string Whitespace = "  ";

    private const double NODE_RADIUS = 10.0;
    private const double X_SPACING = 20.0;
    private const double Y_SPACING = 30.0;

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

    public IEnumerable<BinaryNode<T>> TraversePreorder()
    {
        var result = new List<BinaryNode<T>>();

        result.Add(this);

        if (LeftChild != null)
            result.AddRange(LeftChild.TraversePreorder());

        if (RightChild != null)
            result.AddRange(RightChild.TraversePreorder());

        return result;
    }

    public IEnumerable<BinaryNode<T>> TraverseInorder()
    {
        var result = new List<BinaryNode<T>>();

        if (LeftChild != null)
            result.AddRange(LeftChild.TraverseInorder());

        result.Add(this);

        if (RightChild != null)
            result.AddRange(RightChild.TraverseInorder());

        return result;
    }

    public IEnumerable<BinaryNode<T>> TraversePostorder()
    {
        var result = new List<BinaryNode<T>>();

        if (LeftChild != null)
            result.AddRange(LeftChild.TraversePostorder());

        if (RightChild != null)
            result.AddRange(RightChild.TraversePostorder());

        result.Add(this);

        return result;
    }

    public IEnumerable<BinaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<BinaryNode<T>>();
        var queue = new Queue<BinaryNode<T>>();

        queue.Enqueue(this);

        while (queue.Any())
        {
            var currentNode = queue.Dequeue();

            result.Add(currentNode);

            if (currentNode.LeftChild != null)
                queue.Enqueue(currentNode.LeftChild);

            if (currentNode.RightChild != null)
                queue.Enqueue(currentNode.RightChild);
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

    public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
    {
        ArrangeSubtree(xmin, xmin);
        DrawSubtreeLinks(canvas);
        DrawSubtreeNodes(canvas);
    }

    private void ArrangeSubtree(double xmin, double ymin)
    {
        // Calculate cy, the Y coordinate for this node.
        // This doesn't depend on the children.

        // ..

        if (Value.Equals("C"))
        {
            var ttt = "Hallo Ingo";
        }

        // If the node has no children, just place it here and return.
        if (LeftChild == null && RightChild == null)
        {
            Center = new Point(xmin + NODE_RADIUS, ymin + NODE_RADIUS);
            SubtreeBounds = new Rect(xmin, ymin, NODE_RADIUS * 2, NODE_RADIUS * 2);

            return;
        }

        // Set child_xmin ans child_ymin to the startposition
        // for child subtrees.
        var childXmin = xmin;
        var childYmin = ymin + 2 * NODE_RADIUS + Y_SPACING;

        // Position the child subtrees.
        if (LeftChild != null)
        {
            // Arrange the left child subtree and update
            // child_xmin to allow room for its subtree.

            // ...
            LeftChild.ArrangeSubtree(childXmin, childYmin);
            
            childXmin += (NODE_RADIUS * 2);

            // If eh also have a right child,
            // add space between their subtrees.

            // ...
            if (RightChild != null)
            {
                childXmin += X_SPACING;
            }
        }

        if (RightChild != null)
        {
            // Arrange the child subtree.

            // ...
            RightChild.ArrangeSubtree(childXmin, childYmin);
        }

        if (Value.Equals("C"))
        {
            var ttt = "Hallo Ingo";
        }

        // Arrange this node depending on the number of children.
        if (LeftChild != null && RightChild != null)
        {
            // Two children. Chenter this node over the child nodes.
            // Use the subtree bounds to set our subtree bounds.

            // ...
            var width = X_SPACING + LeftChild.SubtreeBounds.Width + RightChild.SubtreeBounds.Width;
            var maxChildHight = LeftChild.SubtreeBounds.Height > RightChild.SubtreeBounds.Height ? LeftChild.SubtreeBounds.Height : RightChild.SubtreeBounds.Height;
            var height = (2*NODE_RADIUS) + Y_SPACING + maxChildHight;

            SubtreeBounds = new Rect(xmin, xmin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
        else if (LeftChild != null)
        {
            // We have only a left child

            // ...
            var width = LeftChild.SubtreeBounds.Width;
            var height = LeftChild.SubtreeBounds.Height + Y_SPACING + (2*NODE_RADIUS);

            SubtreeBounds = new Rect(xmin, ymin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
        else
        {
            // We have only a right child.

            // ..

            //Center = new Point(xmin + NODE_RADIUS, ymin + NODE_RADIUS);
            //SubtreeBounds = new Rect(xmin, ymin, NODE_RADIUS * 2, NODE_RADIUS * 2);

            var width = RightChild.SubtreeBounds.Width;
            var height = RightChild.SubtreeBounds.Height + Y_SPACING + (2 * NODE_RADIUS);

            SubtreeBounds = new Rect(xmin, ymin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        if (LeftChild != null) canvas.DrawLine(Center, LeftChild.Center, new SolidColorBrush(Colors.Blue), 1);

        if (RightChild != null) canvas.DrawLine(Center, RightChild.Center, new SolidColorBrush(Colors.Blue), 1);

        canvas.DrawRectangle(SubtreeBounds,
            new SolidColorBrush(Colors.Transparent),
            new SolidColorBrush(Colors.Red), 1);

        if (LeftChild != null)
        {
            LeftChild.DrawSubtreeLinks(canvas);
        }

        if (RightChild != null)
        {
            RightChild.DrawSubtreeLinks(canvas);
        }
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
        var nodeTopLeftPoint = new Point(Center.X-NODE_RADIUS, Center.Y - NODE_RADIUS);
        var nodeSize = new Size(NODE_RADIUS * 2, NODE_RADIUS * 2);
        var nodeRect = new Rect(nodeTopLeftPoint, nodeSize);

        canvas.DrawEllipse(nodeRect, Brushes.Transparent, Brushes.Green, 1);

        canvas.DrawLabel(nodeRect, this.Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center,
            VerticalAlignment.Center, 15, 0);

        if (LeftChild != null)
        {
            LeftChild.DrawSubtreeNodes(canvas);
        }

        if (RightChild != null)
        {
            RightChild.DrawSubtreeNodes(canvas);
        }
    }
}