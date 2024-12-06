using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Data;

namespace MathMindCare.Controllers
{
    public class LoginController : Controller
    {
        ClsUser u = new ClsUser();
        public IActionResult Login(string ID)
        {
            return View();
        }

        [HttpPost]
        public Users ValidateUser1(string UserID, string Password, string userType)
        {
            Users usr = u.validateUser(UserID, Password, userType);


            if (usr.USERID != null)
            {
                HttpContext.Session.SetString("USERID", usr.USERID);
                HttpContext.Session.SetString("USERTYPE", usr.LOGINTYPE);
                HttpContext.Session.SetString("USERNAME", usr.NAME);
                HttpContext.Session.SetString("PASSWORD", usr.PASSWORD);

                string user = HttpContext.Session.GetString("USERID").ToString();

            }
            return usr;
        }

        [HttpPost]
        public string NewUser(string UserID1, string Password1, string userType1, string EmailID1, string Name, string Phone)
        {
            string result = u.RegisterUser(UserID1, Password1, userType1, EmailID1, Name, Phone);

            return result;
        }


        [HttpPost]
        public string Changepwd(string UserID, string Password)
        {
            string result = u.Changepwd(UserID, Password);

            return result;
        }


        [HttpPost]
        public string DisableUser(string UserID, string status)
        {
            string result = u.DisableUser(UserID, status);

            return result;
        }
        


    }
}
