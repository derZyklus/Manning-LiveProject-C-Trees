using System.Windows;

namespace nary_node5;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Build a test tree.
        // A
        //         |
        //     +---+---+
        // B   C   D
        //     |       |
        //    +-+      +
        // E F      G
        //    |        |
        //    +      +-+-+
        // H      I J K
        var nodeA = new NaryNode<string>("A");
        var nodeB = new NaryNode<string>("B");
        var nodeC = new NaryNode<string>("C");
        var nodeD = new NaryNode<string>("D");
        var nodeE = new NaryNode<string>("E");
        var nodeF = new NaryNode<string>("F");
        var nodeG = new NaryNode<string>("G");
        var nodeH = new NaryNode<string>("H");
        var nodeI = new NaryNode<string>("I");
        var nodeJ = new NaryNode<string>("J");
        var nodeK = new NaryNode<string>("K");

        nodeA.AddChild(nodeB);
        nodeA.AddChild(nodeC);
        nodeA.AddChild(nodeD);
        nodeB.AddChild(nodeE);
        nodeB.AddChild(nodeF);
        nodeD.AddChild(nodeG);
        nodeE.AddChild(nodeH);
        nodeG.AddChild(nodeI);
        nodeG.AddChild(nodeJ);
        nodeG.AddChild(nodeK);

        // Draw the tree.
        nodeA.ArrangeAndDrawSubtree(MainCanvas, 10, 10);
    }
}