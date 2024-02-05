using AlwaysInTarget.DbCRUD.DbRead;
using AlwaysInTarget.Storage;

namespace AlwaysInTarget
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            WeatherStorage.WeatherConditions = new RWeatherConditions().Output();

            MainPage = new AppShell();
        }
    }
}
