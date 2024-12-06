using Emgu.CV.Features2D;
using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MathMindCare.Controllers
{
    public class HomeController : Controller
    {
        ClsUser u = new ClsUser();
        private readonly ILogger<HomeController> _logger;

        ClsChildProfile ObjClsChildProfile = new ClsChildProfile();
        ClsQuestionnaire ObjClsQuestionnaire = new ClsQuestionnaire();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var user = HttpContext.Session.GetString("USERID");
            if (user == null)
            {
                HttpContext.Session.SetString("USERID", "");
            }
            return View();
        }

        public IActionResult dashboard()
        {
            ChildProfile profile = new ChildProfile();
            string user = "";
            if (HttpContext.Session.GetString("USERID") != null)
                user = HttpContext.Session.GetString("USERID").ToString();
            profile.childProfileList = ObjClsChildProfile.LoadAssignedProfileList(user);
            return View(profile);
        }
        public IActionResult refer(string ID)
        {
            DoctorProfile profile = new DoctorProfile();
            string user = "";
            if (ID != null && ID != "0" && ID != "")
            {
                ID = HttpContext.Session.GetString("CHILDID").ToString();
            }
            if (HttpContext.Session.GetString("USERID") != null)
                user = HttpContext.Session.GetString("USERID").ToString();
            profile.CHILDID = ID;

            profile.doctorProfile = u.LoadDoctorProfileList(user, ID);
            return View(profile);
        }


        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult ManageQuestionnaire(string ID)
        {
            Questionnaire profile = new Questionnaire();
            if (ID != null && ID != "")
            {
                profile = ObjClsQuestionnaire.GetQuestion(ID);
            }

            profile.questionnaire = ObjClsQuestionnaire.LoadQuestion("0", "0");

            return View(profile);

        }
        public IActionResult ManageUsers()
        {
            ClsUser user = new ClsUser();
            List<Users> quest = u.LoadUsersList();
            Users usr = new Users();
            usr.userList = quest;
           


            return View(usr);
        }


        public IActionResult Questionnaire(string ID)

        {
            ClsQuestionnaire ObjClsQuestionnaire = new ClsQuestionnaire();

            List<Questionnaire> Listques = new List<Questionnaire>();
            Questionnaire ques = new Questionnaire();
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            Listques = ObjClsQuestionnaire.LoadQuestion("", ID);
            ques.questionnaire = Listques;
            ques.CHILDID = CHILDID;
            return View(ques);
        }
        public IActionResult levels(string ID)
        {
            HttpContext.Session.SetString("CHILDID", ID);

            Questionnaire ques = new Questionnaire();
            ques.CHILDID = ID;
            return View(ques);
        }

        public IActionResult Instructions()
        {
            //HttpContext.Session.SetString("CHILDID", ID);
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            Questionnaire ques = new Questionnaire();
            ques.CHILDID = CHILDID;
            return View(ques);
        }

        public IActionResult InstructionsWB()
        {
            //HttpContext.Session.SetString("CHILDID", ID);
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            Questionnaire ques = new Questionnaire();
            ques.CHILDID = CHILDID;
            return View(ques);
        }

        public IActionResult Whiteboard(string ID, string type)
        {
            ClsChildProblems ObjClsChildProblems = new ClsChildProblems();

            List<WhiteBoard> Listques = new List<WhiteBoard>();
            WhiteBoard ques = new WhiteBoard();
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            Listques = ObjClsChildProblems.LoadWBNextQuestion(ID, CHILDID, type);
            //ques.questionnaire = Listques;
            if (Listques.Count == 0)
                return RedirectToAction("Result");
            else
                return View(Listques[0]);

        }

        public IActionResult Result(string ID)
        {
            ChildProfile profile = new ChildProfile();
            string user = "";
            if (HttpContext.Session.GetString("USERID") != null)
                user = HttpContext.Session.GetString("USERID").ToString();
            var CHILDID = ID;
            if (CHILDID == null || CHILDID == "" || CHILDID == "0")
                CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            profile = ObjClsChildProfile.GETChildProfile(CHILDID, user);

            ClsChildProblems ObjClsChildProblems = new ClsChildProblems();
            ResultModel ques = new ResultModel();
            ques.NAME = profile.NAME;
            ques.ADDRESS = profile.ADDRESS;
            ques.DOB = profile.DOB;
            ques.REFUSERID = profile.REFUSERID;
            if (profile.GENDER == "1")
                ques.GENDER = "Male";
            if (profile.GENDER == "2")
                ques.GENDER = "Female";
            ques.PHNO = profile.PHNO;
            ques.EMAIL = profile.EMAIL;
            ques.CHILDID = CHILDID;
            ques.childProblems = ObjClsChildProblems.LoadChileProblemResult(CHILDID);
            if(ques.childProblems!=null && ques.childProblems.Count > 0)
            {
                ques.TOTALSCORE = ques.childProblems[0].TOTALSCORE;
                ques.EARNEPOINTS = (Convert.ToDouble(ques.childProblems[0].SCORED) / Convert.ToDouble(ques.childProblems[0].TOTALSCORE) * 100).ToString("######.00");

            }

            ques.whiteBoard = ObjClsChildProblems.LoadWhiteBpardResult(CHILDID);
            ques.doctorsList = ObjClsChildProblems.LoadDoctorsList(user);
            ques.questionnaireResult = ObjClsChildProblems.LoadQuestionareResult(CHILDID);
            if (ID != null && ID != "" && ID != "0")
            {
                ObjClsChildProblems.UpdateReferralViewStatus(user, CHILDID);
            }

            return View(ques);
        }

        public IActionResult Login(string ID)
        {
            if (ID == "0")
            {
                HttpContext.Session.SetString("USERID", "");
                HttpContext.Session.SetString("USERTYPE", "");

                //return RedirectToAction("addchild");
                return View();

            }
            else
            {
                return View();
            }

        }

        public IActionResult Register()
        {

            return View();
        }

        public IActionResult ChildProblems(string ID, string type)
        {
            ClsChildProblems ObjClsChildProblems = new ClsChildProblems();

            List<ChildProblems> Listques = new List<ChildProblems>();
            ChildProblems ques = new ChildProblems();
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            Listques = ObjClsChildProblems.LoadNextQuestion(ID, CHILDID, type);
            //ques.questionnaire = Listques;
            return View(Listques[0]);
        }


        public IActionResult ProfileEdit()
        {
            string user = "";
            string PWD = "";
            if (HttpContext.Session.GetString("USERID") != null)
            {
                user = HttpContext.Session.GetString("USERID").ToString();
                PWD = HttpContext.Session.GetString("PASSWORD").ToString();
            }

            Users objuser = new Users();
            objuser.USERID = user;
            objuser.PASSWORD = PWD;

            return View(objuser);
        }


        public IActionResult addchild(string ID, string search)

        {

            ChildProfile profile = new ChildProfile();
            string user = "";
            if (HttpContext.Session.GetString("USERID") != null)
                user = HttpContext.Session.GetString("USERID").ToString();
            if (user != null && user != "")
            {
                if (ID != null && ID != "" && ID != "0")
                {
                    profile = ObjClsChildProfile.LoadProfile(ID, user);
                }
                else
                {
                    profile.DOB = "";
                    profile.GENDER = "1";
                    profile.AGECATEGORY = "1";

                }
                profile.childProfileList = ObjClsChildProfile.LoadProfileList(user, search);
                profile.SEARCHSTR = search;

                return View(profile);
            }
            else
            {
                return RedirectToAction("Login");
                //return View(profile);
            }
        }

        public IActionResult Doctor()
        {
            DoctorProfile d = new DoctorProfile();
            var user = HttpContext.Session.GetString("USERID");
            if (user == null)
            {
                HttpContext.Session.SetString("USERID", "");
            }
            else
            {
                ClsUser u = new ClsUser();
                d = u.LoadDoctorProfile(user);
            }

            return View(d);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult DoctorInfo()
        {
            DoctorProfile d = new DoctorProfile();
            var user = HttpContext.Session.GetString("USERID");
            if (user == null)
            {
                HttpContext.Session.SetString("USERID", "");
            }
            else
            {
                ClsUser u = new ClsUser();
                d = u.LoadDoctorProfile(user);
            }

            return View(d);
        }

        public IActionResult Practice1()
        {
            return View();
        }

        public IActionResult Noline()
        {
            return View();
        }

        public IActionResult Practice3()
        {
            return View();
        }
        public IActionResult traininglevels()
        {
            return View();
        }
        
        public IActionResult Messages(string ID)
        {
            Messages msg = new Messages();
            var user = HttpContext.Session.GetString("USERID");
            var type = HttpContext.Session.GetString("USERTYPE");
            ClsChildProblems A = new ClsChildProblems();

            msg.USERIDS = A.LoadChatNameList(ID, user, type);
            msg.FROMUSERID = user;
            msg.USERID = user;
            msg.CHILDID = ID;

            return View(msg);
        }
    }
}
