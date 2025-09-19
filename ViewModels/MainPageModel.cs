using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private bool _connected;
        public bool Connected
        {
            get => _connected;
            set
            {
                if (_connected == value) return;
                _connected = value;
                MainButtonColor = value ? Colors.Ivory : Colors.LightGray;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Connected)));
            }
        }
        private Color _mainButtonColor = Colors.DarkGrey;
        public Color MainButtonColor
        {
            get => _mainButtonColor;
            set
            {
                if (_mainButtonColor == value) return;
                _mainButtonColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainButtonColor)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
