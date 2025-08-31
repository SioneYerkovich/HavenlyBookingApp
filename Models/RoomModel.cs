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
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
