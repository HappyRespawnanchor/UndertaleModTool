using System;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using PropertyChanged.SourceGenerator;
using UndertaleModToolAvalonia.Assets;
using UndertaleModToolAvalonia.Controls;
using UndertaleModToolAvalonia.Views;

namespace UndertaleModToolAvalonia.Core;

public partial class SettingsFile : INotifyPropertyChanged
{
    public enum ThemeValue
    {
        SystemDefault = 0,
        Light = 1,
        Dark = 2,
    }
    public enum LanguageValue
    {
        English = 0,
        ChineseSimplified = 1
    }

    [Notify]
    private ThemeValue _Theme;

    private LanguageValue _Language = GetDefaultLanguage();
    public LanguageValue Language
    {
        get => _Language;
        set
        {
            if (_Language != value)
            {
                _Language = value;
                OnLanguageChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Language)));
            }
        }
    }


    public LanguageValue CurrentLanguage
    {
        get
        {
            var name = CultureInfo.CurrentUICulture.Name;
            if (name.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
                return LanguageValue.ChineseSimplified;
            return LanguageValue.English;
        }
    }

    private static LanguageValue GetDefaultLanguage()
    {
        var name = CultureInfo.CurrentUICulture.Name;
        if (name.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
            return LanguageValue.ChineseSimplified;
        return LanguageValue.English;
    }

    void OnThemeChanged()
    {
        if (App.Current is not null)
        {
            App.Current.RequestedThemeVariant = Theme switch
            {
                ThemeValue.SystemDefault => ThemeVariant.Default,
                ThemeValue.Light => ThemeVariant.Light,
                ThemeValue.Dark => ThemeVariant.Dark,
                _ => throw new NotImplementedException(),
            };
        }
    }
    
    void OnLanguageChanged()
    {
        MessageWindow window = new MessageWindow(titleText: Resources.LanguageChangedTitle,
            message: Resources.RestartToApplyNewLanguageText, hasNoButton: true, hasYesButton: true);
        window.Initialize();
        window.Show();
        switch (_Language)
        {
            case LanguageValue.English:
                CultureInfo.CurrentUICulture = new CultureInfo("en");
                break;
            case LanguageValue.ChineseSimplified:
                CultureInfo.CurrentUICulture = new CultureInfo("zh-Hans");
                break;
        }

    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
