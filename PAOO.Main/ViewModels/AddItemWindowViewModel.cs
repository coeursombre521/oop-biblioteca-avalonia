using System.Collections.ObjectModel;
using System.Windows.Input;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class AddItemWindowViewModel : ViewModelBase
{
    private ItemService _itemService = new();

    public static string AddItemWindowTitle => "Adaugare item";
    public static string AddCarteWindowHeader => "Adaugati o carte";
    public static string AddRevistaWindowHeader => "Adaugati o revista";
    public static string AddItemTitluTextBox => "Titlu";
    public static string AddItemAutorTextBox => "Autor";
    public static string AddCarteSubmitButton => "Adaugare carte";
    public static string AddRevistaSubmitButton => "Adaugare revista";
    public static string AddItemCarteTab => "Carte";
    public static string AddItemRevistaTab => "Revista";

    private string? _titluCarte;
    public string? TitluCarte
    {
        get { return _titluCarte; }
        set { this.RaiseAndSetIfChanged(ref _titluCarte, value); }
    }
    
    private string? _titluRevista;
    public string? TitluRevista
    {
        get { return _titluRevista; }
        set { this.RaiseAndSetIfChanged(ref _titluRevista, value); }
    }

    private string? _autor;
    public string? Autor
    {
        get { return _autor; }
        set { this.RaiseAndSetIfChanged(ref _autor, value); }
    }
    
    public ICommand SubmitCommandCarte { get; private set; }
    public ICommand SubmitCommandRevista { get; private set; }

    public AddItemWindowViewModel()
    {
        SubmitCommandCarte = ReactiveCommand.Create(SubmitCarte);
        SubmitCommandRevista = ReactiveCommand.Create(SubmitRevista);
    }


    private void SubmitCarte()
    {
        try {
            if (TitluCarte == null || Autor == null)
            {
                return;
            }

            _itemService.AddCarte(TitluCarte, Autor);

            TitluCarte = null;
            Autor = null;
        }
        catch (System.Exception) {}
    }

    private void SubmitRevista()
    {
        try {
            if (TitluRevista == null)
            {
                return;
            }

            _itemService.AddRevista(TitluRevista);

            TitluRevista = null;
        }
        catch (System.Exception) {}
    }
}