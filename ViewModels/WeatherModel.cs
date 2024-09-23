using AlwaysInTarget.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    internal class WeatherModel : INotifyPropertyChanged
    {
        public ObservableCollection<WeatherConditionM> WeatherConditions { get; set; }

        public WeatherModel(List<WeatherConditionM> WeatherConditions)
        {
            this.WeatherConditions = new ObservableCollection<WeatherConditionM>(WeatherConditions);
        }

        public bool Add()
        {
            bool result = true;

            try
            {
                if (WeatherConditions.Count < 10)
                {
                    WeatherConditions.Add(new WeatherConditionM() { Alt = 0, WindFrom = 0, WindStregngth = 0.0M });
                    Storage.GetStorage().WeatherConditions = WeatherConditions.ToList();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeatherConditions)));
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool Remove()
        {
            bool result = true;

            try
            {
                if (WeatherConditions.Count > 1)
                {
                    WeatherConditions.RemoveAt(WeatherConditions.Count - 1);
                    Storage.GetStorage().WeatherConditions = WeatherConditions.ToList();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeatherConditions)));
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
