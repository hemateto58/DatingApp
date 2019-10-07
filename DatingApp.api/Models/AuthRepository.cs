using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Models
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _DataContext { get; set; }
        public AuthRepository(DataContext DataContext)
        {
            _DataContext = DataContext;

        }
        public async Task<User> login(string userName, string password)
        {
            var userExists=await _DataContext.users.FirstOrDefaultAsync(x=>x.userName==userName);
            if (userExists==null)
                return null;
            if (!VarifiePasswordHash(password,userExists.passwordHash,userExists.passwordSalt))
            {
                return null;
            }
            return userExists;
        }

        private bool VarifiePasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
              var  ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              for (int i = 0; i < ComputeHash.Length; i++)
              {
                  if (ComputeHash[i]!=passwordHash[i])
                  {
                      return false;
                  }
                    
              }
              return true;
            }
        }

        public async Task<User> register(User user, string password)
        {

            byte[] passwordHash, passwordSalt;
            generatePassword(password, out passwordHash, out passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            await _DataContext.AddAsync(user);
            await _DataContext.SaveChangesAsync();
            return user;
        }

        private void generatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> userExists(string userName)
        {
          if (await _DataContext.users.AnyAsync(x=>x.userName==userName))
          {
              return true;
          }
          return false;
        }
    }
}