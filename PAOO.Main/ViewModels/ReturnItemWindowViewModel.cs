using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using DynamicData;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class ReturnItemWindowViewModel : ViewModelBase
{
    private readonly ItemService _itemService = new();
    private readonly MembruService _memberService = new();

    public static string ReturnItemWindowTitle => "Returneaza item";
    public static string ReturnItemWindowHeader => "Returnati un item";
    public static string ReturnItemItemsHeader => "Lista elemente";
    public static string ReturnItemMembersHeader => "Lista membri";
    public static string ReturnItemIdColumn => "Id";
    public static string ReturnItemTypeColumn => "Tip";
    public static string ReturnItemTitluColumn => "Titlu";
    public static string ReturnItemNumeColumn => "Nume";
    public static string ReturnItemAdresaColumn => "Adresa";
    public static string ReturnItemTelefonColumn => "Telefon";
    public static string ReturnItemSubmitButton => "Returneaza";
    
    private readonly SourceList<BorrowableItem> _items = new();
    private readonly ReadOnlyObservableCollection<BorrowableItem> _filteredItems;
    public ReadOnlyObservableCollection<BorrowableItem> FilteredItems => _filteredItems;

    private readonly SourceList<Membru> _members = new();
    private readonly ReadOnlyObservableCollection<Membru> _filteredMembers;
    public ReadOnlyObservableCollection<Membru> FilteredMembers => _filteredMembers;    

    private string _returnDateNotice;
    public string ReturnDateNotice
    {
        get { return _returnDateNotice; }
        set { this.RaiseAndSetIfChanged(ref _returnDateNotice, value); }
    }

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
        set {
            StringBuilder stringBuilder = new();
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            if (value != null)
            {
                stringBuilder.Append($"Data de returnare: {value.DataLimita.ToString()}. ");
                if (SelectedMember != null) {
                    var daysOverdue = _itemService.DaysItemReturnOverdue(value.Id, SelectedMember.Id);
                    if (daysOverdue > 0)
                    {
                        stringBuilder.Append($"Data limita a fost depasita cu {daysOverdue} zile, se va aplica penalizare!");
                    }
                    else
                    {
                        stringBuilder.Append($"Mai aveti {-daysOverdue} zile pana la data limita.");
                    }
                }
                ReturnDateNotice = stringBuilder.ToString();
            }
            else
            {
                ReturnDateNotice = " ";
            }
        }
    }
    
    private Membru? _selectedMember = null;
    public Membru? SelectedMember
    {
        get { return _selectedMember; }
        set {
            this.RaiseAndSetIfChanged(ref _selectedMember, value);
            UpdateItemsList();
        }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public ReturnItemWindowViewModel()
    {
        UpdateMembersList();
        UpdateItemsList();

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
                _itemService.ReturnItem(SelectedItem.Id, SelectedMember.Id);
                UpdateMembersList();
                UpdateItemsList();
            }
        }
        catch (System.Exception) {}
    }

    private void UpdateMembersList()
    {
        _members.Clear();
        _members.AddRange(_memberService.GetAll());
    }

    private void UpdateItemsList()
    {
        _items.Clear();
        if (SelectedMember != null)
        {
            _items.AddRange(_itemService.GetBorrowedByMember(SelectedMember.Id));
        }
    }
}