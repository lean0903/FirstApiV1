using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastucture.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly SocialMediaContext _context;
        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUser(int id)
        {

            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task InsertUser(User user)

        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdateUser(User user)
        {
            var currentUser = await this.GetUser(user.UserId);
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Telephone = user.Telephone;
            currentUser.IsActive = user.IsActive;
            var row = await _context.SaveChangesAsync();//devuelve las cantidad de rows modificadas
            return row > 0;
        }



        public async Task<bool> DeleteUser(int userId)
        {
            var currentUser = await this.GetUser(userId);
            _context.Remove(currentUser);
            return await _context.SaveChangesAsync() > 0;//devuelve las cantidad de rows elimininadas
        }
    }
}
