﻿using Microsoft.Win32;
using ModernWpf.Controls;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Control = System.Windows.Controls.Control;
using Frame = ModernWpf.Controls.Frame;
using Page = System.Windows.Controls.Page;

namespace MosqueMateV2.Helpers
{
    public static class AppHelper
    {
        public static string ChangeAppFont(byte[] fontData)
        {
            if (fontData.Length == 0)
                return null;

            string tempFontPath = Path.Combine(Path.GetTempPath(), "tempFont.ttf");
            if (File.Exists(tempFontPath))
                File.Delete(tempFontPath);
            File.WriteAllBytes(tempFontPath, fontData);
            var fontFamily = new FontFamily(new Uri($"file:///{tempFontPath}"), "./#CustomFont");
            var fontStyle = new Style(typeof(TextElement));
            fontStyle.Setters.Add(new Setter(TextElement.FontFamilyProperty, fontFamily));

            var controlStyle = new Style(typeof(Control));
            controlStyle.Setters.Add(new Setter(Control.FontFamilyProperty, fontFamily));

            var textBlockStyle = new Style(typeof(TextBlock));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, fontFamily));

            System.Windows.Application.Current.Resources.Add(typeof(TextElement), fontStyle);
            System.Windows.Application.Current.Resources.Add(typeof(Control), controlStyle);
            System.Windows.Application.Current.Resources.Add(typeof(TextBlock), textBlockStyle);
            return tempFontPath;
        }
        public static void ChangeAppFontFromResource(Window window, string fontName)
        {
            var font = (FontFamily)System.Windows.Application.Current.Resources[fontName];
            if (font is not null)
                window.FontFamily = font;
        }
        public static void ChangeAppFontFromResource(this Control control, string fontName)
        {
            var font = (FontFamily)System.Windows.Application.Current.Resources[fontName];
            if (font is not null)
                control.FontFamily = font;
        }
        public static FontFamily GetResourceFont(string fontName)
        {
            var font = (FontFamily)System.Windows.Application.Current.Resources[fontName];
            if (font is not null)
                return font;
            return null;
        }
        public static void AddApplicationToStartup()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name ?? SD.Localization.AppAssemblyName;
            string appPath = System.Windows.Application.ResourceAssembly.Location.Replace(".dll", ".exe");
            if (File.Exists(appPath))
            {
                using var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue(appName, appPath);
            }

        }
        public static void RemoveApplicationFromStartup()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name ?? SD.Localization.AppAssemblyName;
            using var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key.GetValue(appName) != null)
            {
                key.DeleteValue(appName);
            }
        }

        public static bool RestartApp()
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                Process.Start(currentProcess.MainModule.FileName);
                currentProcess.Kill();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static void GoHome()
        {
            var window = App.Current.MainWindow;
            if (window is not null)
            {
                var nav = window.FindName("NavView") as NavigationView;
                var homeItem = window.FindName("homeItem") as NavigationViewItem;
                if (nav is not null && homeItem is not null)
                    nav.SelectedItem = homeItem;
            }
        }
        public static void GoBack()
        {
            var window = App.Current.MainWindow;
            if (window is not null)
            {
                var frame = window.FindName("MainFrame") as Frame;
                if (frame is not null)
                {
                    if (frame.CanGoBack)
                    {
                        frame.GoBack();
                    }
                }
            }
        }

        public static bool IsWindowOpen<T>(string windowName, bool IsActivate) where T : Window
        {
            var window = Application.Current.Windows.OfType<T>().FirstOrDefault(w => w.Name.Contains(windowName));
            if (window is not null)
            {
                if (IsActivate)
                {
                    if (window.WindowState == WindowState.Minimized)
                    {
                        window.WindowState = WindowState.Normal;
                        window.Activate();
                    }

                }
                return true;
            }
            return false;
        }

        public static void NavigateToSpecificPage(Page page)
        {
            var window = App.Current.MainWindow;
            if (window is not null)
            {
                var frame = window.FindName("MainFrame") as Frame;
                frame?.Navigate(page);
            }
        }

        /// <summary>
        /// Gets all the windows currently open in the WPF application.
        /// </summary>
        /// <returns>A list of all open windows.</returns>
        public static List<Window> GetAllWindows()
        {
            return Application.Current.Windows.Cast<Window>().ToList();
        }

        public static void ChnageAppTheme(ThemeMode mode)
        {
            var allWindows = GetAllWindows();
            if (allWindows.Count > 0)
            {
                foreach (var window in allWindows)
                {
                    if (window is not null)
                    {
                        window.ThemeMode = mode;
                    }
                }
            }
        }

        public static string PlayListName { get; set; }
        public static string? currentVideoFileName { get; set; }
        public static string? currentVideoFullPath { get; set; }
    }
}
