using AvaloniaApplication2.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace AvaloniaApplication2.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private Aircraft? selectedAircraft;
        private string statusMessage = "Выберите воздушное судно";

        public ObservableCollection<Aircraft> Aircrafts { get; } = new()
        {
            new Airplane("Boeing 747", 1500),
            new Helicopter("Bell 206")
        };

        public Aircraft? SelectedAircraft
        {
            get => selectedAircraft;
            set => this.RaiseAndSetIfChanged(ref selectedAircraft, value);
        }

        public string StatusMessage
        {
            get => statusMessage;
            set => this.RaiseAndSetIfChanged(ref statusMessage, value);
        }

        public ReactiveCommand<Unit, Unit> TakeOffCommand { get; }
        public ReactiveCommand<Unit, Unit> LandCommand { get; }

        public MainViewModel()
        {
            TakeOffCommand = ReactiveCommand.Create(TakeOff);
            LandCommand = ReactiveCommand.Create(Land);

            foreach (var aircraft in Aircrafts)
            {
                aircraft.StatusChanged += (s, msg) => StatusMessage = msg;
            }
        }

        private void TakeOff() => SelectedAircraft?.TakeOff();
        private void Land() => SelectedAircraft?.Land();
    }
}