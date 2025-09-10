#nullable disable

using AlwaysInTarget.Models;
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
        private string _trueHeading { get; set; }
        private int _altitude { get; set; }
        private int _ias;
        private int _tas;
        private int _tas_KM;
        private int _windDirection { get; set; }
        private decimal _windStrenght { get; set; }
        private string _windCorrectionAngel { get; set; }
        private string _bombSightDeflection { get; set; }
        private int _groundSpeed;
        private decimal _distance;
        private int _groundSpeed_KM;
        private decimal _distance_KM;
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
        public string TrueHeading {
            get => _trueHeading;
            set {
                _trueHeading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrueHeading)));
            }
        }
        public int TrueCourse { get; set; }
        public int IAS { 
            get => _ias;
            set 
            {
                _ias = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IAS)));
            }
        }
        public int TAS {
            get => _tas;
            set
            {
                _tas = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TAS)));
            }
        }
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Altitude)));
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
        public decimal GroundSpeed_KM {
            get => _groundSpeed_KM;
            set { 
                _groundSpeed_KM = Convert.ToInt32(value);

                switch (SelectedSystem)
                {
                    case "Metric":
                        GroundSpeed = _groundSpeed_KM;
                        break;
                    case "Imperial":
                        GroundSpeed = Math.Round(_groundSpeed_KM / 1.609M, 2);
                        break;
                }
            } 
        }
        public decimal Distance_KM {
            get => _distance_KM;
            set {
                _distance_KM = value;

                switch (SelectedSystem)
                {
                    case "Metric":
                        Distance = _distance_KM;
                        break;
                    case "Imperial":
                        Distance = Math.Round(_distance_KM / 1.609M, 2);
                        break;
                }
            }
        }
        public decimal GroundSpeed
        {
            get => _groundSpeed;
            set
            {
                if (value == 0) return;
                _groundSpeed = Convert.ToInt32(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroundSpeed)));
            }
        }
        public decimal Distance
        {
            get => _distance;
            set
            {
                if (value == _distance) return;
                _distance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Distance)));
            }
        }
        public bool IsMapHeadingEnabled { get; set; } = false;

        public NavigationOnlineModel()
        {
            SelectedSystem = "Metric";
        }
        public PlaneDataM planeDataM { get; set; }
        public DateTime measuringTimePoint = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
