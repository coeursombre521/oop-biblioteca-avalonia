using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class DeleteMemberWindow : Window
{
    public DeleteMemberWindow()
    {
        InitializeComponent();
        DataContext = new DeleteMemberWindowViewModel();
    }
}