namespace HavenlyBookingApp.Views;
using CommunityToolkit.Maui;

using CommunityToolkit.Maui.Extensions;
using HavenlyBookingApp.Models.ViewModels;

public partial class SignupView : ContentPage
{
	public SignupView()
	{
		InitializeComponent();
        var database = MauiProgram.Services.GetService<HavenlyDatabase>();
        BindingContext = new SignupViewModel(database);
        Clouds.IsAnimationEnabled = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Wait 15 seconds before showing and playing the animation
        await Task.Delay(15000);
        Clouds.IsVisible = true;
        Clouds.IsAnimationEnabled = true;
    }

    private async void PrivacyPolicyTapped(object? sender, TappedEventArgs e)
    {
        var popupContent = new ScrollView
        {
            Content = new Label
            {
                Text = @"Privacy Policy
Last updated: 21/08/25

Thank you for using Havenly Booking App. We are committed to protecting your privacy and ensuring transparency in how your data is collected, stored, and used.

1. Introduction
This Privacy Policy explains how we handle your information when you use the Havenly Booking App. By using the app, you agree to the collection and use of information in accordance with this policy.

2. Information We Collect
We collect only the information necessary to provide and improve our services. This includes:
- Personal Data: Information you provide during signup and profile creation, such as your name, email, and account credentials.
- Booking Data: Details about your bookings, including selected time slots, service type, and any user notes.
- Usage Data: Information on how you interact with the app to improve functionality and user experience.

3. Personal Data
To use the app, you must create an account. We collect personal details required for account management and service booking. If you contact support, we may request your email or contact information for correspondence purposes only.

4. Booking Data
All booking information, including dates, times, and service details, is stored securely in the app’s local database or on our servers, depending on your account settings. This data is used solely to manage your bookings and provide the services offered through the app.

5. How We Use Information
We use collected data to:
- Provide core app functionality, including signup, login, dashboards, and booking management.
- Ensure role-based access control for clients and admins.
- Display available time slots and confirm bookings.
- Improve app performance, usability, and security.
- Respond to support inquiries.

We do not sell, rent, or share your data with third parties for marketing purposes.

6. Disclosure of Information
We will never disclose your information unless required by law or legal obligation (e.g., court order) or as necessary to maintain the integrity of the app.

7. Security of Information
We implement reasonable technical and administrative safeguards to protect your data. This includes encryption, role-based access controls, and secure storage practices. However, no electronic storage system can guarantee 100% security.

8. Protection of Your Data Rights
You have the right to:
- Know what personal and booking data we hold.
- Request correction or deletion of your data.
- Withdraw consent to the use of your data.

Please contact us at havenlygroup@gmail.com to exercise any of these rights.

9. User Consent
All users must agree to this Privacy Policy before creating an account or using the app. Agreement is required to access app features such as booking, dashboards, and profile management.

10. Changes to This Privacy Policy
We may update this Privacy Policy from time to time. If changes are made, the revised policy will be posted within the app, and the 'Last updated' date above will be updated.

11. Contact
If you have any questions about this Privacy Policy or your data rights, please contact:

havenlygroup@gmail.com",
                TextColor = Colors.Black,
                BackgroundColor = Colors.White,
                Padding = 0
            },
        };

        await this.ShowPopupAsync(popupContent, new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = true
        });
    }
}