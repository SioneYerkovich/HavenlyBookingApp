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
    }

    private void OpenFlyoutButton_Clicked(object sender, EventArgs e)
    {
        //toggle the flyout menu
        Shell.Current.FlyoutIsPresented = true;
    }
}