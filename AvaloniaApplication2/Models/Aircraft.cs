using Avalonia.Threading;
using System;

namespace AvaloniaApplication2.Models
{
    public abstract class Aircraft
    {
        public string Name { get; set; }
        public double Altitude { get; protected set; }
        public event EventHandler<string> StatusChanged = (_, _) => { };

        protected Aircraft(string name)
        {
            Name = name;
        }

        public abstract bool TakeOff();
        public abstract void Land();

        protected virtual void OnStatusChanged(string message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                Dispatcher.UIThread.VerifyAccess();
                StatusChanged?.Invoke(this, message);
            });
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Airplane : Aircraft
    {
        public int RunwayLength { get; set; }

        public Airplane(string name, int initialRunwayLength)
            : base(name)
        {
            RunwayLength = initialRunwayLength;
        }

        public override bool TakeOff()
        {
            if (RunwayLength >= 1000)
            {
                Altitude = 10000;
                OnStatusChanged($"{Name} успешно взлетел!");
                return true;
            }
            OnStatusChanged($"{Name} не смог взлететь: недостаточная длина ВПП!");
            return false;
        }

        public override void Land()
        {
            if (RunwayLength < 1000)
            {
                OnStatusChanged($"{Name} не может приземлиться т.к. слишком короткая ВВП!");
                return;
            }
            Altitude = 0;
            OnStatusChanged($"{Name} успешно приземлился!");
        }
    }

    public class Helicopter : Aircraft
    {
        public Helicopter(string name)
            : base(name) { }

        public override bool TakeOff()
        {
            Altitude = 500;
            OnStatusChanged($"{Name} успешно взлетел!");
            return true;
        }

        public override void Land()
        {
            Altitude = 0;
            OnStatusChanged($"{Name} успешно приземлился!");
        }
    }
}