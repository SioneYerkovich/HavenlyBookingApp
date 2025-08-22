using HavenlyBookingApp.Views;

namespace HavenlyBookingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new PolicyView());
        }
    }
}