using System.Windows;

namespace org_chart1;

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
        // Build the tree.
        var generiGloop = BuildGeneriGloopTree();

        // Draw the tree.
        generiGloop.ArrangeAndDrawSubtree(MainCanvas, 10, 10);
    }

    // Build a test tree.
    private NaryNode<string> BuildGeneriGloopTree()
    {
        // Build the top levels.
        var generiGloop = new NaryNode<string>("GeneriGloop");
        var rAd = new NaryNode<string>("R & D");
        var sales = new NaryNode<string>("Sales");
        var professionalServices = new NaryNode<string>("Professional\nServices");
        var applied = new NaryNode<string>("Applied");
        var basic = new NaryNode<string>("Basic");
        var advanced = new NaryNode<string>("Advanced");
        var sciFi = new NaryNode<string>("Sci Fi");
        var insideSales = new NaryNode<string>("Inside\nSales");
        var outsideSales = new NaryNode<string>("Outside\nSales");
        var b2B = new NaryNode<string>("B2B");
        var consumer = new NaryNode<string>("Consumer");
        var accountManagement = new NaryNode<string>("Account\nManagement");
        var hr = new NaryNode<string>("HR");
        var accounting = new NaryNode<string>("Accounting");
        var legal = new NaryNode<string>("Legal");

        generiGloop.AddChild(rAd);
        generiGloop.AddChild(sales);
        generiGloop.AddChild(professionalServices);

        professionalServices.AddChild(hr);
        professionalServices.AddChild(accounting);
        professionalServices.AddChild(legal);

        // Build the bottom levels.
        // Change to 'if (true)' to build the whole tree.
        if (true)
        {
            var training = new NaryNode<string>("Training");
            var hiring = new NaryNode<string>("Hiring");
            var equity = new NaryNode<string>("Equity");
            var discipline = new NaryNode<string>("Discipline");
            var payroll = new NaryNode<string>("Payroll");
            var billing = new NaryNode<string>("Billing");
            var reporting = new NaryNode<string>("Reporting");
            var opacity = new NaryNode<string>("Opacity");
            var compliance = new NaryNode<string>("Compliance");
            var progress_prevention = new NaryNode<string>("Progress\nPrevention");
            var bail_services = new NaryNode<string>("Bail\nServices");

            rAd.AddChild(applied);
            rAd.AddChild(basic);
            rAd.AddChild(advanced);
            rAd.AddChild(sciFi);

            sales.AddChild(insideSales);
            sales.AddChild(outsideSales);
            sales.AddChild(b2B);
            sales.AddChild(consumer);
            sales.AddChild(accountManagement);

            hr.AddChild(training);
            hr.AddChild(hiring);
            hr.AddChild(equity);
            hr.AddChild(discipline);

            accounting.AddChild(payroll);
            accounting.AddChild(billing);
            accounting.AddChild(reporting);
            accounting.AddChild(opacity);

            legal.AddChild(compliance);
            legal.AddChild(progress_prevention);
            legal.AddChild(bail_services);
        }

        return generiGloop;
    }
}