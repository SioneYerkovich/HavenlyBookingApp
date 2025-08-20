using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenlyBookingApp.Models;

namespace HavenlyBookingApp
{
    public class HavenlyDB
    {
        SQLiteAsyncConnection database;

        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var usersTable = await database.CreateTableAsync<UserModel>();
            var roomsTable = await database.CreateTableAsync<RoomModel>();
            var bookingsTable = await database.CreateTableAsync<BookingModel>();
        }

        //Method to retrieve all users from the database
        public async Task<List<UserModel>> GetUsersAsync()
        {
            await Init();
            return await database.Table<UserModel>().ToListAsync();
        }

        //Method to validate and retrieve a user from the database (primarily for logging in)
        public async Task<UserModel> GetUserAsync(string email, string password)
        {
            await Init();
            return await database.Table<UserModel>().Where(user => user.email == email && user.password == password).FirstOrDefaultAsync();
        }

        //Method to save a user in the database, must pass a UserModel object to function correctly
        public async Task<int> SaveUserAsync(UserModel item)
        {
            await Init();
            if (item.userID != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }

        //Method to delete a user in the database, must pass a UserModel object to function correctly
        public async Task<int> DeleteUserAsync(UserModel item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }

        //Method to update a user in the database, must pass a UserModel object to function correctly
        public async Task<int> UpdateUserAsync(UserModel item)
        {
            await Init();
            return await database.UpdateAsync(item);
        }

        //Method to retrieve all rooms from the database
        public async Task<List<RoomModel>> GetRoomsAsync()
        {
            await Init();
            return await database.Table<RoomModel>().ToListAsync();
        }

        //Method to save a room in the database, must pass a RoomModel object to function correctly
        public async Task<int> SaveRoomAsync(RoomModel item)
        {
            await Init();
            if (item.roomID != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }

        //Method to delete a room in the database, must pass a RoomModel object to function correctly
        public async Task<int> DeleteRoomAsync(RoomModel item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }

        //Method to update a room in the database, must pass a RoomModel object to function correctly
        public async Task<int> UpdateRoomAsync(RoomModel item)
        {
            await Init();
            return await database.UpdateAsync(item);
        }

        //Method to retrieve all bookings from the database
        public async Task<List<BookingModel>> GetBookingsAsync()
        {
            await Init();
            return await database.Table<BookingModel>().ToListAsync();
        }

        //Method to save a booking in the database, must pass a BookingModel object to function correctly
        public async Task<int> SaveBookingAsync(BookingModel item)
        {
            await Init();
            if (item.bookingID != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }
        
        //Method to delete a booking in the database, must pass a BookingModel object to function correctly
        public async Task<int> DeleteBookingAsync(BookingModel item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }

        //Method to update a booking in the database, must pass a BookingModel object to function correctly
        public async Task<int> UpdateBookingAsync(BookingModel item)
        {
            await Init();
            return await database.UpdateAsync(item);
        }

        //This method is theoretical, ignore for now
        //public async Task<List<BookingModel>> GetBookedRoomsAsync()
        //{
        //    await Init();
        //    return await database.Table<BookingModel>().Where(booking => booking.IsCompleted).ToListAsync();
        //}
    }
}
