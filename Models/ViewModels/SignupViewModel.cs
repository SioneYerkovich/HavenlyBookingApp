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
using HavenlyBookingApp.Views;

namespace HavenlyBookingApp.Models.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        //Constructor - Called automatically when the viewmodel is accessed. Retrieves the database instance
        public SignupViewModel(HavenlyDatabase database)
        {
            CreateNewUserCommand = new Command(async () => await CreateNewUserAsync());
            _database = database;
        }

        //Declaring that there is a button or feature that will use this command
        public ICommand CreateNewUserCommand { get; }

        //Creating variables that will just be used for the logic in this script
        private string _newFirstName;
        private string _newLastName;
        private string _newEmail;
        private string _newPassword;
        private string _confirmPassword;
        private readonly HavenlyDatabase _database;

        public event PropertyChangedEventHandler PropertyChanged;

        //Method to report the property change, this is happening REAL TIME
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        //Creating variables that are globally accessible and will be the point of reference for bindings
        public string NewFirstName
        {
            //get = read action, get the current value of _newCategoryName
            get => _newFirstName;
            //set = write action
            set
            {
                //When it gets changed
                if (_newFirstName != value)
                {
                    //Update its value
                    _newFirstName = value;
                    //Reports the property change
                    OnPropertyChanged();
                }
            }
        }

        public string NewLastName
        {
            get => _newLastName;
            set
            {
                if (_newLastName != value)
                {
                    _newLastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewEmail
        {
            get => _newEmail;
            set
            {
                if (_newEmail != value)
                {
                    _newEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        //This method formats the user input to a display friendly format
        private string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Trim();
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        //Method to create a new user in the database
        public async Task CreateNewUserAsync()
        {
            if (!await SignupValidation()) //If the details provided fail validation
            {
                return;
            }
            else //Otherwise create a user
            {
                //This formats all of the user details prior to data entry
                var formattedFirstName = Capitalize(NewFirstName);
                var formattedLastName = Capitalize(NewLastName);
                var formattedEmail = NewEmail.ToLower();

                //Create a new usermodel and store in the DB
                await _database.SaveUserAsync(new UserModel
                {
                    FName = formattedFirstName,
                    LName = formattedLastName,
                    Email = formattedEmail,
                    Password = NewPassword,
                    AccountType = "User"
                });

                await Toast.Make("Account created successfully!").Show(); //Success confirmation
                Application.Current.Windows[0].Page = new LoginView(); //Redirect to Login
            }
        }

        //Method to validate this is a new account
        public async Task<bool> CheckUserExist(string NewEmail)
        {
            var matchingUser = await _database.GetUserAsync(NewEmail);
            if (matchingUser != null)
            {
                await Toast.Make("There is already an account registered with that email.").Show();
                return true;
            }
            return false;
        }

        public async Task<bool> SignupValidation()
        {
            // Check first name and last name (letters only)
            if (string.IsNullOrWhiteSpace(NewFirstName))
            {
                await Toast.Make("Please enter your first name").Show();
                return false;
            }

            if (!Regex.IsMatch(NewFirstName, @"^[A-Za-z]+$"))
            {
                await Toast.Make("First name must contain letters only").Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewLastName))
            {
                await Toast.Make("Please enter your last name").Show();
                return false;
            }

            if (!Regex.IsMatch(NewLastName, @"^[A-Za-z]+$"))
            {
                await Toast.Make("Last name must contain letters only").Show();
                return false;
            }

            //Check email format
            if (string.IsNullOrWhiteSpace(NewEmail) || !Regex.IsMatch(NewEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                await Toast.Make("Please enter a valid email format").Show();
                return false;
            }
            
            //Check if account exists
            if (await CheckUserExist(NewEmail))
            {
                return false;
            }

            //Check password fields
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                await Toast.Make("Password cannot be empty").Show();
                return false;
            }

            if (NewPassword != ConfirmPassword)
            {
                await Toast.Make("Passwords do not match").Show();
                return false;
            }

            //User has passed the validations
            return true;
        }
    }

}
