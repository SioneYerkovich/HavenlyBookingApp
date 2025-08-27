using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using HavenlyBookingApp.Sessions;
using HavenlyBookingApp.Views;

namespace HavenlyBookingApp.Models.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //Constructor - Called automatically when the relative view is accessed. Retrieves the database and session instance
        public LoginViewModel(HavenlyDatabase database, UserSession session)
        {
            LoginUserCommand = new Command(async () => await LoginUserAsync());
            _database = database;
            _session = session;
            display();
        }

        //Declaring that there is a button or feature that will use this command
        public ICommand LoginUserCommand { get; }

        //Creating variables that will just be used for the logic in this script
        private string _email;
        private string _password;
        private readonly HavenlyDatabase _database;
        private readonly UserSession _session;

        public event PropertyChangedEventHandler PropertyChanged;

        //Method to report the property change, this is happening REAL TIME
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        //Creating variables that are globally accessible and will be the point of reference for bindings
        public string email
        {
            //get = read action, get the current value of _newCategoryName
            get => _email;
            //set = write action
            set
            {
                //When it gets changed
                if (_email != value)
                {
                    //Update its value
                    _email = value;
                    //Reports the property change
                    OnPropertyChanged();
                }
            }
        }

        public string password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        //Method to log in a user
        public async Task LoginUserAsync()
        {
            var success = await VerifyUserAsync(email, password); //verify the user exists
            if (success) //if they exist
            {
                var user = await _database.GetUserAsync(email); //retrieve the user from DB
                _session.SetUser(user); //Assign the user as the current session
                Application.Current.Windows[0].Page = new AppShell(_session); //Open the application
            }
        }

        //Method to verify/validate the user details
        public async Task<bool> VerifyUserAsync(string email, string password)
        {
            var matchingUser = await _database.GetUserLoginAsync(email, password); //retrieve the first match for the given details from DB
            if (matchingUser != null) //if there's a match
            {
                await Toast.Make("You have been logged in").Show();
                return true;
            }
            await Toast.Make("Your email or password are incorrect").Show();
            return false;
        }

        //This is a developer method that is utilized to see database information in the console when logging in
        private async void display()
        {
            var allUsers = await _database.GetUsersAsync();
            foreach (var user in allUsers)
            {
                Console.WriteLine($"Id: {user.userID}, Email: {user.email}, Name: {user.fName} {user.lName}");
            }
        }
}

}