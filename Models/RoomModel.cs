using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Models
{
    public class RoomModel
    {
        [PrimaryKey, AutoIncrement]
        public int roomID { get; set; }
        public string roomType { get; set; }
        public int bedCount { get; set; }
        public int price { get; set; }
        public string availabilityStatus { get; set; }
    }
}
