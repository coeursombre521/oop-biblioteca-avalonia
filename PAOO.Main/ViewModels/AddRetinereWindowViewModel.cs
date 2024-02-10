using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class AddRetinereWindowViewModel : ViewModelBase
{
    private readonly ItemService _itemService = new();
    private readonly MembruService _memberService = new();
    private readonly RetinereService _retinereService = new();

    public static string AddRetinereWindowTitle => "Retine item";
    public static string AddRetinereWindowHeader => "Retineti un item";
    public static string AddRetinereItemsHeader => "Lista elemente";
    public static string AddRetinereMembersHeader => "Lista membri";
    public static string AddRetinereIdColumn => "Id";
    public static string AddRetinereTypeColumn => "Tip";
    public static string AddRetinereTitluColumn => "Titlu";
    public static string AddRetinereNumeColumn => "Nume";
    public static string AddRetinereAdresaColumn => "Adresa";
    public static string AddRetinereTelefonColumn => "Telefon";
    public static string AddRetinereSubmitButton => "Retine";
    
    private readonly SourceList<BorrowableItem> _items = new();
    private readonly ReadOnlyObservableCollection<BorrowableItem> _filteredItems;
    public ReadOnlyObservableCollection<BorrowableItem> FilteredItems => _filteredItems;

    private readonly SourceList<Membru> _members = new();
    private readonly ReadOnlyObservableCollection<Membru> _filteredMembers;
    public ReadOnlyObservableCollection<Membru> FilteredMembers => _filteredMembers;    

    private string _searchTermItem;
    public string SearchTermItem
    {
        get { return _searchTermItem; }
        set { this.RaiseAndSetIfChanged(ref _searchTermItem, value); }
    }
    
    private string _searchTermMember;
    public string SearchTermMember
    {
        get { return _searchTermMember; }
        set { this.RaiseAndSetIfChanged(ref _searchTermMember, value); }
    }

    private BorrowableItem? _selectedItem = null;
    public BorrowableItem? SelectedItem
    {
        get { return _selectedItem; }
        set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
    }
    
    private Membru? _selectedMember = null;
    public Membru? SelectedMember
    {
        get { return _selectedMember; }
        set { this.RaiseAndSetIfChanged(ref _selectedMember, value); }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public AddRetinereWindowViewModel()
    {
        UpdateLists();

        var filterItem = this.WhenAnyValue(x => x.SearchTermItem)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<BorrowableItem, bool>(
                item => string.IsNullOrEmpty(term) || item.Titlu.Contains(term, StringComparison.OrdinalIgnoreCase)
            ));
        
        var filterMembru = this.WhenAnyValue(x => x.SearchTermMember)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<Membru, bool>(
                membru => string.IsNullOrEmpty(term) || (
                    membru.Nume.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    membru.Adresa.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    membru.Telefon.Contains(term, StringComparison.OrdinalIgnoreCase)
                )
            ));

        _items.Connect()
            .Filter(filterItem)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredItems)
            .Subscribe();

        _members.Connect()
            .Filter(filterMembru)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredMembers)
            .Subscribe();

        SubmitCommand = ReactiveCommand.Create(Submit);
    }

    private void Submit()
    {
        try {
            if (SelectedItem != null && SelectedMember != null)
            {
                _retinereService.AddRetinere(SelectedItem.Id, SelectedMember.Id);
                UpdateLists();
            }
        }
        catch (System.Exception) {}
    }

    private void UpdateLists()
    {
        _items.Clear();
        _items.AddRange(_itemService.GetBorrowed());
        _members.Clear();
        _members.AddRange(_memberService.GetAll());
    }
}