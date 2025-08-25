using HavenlyBookingApp.Models;
using HavenlyBookingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Sessions
{
    public class UserSession
    {
        public bool IsLoggedIn { get; set; } = false;
        public UserModel CurrentUser { get; set; }

        //This method sets the current user as the session
        public void SetUser(UserModel user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
        }

        //This method clears the session
        public void ClearUser()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }
    }
}
