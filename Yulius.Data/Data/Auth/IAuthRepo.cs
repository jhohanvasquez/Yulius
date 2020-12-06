using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yulius.Data.Models;

namespace Yulius.Data.Data.Auth
{
    public interface IAuthRepo
    {
        Task<Result> Login(string email, string password);

        Task<Result> Register(User user, string password);

        Task<Result> isUserExist(string email);
    }
}
