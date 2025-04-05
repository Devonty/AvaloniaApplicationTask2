// Models/Aircraft.cs
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
            StatusChanged.Invoke(this, message);
        }

        // Переопределение ToString()
        public override string ToString()
        {
            return Name; // Отображать название воздушного судна
        }
    }

    public class Airplane : Aircraft
    {
        public double RunwayLength { get; }

        public Airplane(string name, double runwayLength)
            : base(name)
        {
            RunwayLength = runwayLength;
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