using System.Windows;

namespace binary_node5;

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
        //         A
        //        / \
        //       /   \
        //      /     \
        //     B       C
        //    / \     / \
        //   D   E   F   G
        //      / \     /
        //     H   I   J
        //            / \
        //           K   L
        var nodeA = new BinaryNode<string>("A");
        var nodeB = new BinaryNode<string>("B");
        var nodeC = new BinaryNode<string>("C");
        var nodeD = new BinaryNode<string>("D");
        var nodeE = new BinaryNode<string>("E");
        var nodeF = new BinaryNode<string>("F");
        var nodeG = new BinaryNode<string>("G");
        var nodeH = new BinaryNode<string>("H");
        var nodeI = new BinaryNode<string>("I");
        var nodeJ = new BinaryNode<string>("J");
        var nodeK = new BinaryNode<string>("K");
        var nodeL = new BinaryNode<string>("L");

        nodeA.AddLeft(nodeB);
        nodeA.AddRight(nodeC);
        nodeB.AddLeft(nodeD);
        nodeB.AddRight(nodeE);
        nodeC.AddLeft(nodeF);
        nodeC.AddRight(nodeG);
        nodeE.AddLeft(nodeH);
        nodeE.AddRight(nodeI);
        nodeG.AddLeft(nodeJ);
        nodeJ.AddLeft(nodeK);
        nodeJ.AddRight(nodeL);

        // Arrange and draw the tree.
        nodeA.ArrangeAndDrawSubtree(this.MainCanvas, 10, 10);
    }
}