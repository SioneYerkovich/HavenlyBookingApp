using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using HavenlyBookingApp.Models;
using HavenlyBookingApp.Models.ViewModels;
using Microsoft.Maui.Controls;
using HavenlyBookingApp.Sessions;

namespace HavenlyBookingApp.Views;

public partial class DashboardView : ContentPage
{
	public DashboardView()
	{
		InitializeComponent();
        var database = MauiProgram.Services.GetService<HavenlyDatabase>();
        var session = MauiProgram.Services.GetService<UserSession>();
        BindingContext = new DashboardViewModel(database, session);
        Clouds.IsAnimationEnabled = false;
        Clouds1.IsAnimationEnabled = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Wait 15 seconds before showing and playing the animation
        await Task.Delay(3000);
        Clouds.IsVisible = true;
        Clouds.IsAnimationEnabled = true;
        await Task.Delay(20000);
        Clouds1.IsVisible = true;
        Clouds1.IsAnimationEnabled = true;
    }

    private async void Cancel_Booking(object sender, EventArgs e)
    {
        //This if statement only exists to create variables for reference within the method. 
        //Example: CancelBookingAsync requires a BookingModel argument to execute, this is passed using the declarations.
        if (sender is VisualElement button &&
           button.BindingContext is BookingModel booking && 
           BindingContext is DashboardViewModel vm)
        {
            string confirmPassword = await DisplayPromptAsync("Cancel reservation", "Please enter your password to cancel:", keyboard: Keyboard.Password);
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                await Toast.Make("Please provide a password to cancel a reservation.", ToastDuration.Short).Show();
            }
            else if (confirmPassword != vm.Password)
            {
                await Toast.Make("Your password was incorrect.", ToastDuration.Short).Show();
                return;
            }
            else
            {
                await vm.CancelBookingAsync(booking);
                await Toast.Make("Your reservation has been cancelled.", ToastDuration.Short).Show();
            }
            
        }
    }
}