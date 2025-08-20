using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Models
{
    public class BookingModel
    {
        [PrimaryKey, AutoIncrement]
        public int bookingID { get; set; }
        public int userID { get; set; }
        public int roomID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
