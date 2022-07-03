using System.Text;

namespace binary_node4;

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


        PrintResults("Preorder", root.TraversePreorder());
        PrintResults("Inorder", root.TraverseInorder());
        PrintResults("Postorder", root.TraversePostorder());
        PrintResults("Breadth First", root.TraverseBreadthFirst());
    }

    private static void PrintResults(string functionName, IEnumerable<BinaryNode<string>> items)
    {
        functionName += ":";
        var result = new StringBuilder($"{functionName,-20}");

        foreach (var node in items) result.Append($"{node.Value} ");

        Console.WriteLine(result);
    }
}