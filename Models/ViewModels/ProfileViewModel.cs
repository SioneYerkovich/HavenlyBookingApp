using CommunityToolkit.Maui.Alerts;
using HavenlyBookingApp.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Models.ViewModels
{
    public class ProfileViewModel :INotifyPropertyChanged
    {
        //Constructor - Called automatically when the viewmodel is accessed. Retrieves the database and session instance
        public ProfileViewModel(HavenlyDatabase database, UserSession session)
        {
            _session = session;
            _database = database;
        }

        //Creating variables that will just be used for the logic in this script
        public string FirstName => _session.CurrentUser.FName;
        public string LastName => _session.CurrentUser.LName;
        public string Email => _session.CurrentUser.Email;
        public string Password => _session.CurrentUser.Password;
        public string CurrentDate => DateTime.Now.ToString("dddd, dd MMMM yyyy");
        private readonly UserSession _session;
        private readonly HavenlyDatabase _database;
        public event PropertyChangedEventHandler PropertyChanged;

        //Method to report the property change, this is happening REAL TIME
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        //This method formats the user input to a display friendly format
        private string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Trim();
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        public async Task<bool> InputValidation(string fName = null, string lName = null, string email = null, string password = null)
        {   //Check first name format
            if (!string.IsNullOrWhiteSpace(fName) && !Regex.IsMatch(fName, @"^[A-Za-z]+$"))
            {
                return false;
            }
            //Check last name format
            if (!string.IsNullOrWhiteSpace(lName) && !Regex.IsMatch(lName, @"^[A-Za-z]+$"))
            {
                return false;
            }

            //Check email format
            if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return false;
            }

            //User has passed the validations
            return true;
        }

        //Method to update the user first name in the database
        public async Task UpdateFirstNameAsync(string newName)
        {
            //validate the name
            var formattedName = Capitalize(newName); //Format the name
            _session.CurrentUser.FName = formattedName; //Update the session instance first
            await _database.UpdateUserAsync(_session.CurrentUser); //use the session instance to update in the database
            OnPropertyChanged(nameof(FirstName)); //Call the real-time tracker for live updates on UI
        }

        //Method to update the user last name in the database
        public async Task UpdateLastNameAsync(string newName)
        {
            var formattedName = Capitalize(newName);
            _session.CurrentUser.LName = formattedName;
            await _database.UpdateUserAsync(_session.CurrentUser);
            OnPropertyChanged(nameof(LastName));
        }

        //Method to update the user email in the database
        public async Task UpdateEmailAsync(string newEmail)
        {
            var formattedEmail = newEmail.ToLower();
            _session.CurrentUser.Email = formattedEmail;
            await _database.UpdateUserAsync(_session.CurrentUser);
            OnPropertyChanged(nameof(Email));
        }

        //Method to update the user password in the database
        public async Task UpdatePasswordAsync(string newPassword)
        {
            _session.CurrentUser.Password = newPassword;
            await _database.UpdateUserAsync(_session.CurrentUser);
        }

        //Method to deactivate the user account (delete)
        public async Task DeactivateAccountAsync()
        {
            await _database.DeleteUserAsync(_session.CurrentUser);
            _session.ClearUser();
        }

        //Method to log a user out
        public void LogoutAsync()
        {
            _session.ClearUser();
        }
    }

}
