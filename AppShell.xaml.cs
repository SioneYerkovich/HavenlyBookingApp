using HavenlyBookingApp.Sessions;

namespace HavenlyBookingApp
{
    public partial class AppShell : Shell
    {
        private readonly UserSession _session;
        public AppShell(UserSession session)
        {
            InitializeComponent();
            _session = session;
            Clouds.IsAnimationEnabled = false;

            if (_session.CurrentUser.AccountType != "Admin")
            {
                AdminItem.FlyoutItemIsVisible = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Wait 15 seconds before showing and playing the animation
            await Task.Delay(12000);
            Clouds.IsVisible = true;
            Clouds.IsAnimationEnabled = true;
        }
    }
}
