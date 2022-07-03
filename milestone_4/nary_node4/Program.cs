﻿using System.Text;
using nary_node4;

internal class Program
{
    private static void Main(string[] args)
    {
        var root = new NaryNode<string>("Root");
        var a = new NaryNode<string>("A");
        var b = new NaryNode<string>("B");
        var c = new NaryNode<string>("C");
        var d = new NaryNode<string>("D");
        var e = new NaryNode<string>("E");
        var f = new NaryNode<string>("F");
        var g = new NaryNode<string>("G");
        var h = new NaryNode<string>("H");
        var i = new NaryNode<string>("I");

        root.AddChild(a);
        root.AddChild(b);
        root.AddChild(c);

        a.AddChild(d);
        a.AddChild(e);

        d.AddChild(g);

        c.AddChild(f);

        f.AddChild(h);
        f.AddChild(i);

        PrintResults("Preorder", root.TraversePreorder());
        PrintResults("Postorder", root.TraversePostorder());
        PrintResults("Breadth First", root.TraverseBreadthFirst());
    }

    private static void PrintResults(string functionName, IEnumerable<NaryNode<string>> items)
    {
        functionName += ":";
        var result = new StringBuilder($"{functionName,-20}");

        foreach (var node in items) result.Append($"{node.Value} ");

        Console.WriteLine(result);
    }
}