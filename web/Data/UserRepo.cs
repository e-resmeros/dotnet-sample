using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Config;
using api.Core.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(AppDb Db) : base(Db)
        {
        }

        public async Task<User> CheckVoidTypeUser(string mobileLogin = "", string ccc = "")
        {
            string[] types = {"adm", "void"};
            return await this.FindByCondition(user => types.Contains(user.Type.ToLower())
                                                && user.MobileLogin == mobileLogin
                                                && user.Status == "A")
                                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByMobileLoginAsync(User user)
        {
            return await this.FindByCondition(x => x.MobileLogin == user.MobileLogin
                                                && x.Status == "A")
                                .FirstOrDefaultAsync();
        }

        public User GetUserByMobileLogin(User user)
        {
            return this.FindByCondition(x => x.MobileLogin == user.MobileLogin
                                            && x.Status == "A")
                            .FirstOrDefault();
        }

        public void UpdateLastLoginAsync(User user)
        {
            var now = DateTime.Now;
            user.LastLoginDate = Convert.ToDateTime(now);
            Update(user);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void AddUserWithReturnAsync(User user)
        {
            Create(user);
        }

        public async Task<User> AuthenticateUser(User user)
        {
            var loggedUser = await FindByCondition(x => x.UserName == user.UserName
                                                    && x.Type.ToLower().Equals("adm")).FirstOrDefaultAsync();

            if (null == loggedUser)
            {
                return null;
            }

            if (!VerifyPassword(user.Password, loggedUser.Password, ""))
            {
                return null;
            }

            return loggedUser;
        }

        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] base64Decoded = System.Convert.FromBase64String(storedHash);
            string res = Encoding.UTF8.GetString(base64Decoded);
            // char[] res2 = new char[UTF8Encoding.UTF8.GetDecoder().GetCharCount(base64Decoded, 0, base64Decoded.Length)];
            // Encoding.UTF8.GetChars(base64Decoded, 0, base64Decoded.Length, res2, 0);
            // string res2Str = new string(res2);

            if (res != password)
            {
                return false;
            }
            
            return true;
        }

        public User GetAdminByUserName(User user)
        {
            return this.FindByCondition(x => x.UserName == user.UserName
                                                && x.Status == "A"
                                                && x.Type.ToLower().Equals("adm"))
                                .FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetEmailsForCC(string company)
        {
            string[] types = {"rpt", "void"};
            return await this.FindByCondition(user => types.Contains(user.Type.ToLower())
                                                && !"NA".Equals(user.Email)
                                                && user.Company.Equals(company)
                                                && user.Status == "A")
                                .ToListAsync();
        }
    }
}