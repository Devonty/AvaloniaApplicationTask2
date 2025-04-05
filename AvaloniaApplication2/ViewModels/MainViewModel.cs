using Avalonia.Threading;
using AvaloniaApplication2.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication2.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private Aircraft? _selectedAircraft;
        public Aircraft? SelectedAircraft
        {
            get => _selectedAircraft;
            set
            {
                Dispatcher.UIThread.VerifyAccess(); // Проверка потока
                this.RaiseAndSetIfChanged(ref _selectedAircraft, value);
            }
        }

        private string _statusMessage = "Выберите воздушное судно";

        public ObservableCollection<Aircraft> Aircrafts { get; } = new()
        {
            new Airplane("Boeing 747", 1500),
            new Helicopter("Bell 206")
        };

        public string StatusMessage
        {
            get => _statusMessage;
            set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
        }

        public ReactiveCommand<Unit, Unit> TakeOffCommand { get; }
        public ReactiveCommand<Unit, Unit> LandCommand { get; }

        public MainViewModel()
        {
            TakeOffCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    if (SelectedAircraft != null)
                    {
                        await Task.Run(() => SelectedAircraft.TakeOff());
                    }
                    return Unit.Default;
                },
                outputScheduler: RxApp.MainThreadScheduler
            );

            LandCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    if (SelectedAircraft != null)
                    {
                        await Task.Run(() => SelectedAircraft.Land());
                    }
                    return Unit.Default;
                },
                outputScheduler: RxApp.MainThreadScheduler
            );

            foreach (var aircraft in Aircrafts)
            {
                aircraft.StatusChanged += (s, msg) =>
                {
                    Dispatcher.UIThread.Post(() => StatusMessage = msg); // Явное обновление в UI-потоке
                };
            }
        }

        private void OnAircraftStatusChanged(object? sender, string message)
        {
            // Обновление через планировщик ReactiveUI
            RxApp.MainThreadScheduler.Schedule(() => StatusMessage = message);
        }
    }
}