using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace nary_node5;

public class NaryNode<T>
{
    private const string Whitespace = "  ";

    private const double NODE_RADIUS = 10.0;
    private const double X_SPACING = 20.0;
    private const double Y_SPACING = 30.0;

    public NaryNode(T value)
    {
        Value = value;
        Children = new List<NaryNode<T>>();
    }

    public Rect SubtreeBounds { get; private set; }
    public Point Center { get; private set; }

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

    #region Properties

    public IList<NaryNode<T>> Children { get; }

    public T Value { get; set; }

    #endregion

    public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
    {
        ArrangeSubtree(xmin, xmin);
        DrawSubtreeLinks(canvas);
        DrawSubtreeNodes(canvas);
    }

    private void ArrangeSubtree(double xmin, double ymin)
    {
        if (Value.Equals("G"))
        {
            var aaa = "Hallo";
        }

        if (Children.Count == 0)
        {
            Center = new Point(xmin + NODE_RADIUS, ymin + NODE_RADIUS);
            SubtreeBounds = new Rect(xmin, ymin, NODE_RADIUS * 2, NODE_RADIUS * 2);
            return;
        }

        // Set child_xmin and child_ymin to the
        // start position for child subtrees.
        double child_xmin = xmin;
        double child_ymin = ymin + 2 * NODE_RADIUS + Y_SPACING;

        // Set ymax equal to the largest Y position used.
        double ymax = ymin + 2 * NODE_RADIUS;

        double maxSubtreeHight = 0;
        // Position the child subtrees.
        foreach (NaryNode<T> child in Children)
        {
            // Position this child subtree.
            child.ArrangeSubtree(child_xmin, child_ymin);

            // Update child_xmin to allow room for the subtree
            // and space between the subtrees.
            child_xmin += X_SPACING;

            child_xmin += child.SubtreeBounds.Width;
            var bbb = child.SubtreeBounds;
            // Update the subtree bottom ymax.
            ymax = child.SubtreeBounds.Bottom > ymax ? child.SubtreeBounds.Bottom : ymax;
        }



        if (Value.Equals("D"))
        {
            var aaa = "Hallo";
        }

        double xmax = child_xmin - X_SPACING;
        SubtreeBounds = new Rect(xmin, ymin, xmax-xmin, ymax-ymin);
        Center = new Point(xmin + ((xmax - xmin)/2), ymin + NODE_RADIUS);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        if (Children.Count == 1)
        {
            // ...
            Children[0].DrawSubtreeLinks(canvas);
        }
        else if(Children.Count > 0)
        {

            foreach (var child in Children)
            {
                child.DrawSubtreeLinks(canvas);
            }
        }

        //if (!Value.Equals("G") && !Value.Equals("A"))
        //{
        //    var aaa = "Hallo";
        //    return;
        //}
        canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
        var nodeTopLeftPoint = new Point(Center.X - NODE_RADIUS, Center.Y - NODE_RADIUS);
        var nodeSize = new Size(NODE_RADIUS * 2, NODE_RADIUS * 2);
        var nodeRect = new Rect(nodeTopLeftPoint, nodeSize);

        canvas.DrawEllipse(nodeRect, Brushes.White, Brushes.Black, 1);
        canvas.DrawLabel(nodeRect, Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center,
            VerticalAlignment.Center, 10, 0);

        foreach (var child in Children)
        {
            child.DrawSubtreeNodes(canvas);
        }
    }
}