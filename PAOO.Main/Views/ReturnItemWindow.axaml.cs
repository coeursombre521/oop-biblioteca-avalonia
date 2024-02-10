using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class ReturnItemWindow : Window
{
    public ReturnItemWindow()
    {
        InitializeComponent();
        DataContext = new ReturnItemWindowViewModel();
    }
}