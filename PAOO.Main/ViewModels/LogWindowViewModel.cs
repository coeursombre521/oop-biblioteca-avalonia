using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using PAOO.Main.Models;
using PAOO.Main.Services;
using ReactiveUI;

namespace PAOO.Main.ViewModels;

public class LogWindowViewModel : ViewModelBase
{
    private readonly LogService _logService = new();

    public static string LogWindowTitle => "Vizualizare tranzactii";
    public static string LogWindowHeader => "Vizualizare tranzactii";
    public static string LogIdColumn => "Id";
    public static string LogTypeColumn => "Tip";
    public static string LogMessageColumn => "Mesaj";
    public static string LogTimestampColumn => "Inregistrat la";
    public static string LogClearLogButton => "Golire lista tranzactii";
    
    private readonly SourceList<Log> _log = new();
    private readonly ReadOnlyObservableCollection<Log> _filteredLog;
    public ReadOnlyObservableCollection<Log> FilteredLog => _filteredLog;

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
    
    public ICommand ClearLogCommand { get; private set; }

    public LogWindowViewModel()
    {
        UpdateList();

        var filter = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<Log, bool>(
                logItem => string.IsNullOrEmpty(term) || (
                    logItem.Message.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    logItem.Type.ToString().Contains(term, StringComparison.OrdinalIgnoreCase)
                )   
            ));

        _log.Connect()
            .Filter(filter)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredLog)
            .Subscribe();

        ClearLogCommand = ReactiveCommand.Create(ClearLog);
    }

    private void ClearLog()
    {
        try {
            _logService.ClearLog();
            UpdateList();
        }
        catch (System.Exception) {}
    }

    private void UpdateList()
    {
        _log.Clear();
        _log.AddRange(_logService.GetLog());
    }
}