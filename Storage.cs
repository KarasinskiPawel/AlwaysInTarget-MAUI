﻿using AlwaysInTarget.DbCRUD.DbRead;
using AlwaysInTarget.Models;
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
        public NavigationModel NavigationModel { get; set; }

        private Storage()
        {
            WeatherConditions = new RWeatherConditions().Output();
            NavigationModel = new NavigationModel();
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