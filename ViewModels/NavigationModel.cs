using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class NavigationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _windDirection = 0;
        private decimal _windStrenght = 0.00M;
        private string _windCorrectionAngel = string.Empty;
        private string _heading = string.Empty;
        private string _bombSightDeflection = string.Empty;
        private bool _navigationPointAdded = false;

        private int _tas_KM;

        public string SelectedSystem { get; set; }
        public int Course { get; set; }
        public int IAS { get; set; }
        public int TAS_KM {
            get => _tas_KM;
            set
            {
                if (_tas_KM == value)
                    return;

                _tas_KM = value;
                TAS_MPH = Convert.ToInt32(value / 1.609M);
            }
        }
        public int TAS_MPH { get; set; }
        public  int Altitude { get; set; }    
        public int WindDirection
        {
            get => _windDirection;
            set
            {
                if(_windDirection == value)
                    return;

                _windDirection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindDirection)));
            }
        }
        public decimal WindStrenght
        {
            get => _windStrenght;
            set
            {
                if(value == _windStrenght) return;
                _windStrenght = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindStrenght)));
            }
        }
        public string? WindCorrectionAngel
        {
            get => _windCorrectionAngel;
            set
            {
                if (value == _windCorrectionAngel) return;
                _windCorrectionAngel = value == null ? "" : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindCorrectionAngel)));
            }
        }
        public string? Heading
        {
            get => _heading;
            set
            {
                if (value == _heading) return;
                _heading = value == null ? "" : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Heading)));
            }
        }
        public string? BombSightDeflection
        {
            get => _bombSightDeflection;
            set
            {
                if (value == _bombSightDeflection) return;
                _bombSightDeflection = value == null ? "" : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BombSightDeflection)));
            }
        }

        public bool NavigationPointAdded
        { get => _navigationPointAdded;
            set
            {
                if (value == _navigationPointAdded) return;
                _navigationPointAdded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavigationPointAdded)));
            }
        }

        public string[] MeasureSystems { get; set; } = ["Metric", "Imperial"];

        public NavigationModel()
        {
            SelectedSystem = "Metric";
        }
    }
}
