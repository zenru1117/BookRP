using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BookRP.Models;
using ReactiveUI;

namespace BookRP.ViewModels;

public class MainViewModel : ReactiveObject
{
    public List<ClassificationItem> Classifications => Classification.List;
    private ClassificationItem? _selectedClassification;
    
    public string AppVersion { get; }

    public ClassificationItem? SelectedClassification
    {
        get => _selectedClassification;
        set => this.RaiseAndSetIfChanged(ref _selectedClassification, value);
    }

    public MainViewModel()
    {
        AppVersion = GetAppVersion();
        SelectedClassification = Classification.List.FirstOrDefault();
    }

    private string GetAppVersion()
    {
        var entryAsm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var infoAttr = entryAsm.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return !string.IsNullOrWhiteSpace(infoAttr?.InformationalVersion) ? infoAttr.InformationalVersion : "err";
    }
}