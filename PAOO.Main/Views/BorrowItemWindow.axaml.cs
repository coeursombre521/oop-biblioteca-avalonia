using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class BorrowItemWindow : Window
{
    public BorrowItemWindow()
    {
        InitializeComponent();
        DataContext = new BorrowItemWindowViewModel();
    }
}