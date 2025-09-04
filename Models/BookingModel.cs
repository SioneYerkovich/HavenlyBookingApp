﻿using SQLite;
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
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        [Ignore]
        public string RoomNumber { get; set; } //THIS IS FOR TESTING, WILL BE REMOVED ONCE BOOKING FEATURE IS COMPLETE

        [Ignore]
        public string RoomType { get; set; } //THIS IS FOR TESTING, WILL BE REMOVED ONCE BOOKING FEATURE IS COMPLETE

        [Ignore]
        public string UserFullName { get; set; } //THIS IS FOR TESTING, WILL BE REMOVED ONCE BOOKING FEATURE IS COMPLETE
    }
}
