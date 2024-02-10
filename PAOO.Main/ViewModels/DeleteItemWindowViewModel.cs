using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class DeleteItemWindowViewModel : ViewModelBase
{
    private readonly ItemService _itemService = new();

    public static string DeleteItemWindowTitle => "Stergere item";
    public static string DeleteItemWindowHeader => "Stergeti un item";
    public static string DeleteItemIdColumn => "Id";
    public static string DeleteItemTypeColumn => "Tip";
    public static string DeleteItemTitluColumn => "Titlu";
    public static string DeleteItemAutorColumn => "Autor";
    public static string DeleteItemSubmitButton => "Stergere";
    
    private readonly SourceList<BorrowableItem> _items = new();
    private readonly ReadOnlyObservableCollection<BorrowableItem> _filteredItems;
    public ReadOnlyObservableCollection<BorrowableItem> FilteredItems => _filteredItems;

    private string _searchTerm;
    public string SearchTerm
    {
        get { return _searchTerm; }
        set { this.RaiseAndSetIfChanged(ref _searchTerm, value); }
    }

    private BorrowableItem? _selectedItem = null;
    public BorrowableItem? SelectedItem
    {
        get { return _selectedItem; }
        set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public DeleteItemWindowViewModel()
    {
        _items.AddRange(_itemService.GetAll());

        var filter = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<BorrowableItem, bool>(
                item => string.IsNullOrEmpty(term) || item.Titlu.Contains(term, StringComparison.OrdinalIgnoreCase)
            ));

        _items.Connect()
            .Filter(filter)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredItems)
            .Subscribe();

        SubmitCommand = ReactiveCommand.Create(Submit);
    }

    private void Submit()
    {
        try {
            if (SelectedItem != null)
            {
                _itemService.RemoveItem(SelectedItem.Id);
                _items.Remove(SelectedItem);
            }
        }
        catch (System.Exception) {}
    }
}