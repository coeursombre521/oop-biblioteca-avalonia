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

public class DeleteRetinereWindowViewModel : ViewModelBase
{
    private readonly RetinereService _retinereService = new();
    private readonly MembruService _memberService = new();

    public static string DeleteRetinereWindowTitle => "Returneaza item";
    public static string DeleteRetinereWindowHeader => "Returnati un item";
    public static string DeleteRetinereRetineriHeader => "Lista retineri";
    public static string DeleteRetinereMembersHeader => "Lista membri";
    public static string DeleteRetinereIdColumn => "Id";
    public static string DeleteRetinereTypeColumn => "Tip";
    public static string DeleteRetinereTitluColumn => "Titlu";
    public static string DeleteRetinereNumeColumn => "Nume";
    public static string DeleteRetinereAdresaColumn => "Adresa";
    public static string DeleteRetinereTelefonColumn => "Telefon";
    public static string DeleteRetinereSubmitButton => "Returneaza";
    
    private readonly SourceList<Retinere> _retineri = new();
    private readonly ReadOnlyObservableCollection<Retinere> _filteredRetineri;
    public ReadOnlyObservableCollection<Retinere> FilteredRetineri => _filteredRetineri;

    private readonly SourceList<Membru> _members = new();
    private readonly ReadOnlyObservableCollection<Membru> _filteredMembers;
    public ReadOnlyObservableCollection<Membru> FilteredMembers => _filteredMembers;    

    private string _retinereNotice;
    public string RetinereNotice
    {
        get { return _retinereNotice; }
        set { this.RaiseAndSetIfChanged(ref _retinereNotice, value); }
    }

    private string _searchTermRetinere;
    public string SearchTermRetinere
    {
        get { return _searchTermRetinere; }
        set { this.RaiseAndSetIfChanged(ref _searchTermRetinere, value); }
    }
    
    private string _searchTermMember;
    public string SearchTermMember
    {
        get { return _searchTermMember; }
        set { this.RaiseAndSetIfChanged(ref _searchTermMember, value); }
    }

    private Retinere? _selectedRetinere = null;
    public Retinere? SelectedRetinere
    {
        get { return _selectedRetinere; }
        set {
            StringBuilder stringBuilder = new();
            this.RaiseAndSetIfChanged(ref _selectedRetinere, value);
            if (value != null)
            {
                stringBuilder.Append($"Data limita a retinerii: {value.DataLimita.ToString()}. ");
                var daysOverdue = _retinereService.DaysRetinereOverdue(value.Id);
                if (daysOverdue > 0)
                {
                    stringBuilder.Append($"Data limita a fost depasita cu {daysOverdue} zile.");
                }
                else
                {
                    stringBuilder.Append($"Mai sunt {-daysOverdue} zile pana la data limita.");
                }
                RetinereNotice = stringBuilder.ToString();
            }
            else
            {
                RetinereNotice = " ";
            }
        }
    }
    
    private Membru? _selectedMember = null;
    public Membru? SelectedMember
    {
        get { return _selectedMember; }
        set {
            this.RaiseAndSetIfChanged(ref _selectedMember, value);
            UpdateRetineriList();
        }
    }
    
    public ICommand SubmitCommand { get; private set; }

    public DeleteRetinereWindowViewModel()
    {
        UpdateMembersList();
        UpdateRetineriList();

        var filterRetinere = this.WhenAnyValue(x => x.SearchTermRetinere)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Select(term => new Func<Retinere, bool>(
                retinere => string.IsNullOrEmpty(term) || (
                    retinere.Membru.Nume.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    retinere.Membru.Adresa.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    retinere.Membru.Telefon.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    retinere.BorrowableItem.Titlu.Contains(term, StringComparison.OrdinalIgnoreCase)
                )
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

        _retineri.Connect()
            .Filter(filterRetinere)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _filteredRetineri)
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
            if (SelectedRetinere != null && SelectedMember != null)
            {
                _retinereService.RemoveRetinere(SelectedRetinere.Id);
                UpdateMembersList();
                UpdateRetineriList();
            }
        }
        catch (System.Exception) {}
    }

    private void UpdateMembersList()
    {
        _members.Clear();
        _members.AddRange(_memberService.GetAll());
    }

    private void UpdateRetineriList()
    {
        _retineri.Clear();
        if (SelectedMember != null)
        {
            _retineri.AddRange(_retinereService.GetRetineriByMember(SelectedMember.Id));
        }
    }
}