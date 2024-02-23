using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class FlightPlanModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<NavigationPointM> _navigationPoints = new List<NavigationPointM>();
        public List<NavigationPointM> NavigationPoints
        { 
            get => _navigationPoints;
            set
            {
                if (value != null) 
                {
                    _navigationPoints = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavigationPoints)));
                }
            }
        }

        public FlightPlanModel()
        {
            NavigationPoints = new List<NavigationPointM>();
        }

        public bool Add(NavigationPointM navigationPoint)
        {
            bool output = true;

            try
            {
                NavigationPoints.Add(navigationPoint);
            }
            catch
            {
                output = false;
            }

            return output;
        }

        public bool Clear()
        {
            bool output = true;

            try
            {
                NavigationPoints = new List<NavigationPointM>();
            }
            catch
            {
                output = false;
            }

            return output;
        }
    }
}
