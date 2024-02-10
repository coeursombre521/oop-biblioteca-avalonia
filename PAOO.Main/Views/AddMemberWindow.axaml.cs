using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class AddMemberWindow : Window
{
    public AddMemberWindow()
    {
        InitializeComponent();
        DataContext = new AddMemberWindowViewModel();
    }
}