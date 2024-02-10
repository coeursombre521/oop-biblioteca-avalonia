using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class DeleteMemberWindowViewModel : ViewModelBase
{
    private readonly MembruService _membriService = new();

    public static string DeleteMemberWindowTitle => "Stergere membru";
    public static string DeleteMemberWindowHeader => "Stergeti un membru";
    public static string DeleteMemberIdColumn => "Id";
    public static string DeleteMemberNumeColumn => "Nume";
    public static string DeleteMemberAdresaColumn => "Adresa";
    public static string DeleteMemberTelefonColumn => "Telefon";
    public static string DeleteMemberSubmitButton => "Stergere";
    
    private readonly SourceList<Membru> _membri = new();
    private readonly ReadOnlyObservableCollection<Membru> _filteredMembri;
    public ReadOnlyObservableCollection<Membru> FilteredMembri => _filteredMembri;

    private string _searchTerm;
    public string SearchTerm
    {
        get { return _searchTerm; }
        set { this.RaiseAndSetIfChanged(ref _searchTerm, value); }
    }

    private Membru? _selectedItem = null;
    public Membru? SelectedItem
    {
        get { return _selectedItem; }
        set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public DeleteMemberWindowViewModel()
    {
        UpdateLists();

        var filter = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<Membru, bool>(
                membru => string.IsNullOrEmpty(term) || (
                    membru.Nume.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    membru.Adresa.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    membru.Telefon.Contains(term, StringComparison.OrdinalIgnoreCase)
                )
            ));

        _membri.Connect()
            .Filter(filter)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredMembri)
            .Subscribe();

        SubmitCommand = ReactiveCommand.Create(Submit);
    }

    private void Submit()
    {
        try {
            if (SelectedItem != null)
            {
                _membriService.RemoveMember(SelectedItem.Id);
                UpdateLists();
            }
        }
        catch (System.Exception) {}
    }

    private void UpdateLists()
    {
        _membri.Clear();
        _membri.AddRange(_membriService.GetAll());
    }
}