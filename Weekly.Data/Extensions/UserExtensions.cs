using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Repositiories;
using Weekly.Entities;

namespace Weekly.Data.Extensions
{
    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }

        public static User GetSingleByEmail(this IEntityBaseRepository<User> userRepository, string email)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Email == email);

        }
    }
}
