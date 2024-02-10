using System.Collections.ObjectModel;
using System.Windows.Input;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class AddMemberWindowViewModel : ViewModelBase
{
    private MembruService _membriService = new();

    public static string AddMemberWindowTitle => "Adaugare membru";
    public static string AddMemberWindowHeader => "Adaugati un membru";
    public static string AddMemberNumeTextBox => "Nume";
    public static string AddMemberAdresaTextBox => "Adresa";
    public static string AddMemberTelefonTextBox => "Telefon";
    public static string AddMemberSubmitButton => "Adaugare";

    private string? _nume;
    public string? Nume
    {
        get { return _nume; }
        set { this.RaiseAndSetIfChanged(ref _nume, value); }
    }

    private string? _adresa;
    public string? Adresa
    {
        get { return _adresa; }
        set { this.RaiseAndSetIfChanged(ref _adresa, value); }
    }

    private string? _telefon;
    public string? Telefon
    {
        get { return _telefon; }
        set { this.RaiseAndSetIfChanged(ref _telefon, value); }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public AddMemberWindowViewModel()
    {
        SubmitCommand = ReactiveCommand.Create(Submit);
    }


    private void Submit()
    {
        try {
            if (Nume == null || Adresa == null || Telefon == null)
            {
                return;
            }

            _membriService.AddMember(Nume, Adresa, Telefon);

            Nume = null;
            Adresa = null;
            Telefon = null;
        }
        catch (System.Exception) {}
    }
}