using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Views;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApplication2
{
    public partial class App : Avalonia.Application // Добавлено наследование
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainView(); // Используйте MainView вместо MainWindow
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}