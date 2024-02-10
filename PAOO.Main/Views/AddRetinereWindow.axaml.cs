using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class AddRetinereWindow : Window
{
    public AddRetinereWindow()
    {
        InitializeComponent();
        DataContext = new AddRetinereWindowViewModel();
    }
}