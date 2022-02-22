using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface IUserRepository
    {
        List<UserModel> GetUsers();
        UserModel Create(UserModel userModel);
        UserModel Authenticate(string email, string password);
        UserModel UpdateUser(int userId, UserModel userModel);
        UserModel GetUser(int userId);
        UserModel DeleteUser(int userId);
    }
}
