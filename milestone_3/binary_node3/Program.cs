namespace binary_node3;

internal class Program
{
    private static void Main(string[] args)
    {
        var root = new BinaryNode<string>("Root");
        var a = new BinaryNode<string>("A");
        var b = new BinaryNode<string>("B");
        var c = new BinaryNode<string>("C");
        var d = new BinaryNode<string>("D");
        var e = new BinaryNode<string>("E");
        var f = new BinaryNode<string>("F");

        root.AddLeft(a);
        root.AddRight(b);
        a.AddLeft(c);
        a.AddRight(d);
        b.AddRight(e);
        e.AddLeft(f);

        // Find some values.
        FindValue(root, "Root");
        FindValue(root, "E");
        FindValue(root, "F");
        FindValue(root, "Q");

        // Find F in the B subtree
        FindValue(b, "F");
    }

    private static void FindValue(BinaryNode<string> startNode, string target)
    {
        var foundNode = startNode.FindNode(target);
        if (foundNode != null)
            Console.WriteLine($"Found {foundNode.Value}");
        else
            Console.WriteLine($"Value {target} not found");
    }
}