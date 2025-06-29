using System;
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

public partial class SettingsFile
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

    [Notify]
    public LanguageValue _Language;

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
        CultureInfo.CurrentUICulture = Language switch
        {
            LanguageValue.English => new CultureInfo("en"),
            LanguageValue.ChineseSimplified => new CultureInfo("zh-Hans"),
            _ => CultureInfo.CurrentUICulture
        };
        
        
        MessageWindow window = new MessageWindow(titleText: Resources.LanguageChangedTitle,
            message: Resources.RestartUndertaleModToolToApplyNewLanguage, hasNoButton: true, hasYesButton: true);
  
        
        window.Initialize();
        window.Show();
        
    }
    
}
