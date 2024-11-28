using bnbAPI.Context;
using bnbAPI.model;

namespace bnbAPI.Source.Db
{
    public class UserAccess
    {
        private ApplicationDbContext _context;

        public UserAccess(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get user by username (used during login)
        public User GetUserByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            Console.WriteLine($"Retrieved User: {user?.Username}"); // Log username for debugging
            return user;
        }

        // Add a new user
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

