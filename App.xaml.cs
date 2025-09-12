using AlwaysInTarget.DbCRUD.DbRead;

namespace AlwaysInTarget
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
            DeviceDisplay.KeepScreenOn = true;
        }
    }
}
