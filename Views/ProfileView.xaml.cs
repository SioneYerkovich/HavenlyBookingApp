using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using HavenlyBookingApp.Models;
using HavenlyBookingApp.Models.ViewModels;
using Microsoft.Maui.Controls;
namespace HavenlyBookingApp.Views;

public partial class ProfileView : ContentPage
{
    // Contructor, gets called by maui when profile view is accessed by the user
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    //This method is called when the user clicks Update account details button
	public async void UpdateDetails(object sender, EventArgs e)
	{
        //Creating a variable to use for logic in this if statement
        if (BindingContext is ProfileViewModel getVM) 
        {
            //store the user input as newFirstName
            string newFirstName = await DisplayPromptAsync("Update details", "enter your new first name:", initialValue: getVM.FirstName);
            if (!string.IsNullOrWhiteSpace(newFirstName)) //if its not blank
            {
                //run the input validation and pass it in the methods fName parameter
                bool validate = await getVM.InputValidation(fName: newFirstName);
                if (!validate)
                {
                    await Toast.Make("First name must contain letters only").Show();
                    return;
                }
                //Call the viewmodel method for updating the first name in HavenlyDatabase
                await getVM.UpdateFirstNameAsync(newFirstName);
                await Toast.Make("First name updated successfully").Show();
            }
            else if (string.IsNullOrWhiteSpace(newFirstName)) //Quick empty validation check
            {
                await Toast.Make("Your first name cannot be empty.", ToastDuration.Short).Show();
                return;
            }

            string newLastName = await DisplayPromptAsync("Update details", "Enter your new last name:", initialValue: getVM.LastName);
            if (!string.IsNullOrWhiteSpace(newLastName))
            {
                bool validate = await getVM.InputValidation(lName: newLastName);
                if (!validate)
                {
                    await Toast.Make("Last name must contain letters only").Show();
                    return;
                }
                await getVM.UpdateLastNameAsync(newLastName);
                await Toast.Make("Last Name updated successfully").Show();
            }
            else if (string.IsNullOrWhiteSpace(newLastName))
            {
                await Toast.Make("Your last name cannot be empty.", ToastDuration.Short).Show();
                return;
            }
        }
    }

    //This method is called when the user clicks Update account details button
    public async void UpdateEmail(object sender, EventArgs e)
    {
        //Creating a variable to use for logic in this if statement
        if (BindingContext is ProfileViewModel getVM)
        {
            //store the user input as newEmail
            string newEmail = await DisplayPromptAsync("Update Email", "Enter your new email:", initialValue: getVM.Email);
            if (!string.IsNullOrWhiteSpace(newEmail)) //if its not blank
            {
                //run the input validation and pass it in the methods fName parameter
                bool validate = await getVM.InputValidation(email: newEmail);
                if (!validate)
                {
                    await Toast.Make("Please enter a valid email format").Show();
                    return;
                }
                //Call the viewmodel method for updating the first name in HavenlyDatabase
                await getVM.UpdateEmailAsync(newEmail);
                await Toast.Make("Email updated successfully").Show();
            }
            else if (string.IsNullOrEmpty(newEmail)) //Quick empty validation check
            {
                await Toast.Make("You must provide an email address", ToastDuration.Short).Show();
                return;
            }
        }
    }

    //This method is called when the user clicks change password button
    public async void UpdatePassword(object sender, EventArgs e)
    {
        //Creating a variable to use for logic in this if statement
        if (BindingContext is ProfileViewModel getVM)
        {
            //take the user current password for security check, then store the newPassword in a variable
            string currentPassword = await DisplayPromptAsync("Change Password", "Provide your current password:", keyboard: Keyboard.Password);
            string newPassword = await DisplayPromptAsync("Change Password", "Enter your new password:", keyboard: Keyboard.Password);
            if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(currentPassword)) //if neither are blank
            {
                if (newPassword == getVM.Password) //if they enter the same password throw error
                {
                    await Toast.Make("Please use a different password than last time").Show();
                    return;
                }
                else if (currentPassword == getVM.Password) // if they pass the security validation, update the password
                {
                    //Call the viewmodel method for updating the first name in HavenlyDatabase
                    await getVM.UpdatePasswordAsync(newPassword);
                    await Toast.Make("Password updated successfully").Show();
                }
            }
            else //If either field are left empty
            {
                await Toast.Make("You must fill out both fields", ToastDuration.Short).Show();
                return;
            }
        }
    }

    public async void DeactivateAccount(object sender, EventArgs e)
    {
        //Creating a variable to use for logic in this if statement
        if (BindingContext is ProfileViewModel getVM)
        {
            //take the user current password for security check, then store the newPassword in a variable
            string currentEmail = await DisplayPromptAsync("Deactivate Account", "Enter your current email:");
            string currentPassword = await DisplayPromptAsync("Are you sure?", "Confirm your current password:", keyboard: Keyboard.Password);
            if (!string.IsNullOrWhiteSpace(currentEmail) && !string.IsNullOrWhiteSpace(currentPassword)) //if neither are blank
            {
                if (currentPassword == getVM.Password && currentEmail == getVM.Email) // if they pass the security validation, update the password
                {
                    //Call the viewmodel method for updating the first name in HavenlyDatabase
                    await getVM.DeactivateAccountAsync();
                    await Toast.Make("Your account has been deactivated").Show();
                    Application.Current.Windows[0].Page = new LoginView();
                }
                else
                {
                    await Toast.Make("Your email or password were incorrect").Show();
                    return;
                }
            }
            else //If either field are left empty
            {
                await Toast.Make("You must fill out both fields", ToastDuration.Short).Show();
                return;
            }
        }
    }

    public void Logout(object sender, EventArgs e)
    {
        //Creating a variable to use for logic in this if statement
        if (BindingContext is ProfileViewModel getVM)
        {
            Toast.Make("You have logged out", ToastDuration.Short).Show();
            getVM.LogoutAsync();
            Application.Current.Windows[0].Page = new LoginView();
        }
    }
}