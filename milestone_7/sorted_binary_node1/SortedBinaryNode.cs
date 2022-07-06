using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace sorted_binary_node1;

public class SortedBinaryNode<T> where T : IComparable<T>
{
    #region Properties

    public Point Center { get; private set; }
    public SortedBinaryNode<T> LeftChild { get; private set; }
    public SortedBinaryNode<T> RightChild { get; private set; }
    public Rect SubtreeBounds { get; private set; }
    public T Value { get; set; }

    #endregion

    private const string Whitespace = "  ";

    private const double NODE_RADIUS = 10.0;
    private const double X_SPACING = 20.0;
    private const double Y_SPACING = 30.0;

    public SortedBinaryNode(T value)
    {
        Value = value;
        LeftChild = null;
        RightChild = null;
    }

    public SortedBinaryNode<T> FindNode(T targetValue)
    {
        if (targetValue.CompareTo(Value) == 0) return this;

        if (targetValue.CompareTo(Value) < 0)
        {
            if(LeftChild != null)
                return LeftChild.FindNode(targetValue);
        }
        else
        {
            if (RightChild != null)
                return RightChild.FindNode(targetValue);
        }

        return null;
    }

    public IEnumerable<SortedBinaryNode<T>> TraversePreorder()
    {
        var result = new List<SortedBinaryNode<T>>();

        result.Add(this);

        if (LeftChild != null)
            result.AddRange(LeftChild.TraversePreorder());

        if (RightChild != null)
            result.AddRange(RightChild.TraversePreorder());

        return result;
    }

    public IEnumerable<SortedBinaryNode<T>> TraverseInorder()
    {
        var result = new List<SortedBinaryNode<T>>();

        if (LeftChild != null)
            result.AddRange(LeftChild.TraverseInorder());

        result.Add(this);

        if (RightChild != null)
            result.AddRange(RightChild.TraverseInorder());

        return result;
    }

    public IEnumerable<SortedBinaryNode<T>> TraversePostorder()
    {
        var result = new List<SortedBinaryNode<T>>();

        if (LeftChild != null)
            result.AddRange(LeftChild.TraversePostorder());

        if (RightChild != null)
            result.AddRange(RightChild.TraversePostorder());

        result.Add(this);

        return result;
    }

    public IEnumerable<SortedBinaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<SortedBinaryNode<T>>();
        var queue = new Queue<SortedBinaryNode<T>>();

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
                childXmin += LeftChild.SubtreeBounds.Width;
            }
        }

        if (RightChild != null)
        {
            // Arrange the child subtree.

            // ...
            RightChild.ArrangeSubtree(childXmin, childYmin);
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

            SubtreeBounds = new Rect(xmin, ymin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
        else if (LeftChild != null)
        {
            // We have only a left child
            var width = LeftChild.SubtreeBounds.Width;
            var height = LeftChild.SubtreeBounds.Height + Y_SPACING + (2*NODE_RADIUS);

            SubtreeBounds = new Rect(xmin, ymin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
        else 
        {
            // We have only a right child.
            var width = RightChild.SubtreeBounds.Width;
            var height = RightChild.SubtreeBounds.Height + Y_SPACING + (2 * NODE_RADIUS);

            SubtreeBounds = new Rect(xmin, ymin, width, height);
            Center = new Point(xmin + (width / 2), ymin + NODE_RADIUS);
        }
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        if (LeftChild != null)
        {
            canvas.DrawLine(Center, LeftChild.Center, new SolidColorBrush(Colors.Blue), 1);
        }
            

        if (RightChild != null) canvas.DrawLine(Center, RightChild.Center, new SolidColorBrush(Colors.Blue), 1);


        //canvas.DrawRectangle(SubtreeBounds,
        //    new SolidColorBrush(Colors.Transparent),
        //    new SolidColorBrush(Colors.Red), 1);

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

        canvas.DrawEllipse(nodeRect, Brushes.White, Brushes.Green, 1);

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


    public void AddNode(SortedBinaryNode<T> node)
    {
        if(node == null)
            return;

        if (node.Value.CompareTo(this.Value) == 0)
            throw new ArgumentException("Value is already in the tree.");

        if (node.Value.CompareTo(this.Value) < 0)
        {
            if (this.LeftChild == null)
            {
                LeftChild = node;
            }
            else
            {
                LeftChild.AddNode(node);
            }
            return;
        }

        if (this.RightChild == null)
        {
            RightChild = node;
        }
        else
        {
            RightChild.AddNode(node);
        }
    }
}