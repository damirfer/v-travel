using Model.ViewModels;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface IAuthService
    {
        bool CheckCredentials(LoginVM vm, ref int userId);
        UserDataVM GetData(int userId);
    }
    public class AuthService : IAuthService
    {
        private LastaContext db;

        public AuthService(LastaContext databaseContext)
        {
            db = databaseContext;
        }

        public bool CheckCredentials(LoginVM vm, ref int userId)
        {
            var user = db.Guide.FirstOrDefault(x => x.Username == vm.Username && x.PasswordHash == Helper.Helper.GenerateHash(x.PasswordSalt, vm.Password));

            if (user == null)
                return false;
            userId = user.GuideId;
            return true;
        }

        public UserDataVM GetData(int userId)
        {
            UserDataVM vm = new UserDataVM();
            vm.UserId = userId;
            var user = db.Guide.FirstOrDefault(x => x.GuideId == userId);
            vm.UserName = user.FirstName + " " + user.LastName;
            vm.UserProfile = Helper.Helper.GetImageUrl(user.PhotoUrl);
            vm.IsAdmin = user.IsAdmin;

            return vm;
        }

    }

}
