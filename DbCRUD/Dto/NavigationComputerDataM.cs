using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.DbCRUD.Dto
{
    public class NavigationComputerDataM : INotifyPropertyChanged
    {
        public NavigationComputerDataM()
        {
            IsEnabled = false;
        }
        public int KeyId { get; set; }
        public int MapHeading { get; set; }
        public int MapDistance { get; set; }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value) return;
                _isEnabled = value;
                ButtonColor = value ? Colors.YellowGreen : Colors.Gray;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        private Color _buttonColor = Colors.DarkGray;
        public Color ButtonColor
        {
            get => _buttonColor;
            set
            {
                if (_buttonColor == value) return;
                _buttonColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonColor)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
