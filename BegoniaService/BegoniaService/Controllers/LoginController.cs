using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BegoniaService.Models;
using BegoniaService.Dots;

namespace BegoniaService.Controllers
{
    public class LoginController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        public async Task<string> ManagerLogin(LoginInf loginInf)
        {
            var temp = db.Users.Where(u => u.Account == loginInf.Account && u.Password == loginInf.Password);
            if (!temp.Any())
            {
                return "failed";
            }
            if (temp.ToArray()[0].Identity == "user")
            {
                return "failed";
            }
            return "success";
        }

        public async Task<string> UserLogin(LoginInf loginInf)
        {
            var temp = db.Users.Where(u => u.Account == loginInf.Account && u.Password == loginInf.Password);
            if (!temp.Any())
            {
                return "failed";
            }
            if (temp.ToArray()[0].State == "stop")
            {
                return "stop";
            }
            return "success";
        }
    }
}
