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
                _connected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Connected)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
