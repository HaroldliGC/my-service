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

        public async Task<object> UserLogin(LoginInf loginInf)
        {
            LoginReturnInf rec = new LoginReturnInf() { type = "failed" };
            var temp = db.Users.Where(u => u.Account == loginInf.Account && u.Password == loginInf.Password);
            if (!temp.Any())
            {
                rec.message = "用户名密码不正确";
                return rec;
            }
            User user = temp.ToArray()[0];
            int userId = user.Id;
            string userName = user.Name;
            if (user.State == "stop")
            {
                rec.message = "该账户已被停用";
                return rec;
            }
            rec.type = "success";
            rec.message = "登陆成功";
            rec.userId = userId;
            rec.userName = userName;
            return rec;
        }
    }
}
