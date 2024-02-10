using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class AddItemWindow : Window
{
    public AddItemWindow()
    {
        InitializeComponent();
        DataContext = new AddItemWindowViewModel();
    }
}