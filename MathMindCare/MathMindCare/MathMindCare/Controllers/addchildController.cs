using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathMindCare.Controllers
{
    public class addchildController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        ClsChildProfile u = new ClsChildProfile();

        public string NewChild(ChildProfile profile)
        {
            string result ="";

            string user = HttpContext.Session.GetString("USERID").ToString();
            profile.USERID = user;
            if (user != "")
            {
                result = u.AddChild(profile);

            }

            return result;
        }
    }
}
