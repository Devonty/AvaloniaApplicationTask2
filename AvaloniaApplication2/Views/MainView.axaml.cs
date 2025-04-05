using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            // InitializeComponent();
            AvaloniaXamlLoader.Load(this);
        }
    }
}