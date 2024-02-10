using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class DeleteRetinereWindow : Window
{
    public DeleteRetinereWindow()
    {
        InitializeComponent();
        DataContext = new DeleteRetinereWindowViewModel();
    }
}