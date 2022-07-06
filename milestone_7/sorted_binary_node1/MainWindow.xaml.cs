using System;
using System.Windows;

namespace sorted_binary_node1;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private SortedBinaryNode<int> _rootNode;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        ValueTextBox.Focus();

        _rootNode = new SortedBinaryNode<int>(-1);

        RunTests();

        DrawTree();
    }

    private void RunTests()
    {
        _rootNode.AddNode(new SortedBinaryNode<int>(60));
        _rootNode.AddNode(new SortedBinaryNode<int>(35));
        _rootNode.AddNode(new SortedBinaryNode<int>(76));
        _rootNode.AddNode(new SortedBinaryNode<int>(21));
        _rootNode.AddNode(new SortedBinaryNode<int>(42));
        _rootNode.AddNode(new SortedBinaryNode<int>(71));
        _rootNode.AddNode(new SortedBinaryNode<int>(89));
        _rootNode.AddNode(new SortedBinaryNode<int>(17));
        _rootNode.AddNode(new SortedBinaryNode<int>(24));
        _rootNode.AddNode(new SortedBinaryNode<int>(74));
        _rootNode.AddNode(new SortedBinaryNode<int>(11));
        _rootNode.AddNode(new SortedBinaryNode<int>(23));
        _rootNode.AddNode(new SortedBinaryNode<int>(72));
        _rootNode.AddNode(new SortedBinaryNode<int>(75));
    }

    private void DrawTree()
    {
        MainCanvas.Children.Clear();

        _rootNode.ArrangeAndDrawSubtree(MainCanvas, 5, 5);
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ValueTextBox.Text, out var value) || value < 0)
            MessageBox.Show("The value must be a non-negative integer.");
        else
            try
            {
                _rootNode.AddNode(new SortedBinaryNode<int>(value));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        DrawTree();

        ValueTextBox.Focus();
        ValueTextBox.Clear();
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void FindButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ValueTextBox.Text, out var value) || value < 0)
            MessageBox.Show("The value must be an integer.");
        else
            try
            {
                var node = _rootNode.FindNode(value);
                MessageBox.Show(node == null ? $"The value {value} is not in the tree." : $"Found value {node.Value}.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        //DrawTree();

        ValueTextBox.Focus();
        ValueTextBox.Clear();
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        _rootNode = new SortedBinaryNode<int>(-1);

        DrawTree();

        ValueTextBox.Focus();
        ValueTextBox.Clear();
    }
}