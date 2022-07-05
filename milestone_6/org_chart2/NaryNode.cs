using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace org_chart2;

public class NaryNode<T>
{
    private const string Whitespace = "  ";

    private const double BOX_HALF_WIDTH = 80 / 2;
    private const double BOX_HALF_HEIGHT = 40 / 2;
    private const double X_SPACING = 20.0;
    private const double Y_SPACING = 30.0;

    public NaryNode(T value)
    {
        Value = value;
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

    public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
    {
        ArrangeSubtree(xmin, xmin);
        DrawSubtreeLinks(canvas);
        DrawSubtreeNodes(canvas);
    }

    private void ArrangeSubtree(double xmin, double ymin)
    {
        double xmax = 0;
        double ymax = 0;
        double child_xmin = 0;
        double child_ymin = 0;

        if (IsLeaf())
        {
            Center = new Point(xmin + BOX_HALF_WIDTH, ymin + BOX_HALF_HEIGHT);
            SubtreeBounds = new Rect(xmin, ymin, BOX_HALF_WIDTH * 2, BOX_HALF_HEIGHT * 2);
            return;
        }

        if (IsTwig())
        {
            child_xmin = xmin + X_SPACING;
            child_ymin = ymin + 2 * BOX_HALF_HEIGHT;

            foreach (var child in Children)
            {
                child_ymin += Y_SPACING;

                child.ArrangeSubtree(child_xmin, child_ymin);

                child_ymin += 2 * BOX_HALF_HEIGHT;
            }

            xmax = child_xmin + 2 * BOX_HALF_WIDTH;
            ymax = child_ymin;

            Center = new Point(xmin + BOX_HALF_WIDTH, ymin + BOX_HALF_HEIGHT);
            SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
            return;
        }

        // Set child_xmin and child_ymin to the
        // start position for child subtrees.
        child_xmin = xmin;
        child_ymin = ymin + 2 * BOX_HALF_HEIGHT + Y_SPACING;


        // Set ymax equal to the largest Y position used.
        ymax = ymin + 2 * BOX_HALF_HEIGHT;

        // Position the child subtrees.
        foreach (var child in Children)
        {
            // Position this child subtree.
            child.ArrangeSubtree(child_xmin, child_ymin);

            // Update child_xmin to allow room for the subtree
            // and space between the subtrees.
            child_xmin += X_SPACING;
            child_xmin += child.SubtreeBounds.Width;

            // Update the subtree bottom ymax.
            ymax = child.SubtreeBounds.Bottom > ymax ? child.SubtreeBounds.Bottom : ymax;
        }

        xmax = child_xmin - X_SPACING;
        SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        Center = new Point(xmin + (xmax - xmin) / 2, ymin + BOX_HALF_HEIGHT);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        if (IsTwig())
        {
            var ymin = Center.Y;
            var ymax = ymin + Children.Count * (BOX_HALF_HEIGHT * 2 + Y_SPACING);
            var startPoint = new Point(SubtreeBounds.X + X_SPACING / 2, ymin);
            var endPoint = new Point(SubtreeBounds.X + X_SPACING / 2, ymax);

            canvas.DrawLine(startPoint, endPoint, Brushes.Green, 1);

            for (var i = 0; i < Children.Count; i++)
            {
                ymin += BOX_HALF_HEIGHT * 2 + Y_SPACING;
                startPoint = new Point(SubtreeBounds.X + X_SPACING / 2, ymin);
                endPoint = new Point(SubtreeBounds.X + X_SPACING, ymin);

                canvas.DrawLine(startPoint, endPoint, Brushes.Green, 1);
            }

            foreach (var child in Children) child.DrawSubtreeLinks(canvas);
        }
        else
        {
            if (Children.Count == 1)
            {
                canvas.DrawLine(Center, Children[0].Center, Brushes.Green, 1);
                Children[0].DrawSubtreeLinks(canvas);
            }
            else if (Children.Count > 0)
            {
                // Find the Y coordinate of the center
                // halfway to the children.
                var yHalfwayOfCenter = (Children[0].Center.Y - Center.Y) / 2;

                // Draw the vertical line to the center line.
                canvas.DrawLine(Center, new Point(Center.X, Center.Y + yHalfwayOfCenter), Brushes.Green, 1);

                // Draw the horizontal center line over the children.
                var startHorizontalLine = new Point(Children[0].Center.X, Center.Y + yHalfwayOfCenter);
                var endHorizontalLine = new Point(Children.Last().Center.X, Center.Y + yHalfwayOfCenter);
                canvas.DrawLine(startHorizontalLine, endHorizontalLine, Brushes.Green, 1);

                // Draw lines from the center line to the children.
                foreach (var child in Children)
                    canvas.DrawLine(child.Center, new Point(child.Center.X, child.Center.Y - yHalfwayOfCenter),
                        Brushes.Green, 1);

                foreach (var child in Children) child.DrawSubtreeLinks(canvas);
            }
        }

        canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
        var nodeTopLeftPoint = new Point(Center.X - BOX_HALF_WIDTH, Center.Y - BOX_HALF_HEIGHT);
        var nodeSize = new Size(BOX_HALF_WIDTH * 2, BOX_HALF_HEIGHT * 2);
        var nodeRect = new Rect(nodeTopLeftPoint, nodeSize);


        var backgroundBrush = IsLeaf() ? Brushes.White : Brushes.Pink;

        canvas.DrawRectangle(new Rect(nodeTopLeftPoint, new Size(BOX_HALF_WIDTH * 2, BOX_HALF_HEIGHT * 2)),
            backgroundBrush, Brushes.Black, 2);

        canvas.DrawLabel(nodeRect, Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center,
            VerticalAlignment.Center, 12, 0);

        foreach (var child in Children) child.DrawSubtreeNodes(canvas);
    }

    public bool IsLeaf()
    {
        return !Children.Any();
    }

    public bool IsTwig()
    {
        if (IsLeaf()) return false;

        return Children.All(child => child.IsLeaf());
    }

    #region Properties

    public IList<NaryNode<T>> Children { get; } = new List<NaryNode<T>>();

    public T Value { get; set; }

    #endregion
}