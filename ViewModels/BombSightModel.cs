using AlwaysInTarget.Calculate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace AlwaysInTarget.ViewModels
{
    public class BombSightModel : INotifyPropertyChanged
    {
        private int _course;
        private int _windDirection;
        private string _bombSightDeflection;

        BombSightCalculate bombSightCalculate;
        public int Course
        {
            get => _course;
            set
            {
                _course = value;

                //if (_windDirection > 0 && _course > 0)
                    BombSightDeflection = new BombSightCalculate(_course, _windDirection).Output();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Course)));
            }
        }

        public int WindDirection
        {
            get => _windDirection;
            set
            {
                _windDirection = value;

                //if (_windDirection > 0 && _course > 0)
                    BombSightDeflection = new BombSightCalculate(_course, _windDirection).Output();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindDirection)));
            }
        }

        public string BombSightDeflection
        {
            get => _bombSightDeflection;
            set
            {
                _bombSightDeflection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BombSightDeflection)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
