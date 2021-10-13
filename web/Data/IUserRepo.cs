using System.Collections.Generic;
using System.Threading.Tasks;
using api.Core.Data;
using api.Models;

namespace api.Data
{
    public interface IUserRepo : IRepositoryBase<User>
    {
        public Task<User> GetUserByMobileLoginAsync(User user);

        public User GetUserByMobileLogin(User user);

        public User GetAdminByUserName(User user);

        public Task<User> AuthenticateUser(User user);

        public void UpdateLastLoginAsync(User user);

        public Task<User> CheckVoidTypeUser(string mobileLogin = "", string ccc = "");

        public Task<IEnumerable<User>> GetEmailsForCC(string company);

        public Task SaveAsync(); 

    }
}