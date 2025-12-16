using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeteoLab_FelixDanakasPruneau.Models
{
    public class DonneeMeteo : INotifyPropertyChanged
    {
        private DateTimeOffset _date = DateTimeOffset.Now;
        private string _temperature;
        private double _humidite;
        private double _precipitations;

        public DateTimeOffset Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        // Température en degrés Celsius
        public string Temperature
        {
            get => _temperature;
            set { _temperature = value; OnPropertyChanged(); }
        }

        // Humidité en pourcentage (0-100)
        public double Humidite
        {
            get => _humidite;
            set { _humidite = value; OnPropertyChanged(); }
        }

        // Précipitations en mm
        public double Precipitations
        {
            get => _precipitations;
            set { _precipitations = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
