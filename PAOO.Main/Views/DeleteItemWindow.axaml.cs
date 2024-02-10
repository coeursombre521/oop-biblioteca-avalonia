using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class DeleteItemWindow : Window
{
    public DeleteItemWindow()
    {
        InitializeComponent();
        DataContext = new DeleteItemWindowViewModel();
    }
}