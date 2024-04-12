using AlwaysInTarget.DbCRUD.DbRead;
using AlwaysInTarget.Models;
using AlwaysInTarget.Network;
using AlwaysInTarget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget
{
    public class Storage
    {
        private static Storage? _instance;
        private static object obj = new();

        public List<WeatherConditionM> WeatherConditions { get; set; }
        public FlightPlanModel FlightPlanModel { get; set; }
        public NavigationModel NavigationModel { get; set; }
        public NavigationOnlineModel NavigationOnlineModel { get; set; }

        public BombSightModel BombSightModel { get; set; }
        public Il2DialServerModel Il2DialServerModel { get; set; }

        public UDPClient UdpClient { get; set; }
        public MainPageModel MainPageModel { get; set; }

        private Storage()
        {
            WeatherConditions = new RWeatherConditions().Output();
            NavigationModel = new NavigationModel();
            NavigationOnlineModel = new NavigationOnlineModel();
            BombSightModel = new BombSightModel();
            FlightPlanModel = new FlightPlanModel();
            Il2DialServerModel = new Il2DialServerModel();
            UdpClient = new UDPClient();
            MainPageModel = new MainPageModel();
        }

        public static Storage GetStorage()
        {
            lock(obj)
            {
                if (_instance == null)
                {
                    _instance = new Storage();                
                }
            }

            return _instance;
        }
    }
}
