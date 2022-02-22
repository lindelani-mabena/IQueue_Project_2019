using System.Collections.Generic;
using System.Linq;
using WebApplication.Code;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;
        public UserRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }
        public List<UserModel> GetUsers()
        {
            var userModels = new List<UserModel>();
            var users = _db.Users;

            foreach (var user in users)
            {
                userModels.Add((UserModel)_modelFactory.CreateUserModel(user));
            }
            return userModels;
        }

        public UserModel Create(UserModel userModel)
        {
            if (UserExist(userModel.Email)) return null;

            var user = new User()
            {
                FirstName = userModel.FirstName,
                Title = userModel.Title,
                Email = userModel.Email,
                Password_Hash = Security.ComputeSha256Hash(
                        userModel.PasswordHash),
                Initial_Date = userModel.InitialDate,
                Last_Update = userModel.LastUpdate
            };
            _db.Users.InsertOnSubmit(user);
            _db.SubmitChanges();

            return GetUserByEmail(user.Email);
        }
        private UserModel GetUserByEmail(string email)
        {
            var queryUser = _db.Users.FirstOrDefault(x => x.Email == email);
            return (UserModel) _modelFactory.CreateUserModel(queryUser);
        }
        private bool UserExist(string email)
        {
            var queryUsers = from x in _db.Users
                             where x.Email == email
                             select x;
            var user = queryUsers.FirstOrDefault();

            return (user != null);
        }
        public UserModel Authenticate(string email, string password)
        {
            var userModel = GetUserByEmail(email);
            if (userModel == null) return null;
            return !ValidatePassword(userModel.PasswordHash, 
                Security.ComputeSha256Hash(password)) ? null : userModel;
        }
        private static bool ValidatePassword(string input, string password)
        {
            if (string.IsNullOrWhiteSpace(input) ||
                string.IsNullOrWhiteSpace(password)) return false;
            return string.Equals(input, password);
        }
        public UserModel UpdateUser(int userId, UserModel userModel)
        {
            var queryUser = QueryUserById(userId);
            if (queryUser == null) return null;

            queryUser.FirstName = userModel.FirstName;
            queryUser.LastName = userModel.LastName;
            queryUser.Email = userModel.Email;
            queryUser.ActiveAccount = userModel.AccountActive;
            queryUser.Online = userModel.Online;
            queryUser.LoginCount = userModel.LoginCount;
            queryUser.Title = userModel.Title;
            queryUser.Last_Update = userModel.GetLastUpdateDate();
            
            _db.SubmitChanges();
            return (UserModel) _modelFactory.CreateUserModel(queryUser);
        }
        private User QueryUserById(int userId)
        {
            var queryUser = _db.Users.FirstOrDefault(x => x.User_Id == userId);
            return queryUser;
        }
        public UserModel GetUser(int userId)
        {
            var queryUser = _db.Users.FirstOrDefault(x => x.User_Id == userId);
            return queryUser == null ? null : (UserModel) _modelFactory.CreateUserModel(queryUser);
        }
        public UserModel DeleteUser(int userId)
        {
            var queryUser = QueryUserById(userId);
            if (queryUser == null) return null;
            _db.Users.DeleteOnSubmit(queryUser);
            _db.SubmitChanges();
            return (UserModel) _modelFactory.CreateUserModel(queryUser);
        }
    }
}