using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Models.UserViewBinding;

namespace RestaurantSystem.Services
{
    public class UserOperation
    {
        public AppDbContext _db { get; set; }
        public UserOperation(AppDbContext db)
        {
            _db = db;
        }
        public async Task<int> CheckUserLogin(string Email, string password)
        {
            var user = await _db.Users
      .FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null)
            {
                return -1; // email not found
            }

            PasswordHasher<string> passwordHasher =
                new PasswordHasher<string>();

            var result = passwordHasher.VerifyHashedPassword(
                null,
                user.Password,
                password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return 0; // wrong password
            }

            if (user.Role == "admin")
            {
                return 1; // admin
            }

            return user.Id; // normal user
        }
        public async Task RegisterUserAccount(UserViewBinding u)
        {
            // here consider all user register from site is client so for admin the Manager created here acc using minimal api's
            User user = new User()
            {
                Email = u.Email,
                Name = u.Name,
                Role = "user",
                Password = u.Password,
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async Task<User> LoadUser(int id)
        {
            return await _db.Users.FindAsync(id);
        }
    }
}
