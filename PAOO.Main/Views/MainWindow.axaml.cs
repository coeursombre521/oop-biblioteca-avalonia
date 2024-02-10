using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PAOO.Main.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnAddMemberClick(object sender, RoutedEventArgs e)
    {
        var window = new AddMemberWindow();
        window.Show();
    }

    private void OnDeleteMemberClick(object sender, RoutedEventArgs e)
    {
        var window = new DeleteMemberWindow();
        window.Show();
    }

    private void OnAddItemClick(object sender, RoutedEventArgs e)
    {
        var window = new AddItemWindow();
        window.Show();
    }

    private void OnDeleteItemClick(object sender, RoutedEventArgs e)
    {
        var window = new DeleteItemWindow();
        window.Show();
    }

    private void OnBorrowItemClick(object sender, RoutedEventArgs e)
    {
        var window = new BorrowItemWindow();
        window.Show();
    }

    private void OnReturnItemClick(object sender, RoutedEventArgs e)
    {
        var window = new ReturnItemWindow();
        window.Show();
    }

    private void OnPayPenaltyClick(object sender, RoutedEventArgs e)
    {
        var window = new PayPenaltyWindow();
        window.Show();
    }

    private void OnAddRetinereClick(object sender, RoutedEventArgs e)
    {
        var window = new AddRetinereWindow();
        window.Show();
    }

    private void OnDeleteRetinereClick(object sender, RoutedEventArgs e)
    {
        var window = new DeleteRetinereWindow();
        window.Show();
    }
    
    private void OnViewLogsClick(object sender, RoutedEventArgs e)
    {
        var window = new LogWindow();
        window.Show(); 
    }
}