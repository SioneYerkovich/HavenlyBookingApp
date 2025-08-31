using HavenlyBookingApp.Sessions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenlyBookingApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel(HavenlyDatabase database, UserSession session) 
        {
            _session = session;
            _database = database;

            //// Hardcoded rooms and bookings just for testing dashboard logic
            //Rooms = new ObservableCollection<RoomModel>
            //{
            //    new RoomModel { RoomID = 1, RoomNumber = 101, RoomType = "Studio", Capacity = 1, Price = 150.00, IsAvailable = false },
            //    new RoomModel { RoomID = 2, RoomNumber = 102, RoomType = "Suite", Capacity = 2, Price = 200.00, IsAvailable = false },
            //    new RoomModel { RoomID = 3, RoomNumber = 103, RoomType = "Deluxe Suite", Capacity = 4, Price = 250.00, IsAvailable = false }
            //};

            //BookingItems = new ObservableCollection<BookingModel>
            //{
            //    new BookingModel { BookingID = 1, RoomID = 1, RoomNumber = 101, RoomType = "Suite", StartDate = "01-09-2025", EndDate = "05-09-2025" },
            //    new BookingModel { BookingID = 2, RoomID = 2, RoomNumber = 102, RoomType = "Studio", StartDate = "10-10-2025", EndDate = "12-10-2025" }
            //};
        }

        //Creating variables that will just be used for the logic in this script
        public string FirstName => _session.CurrentUser.fName;
        public string LastName => _session.CurrentUser.lName;
        private readonly UserSession _session;
        private readonly HavenlyDatabase _database;

        //These are creating a prototype list, but no actual tangible list exists
        public ObservableCollection<RoomModel> Rooms { get; set; }
        public ObservableCollection<BookingModel> BookingItems { get; set; }
    }
}
