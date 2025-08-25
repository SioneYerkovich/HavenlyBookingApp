using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int userID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accountType { get; set; }
    }
}
