using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
            //This is to create the real tangible object based on the prototype
            BookingItems = new ObservableCollection<BookingModel>();
            _session = session;
            _database = database;
            _ = InitializeDataAsync();
            _ = CheckBookingsInDatabaseAsync();
        }

        //Creating variables that will only be used for the logic in this script
        public string FirstName => _session.CurrentUser.FName;
        public string LastName => _session.CurrentUser.LName;
        public string Password => _session.CurrentUser.Password;
        private readonly UserSession _session;
        private readonly HavenlyDatabase _database;

        //These are creating a prototype list, but no actual tangible list exists
        public ObservableCollection<RoomModel> Rooms { get; set; }
        public ObservableCollection<BookingModel> BookingItems { get; set; }



        //METHODS
        //This method gets all data ready for the user on app launch
        private async Task InitializeDataAsync()
        {
            await LoadBookingsAsync();
        }

        //This method is temporary, it creates fake rooms to test booking retrieval logic
        public async Task CreateRoomsAsync()
        {
            var existingRooms = await _database.GetRoomsAsync();
            if (existingRooms.Count != 0)
            {
                return;
            }

            //Hardcoded rooms just for testing dashboard logic
            Rooms = new ObservableCollection<RoomModel>
           {
                new RoomModel { RoomNumber = "101", RoomType = "Studio", Capacity = 1, Price = 150.00, IsAvailable = true },
                new RoomModel { RoomNumber = "102", RoomType = "Suite", Capacity = 2, Price = 200.00, IsAvailable = true },
                new RoomModel { RoomNumber = "103", RoomType = "Deluxe Suite", Capacity = 4, Price = 250.00, IsAvailable = true }
           };

            foreach (var room in Rooms)
            {
                await _database.SaveRoomAsync(room);
            }
        }

        //This method is temporary, it creates fake bookings to test booking retrieval logic
        public async Task CreateBookingsAsync()
        {
            var existingBookings = await _database.GetBookingsAsync();
            if (existingBookings.Count != 0)
            {
                return;
            }

            //Hardcoded bookings just for testing dashboard logic
            BookingItems = new ObservableCollection<BookingModel>
            {
                new BookingModel { UserID = _session.CurrentUser.UserID ,RoomID = 1, RoomNumber = "101", RoomType = "Suite", StartDate = "01-09-2025", EndDate = "05-09-2025" },
                new BookingModel { UserID = _session.CurrentUser.UserID ,RoomID = 2, RoomNumber = "102", RoomType = "Studio", StartDate = "10-10-2025", EndDate = "12-10-2025" }
            };

            foreach (var booking in BookingItems)
            {
                await _database.SaveBookingAsync(booking);
            }
        }

        //This method is temporary, it will display all bookings and rooms in the database within the console
        public async Task CheckBookingsInDatabaseAsync()
        {
            var bookings = await _database.GetBookingsAsync(); // Fetch all bookings
            var rooms = await _database.GetRoomsAsync();

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found in the database.");
                return;
            }

            if (rooms.Count == 0)
            {
                Console.WriteLine("No rooms found in the database.");
                return;
            }

            Console.WriteLine("Bookings in database:");

            foreach (var booking in bookings)
            {
                Console.WriteLine($"BookingID: {booking.BookingID}, UserID: {booking.UserID}, RoomID: {booking.RoomID}, RoomNumber: {booking.RoomNumber}, RoomType: {booking.RoomType}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate}");
            }

            Console.WriteLine("Rooms in database:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"RoomID: {room.RoomID}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType}, Capacity: {room.Capacity}, Price: {room.Price}, IsAvailable: {room.IsAvailable}");
            }
        }

        //Method to retrieve all bookings within the database for a user
        public async Task LoadBookingsAsync()
        {
            //Fetch database info
            var bookings = await _database.GetBookingsAsync();
            var users = await _database.GetUsersAsync();
            var rooms = await _database.GetRoomsAsync();

            //Clear the collection for a clean slate
            BookingItems.Clear();

            //Re-populate the observablecollection with fresh data
            foreach (var b in bookings)
            {
                var user = users.FirstOrDefault(u => u.UserID == b.UserID); //Find the first user that has a matching UserID in bookings
                var room = rooms.FirstOrDefault(r => r.RoomID == b.RoomID); //Find the first room that has a matching RoomID in bookings

                //Add booking to the collection with extra details for UI bindings
                BookingItems.Add(new BookingModel
                {
                    BookingID = b.BookingID,
                    UserID = b.UserID,
                    RoomID = b.RoomID,
                    UserFullName = user.FName + " " + user.LName,
                    RoomType = room.RoomType,
                    RoomNumber = room.RoomNumber.ToString(),
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                });
            }
        }

        public async Task CancelBookingAsync(BookingModel booking)
        {
            if (BookingItems.Contains(booking))
            {
                BookingItems.Remove(booking);
                await _database.DeleteBookingAsync(booking);
                await LoadBookingsAsync();
            }
        }
    }
}
