using System.Net;
using System.Web;
using System.Xml.Linq;
using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MathMindCare.Controllers
{
    public class DoctorProfController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        ClsUser u = new ClsUser();

        [HttpPost]

        //public string Profile(string UserID, string Specialization, string Clinicadd, string EmailID, string Name, string Phone, string Address, string Photo, string Experience,string imageData)
        public string Profile(DoctorProfile dr)
        {
            var user = HttpContext.Session.GetString("USERID");
            string result = "";
            if (user == null)
            {
                result = "Session Expired";
            }
            else
            {
                dr.USERID = user;
                //result = u.Doctorprofile(user, Specialization, Clinicadd, EmailID, Name, Phone, Address, imageData, Experience);
                result = u.Doctorprofile(dr);
            }
            return result;
        }
    }
}


