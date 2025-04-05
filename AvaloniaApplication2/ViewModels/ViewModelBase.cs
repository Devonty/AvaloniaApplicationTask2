using ReactiveUI;
using System.ComponentModel;

namespace AvaloniaApplication2.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged
        {
            add => base.PropertyChanged += value;
            remove => base.PropertyChanged -= value;
        }
    }
}