using bnbAPI.Context;
using bnbAPI.model;
using bnbAPI.Source.Db;
using System.Security.Cryptography;
using System.Text;

namespace bnbAPI.Source.Svc
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User ValidateUser(string username, string password)
        {
            UserAccess access = new UserAccess(_context);

            var user = access.GetUserByUsername(username);
            if (user == null)
            {
                return null; // User not found
            }

            // Directly compare plain-text passwords
            if (user.Password != password)
            {
                return null; // Invalid password
            }

            return user; // Valid user
        }

        public void AddUser(User user)
        {
            UserAccess access = new UserAccess(_context);

            // Directly save the plain-text password
            access.AddUser(user);
        }
    }
}
