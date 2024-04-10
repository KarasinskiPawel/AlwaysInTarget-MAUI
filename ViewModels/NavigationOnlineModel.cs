#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class NavigationOnlineModel : INotifyPropertyChanged
    {
        private string _planeType = string.Empty;
        private int _course;
        private int _trueHeading { get; set; }
        private int _altitude { get; set; }
        private int _tas_KM;
        private int _windDirection { get; set; }
        private decimal _windStrenght { get; set; }
        private string _windCorrectionAngel { get; set; }
        private string _bombSightDeflection { get; set; }
        public string PlaneType {
            get => _planeType;
            set {
                _planeType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaneType)));
            }
        }
        public string SelectedSystem { get; set; }
        public string[] MeasureSystems { get; set; } = ["Metric", "Imperial"];
        public int Course {
            get => _course;
            set {
                _course = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Course)));
            }
        }
        public int TrueHeading {
            get => _trueHeading;
            set {
                _trueHeading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrueHeading)));
            }
        }
        public int TrueCourse { get; set; }

        public int IAS { get; set; }
        public int TAS_KM
        {
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
        public int Altitude {
            get => _altitude;
            set {
                _altitude = value;
            }
        }
        public int WindDirection
        {
            get => _windDirection;
            set
            {
                if (_windDirection == value)
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
                if (value == _windStrenght) return;
                _windStrenght = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindStrenght)));
            }
        }
        public string WindCorrectionAngel
        {
            get => _windCorrectionAngel;
            set
            {
                if (value == _windCorrectionAngel) return;
                _windCorrectionAngel = value == null ? "" : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindCorrectionAngel)));
            }
        }
        public string BombSightDeflection
        {
            get => _bombSightDeflection;
            set
            {
                if (value == _bombSightDeflection) return;
                _bombSightDeflection = value == null ? "" : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BombSightDeflection)));
            }
        }
        public NavigationOnlineModel()
        {
            SelectedSystem = "Metric";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
