using System.Threading.Tasks;

namespace DatingApp.api.Models
{
    public interface IAuthRepository
    {
         Task<User> register(User user,string password);
         Task<User> login(string userName,string password);
         Task<bool> userExists(string userName);
    }
}