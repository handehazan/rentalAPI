using bnbAPI.model;

namespace bnbAPI.Source.Svc
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
        void AddUser(User user);
    }
}
