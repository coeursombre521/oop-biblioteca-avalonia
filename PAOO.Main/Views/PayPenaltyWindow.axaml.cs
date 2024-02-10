using Avalonia.Controls;
using Avalonia.Interactivity;
using PAOO.Main.ViewModels;

namespace PAOO.Main.Views;

public partial class PayPenaltyWindow : Window
{
    public PayPenaltyWindow()
    {
        InitializeComponent();
        DataContext = new PayPenaltyWindowViewModel();
    }
}