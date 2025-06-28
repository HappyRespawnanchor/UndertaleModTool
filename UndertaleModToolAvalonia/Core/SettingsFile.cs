using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using PropertyChanged.SourceGenerator;
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
        switch (Language)
        {
            case LanguageValue.English:
                CultureInfo.CurrentUICulture = new CultureInfo("en");
                break;
            case LanguageValue.ChineseSimplified:
                CultureInfo.CurrentUICulture = new CultureInfo("zh-Hans");
                break;
        }
        // 弹出提示，询问是否重启
        // 弹出你的 MessageWindow
        var msgWindow = new MessageWindow(
            "Language has been changed.\nRestart UndertaleModTool now?", 
            "Language Changed", 
            yes: true, 
            no: true
        );

        // 显示为对话框，等待结果
        var result = msgWindow.ShowDialog<MessageWindow.Result>(new Window()).Result;


    }
    
}
