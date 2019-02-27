using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;

namespace Shop.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userMAnager;

        public UserHelper(UserManager<User> userMAnager)
        {
            this.userMAnager = userMAnager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userMAnager.CreateAsync(user, password);
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
