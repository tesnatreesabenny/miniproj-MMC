
using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Data;
using System.Diagnostics;
using System.Text;


namespace MathMindCare.Controllers
{
    public class ChildProblemController : Controller
    {
        ClsChildProblems ObjClsChildProblems = new ClsChildProblems();
        public IActionResult Index()
        {
            return View();
        }

        public string AddCPTestResult(ChildProblems quest)
        {
            string result = "";
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            quest.CHILDID = CHILDID;

            result = ObjClsChildProblems.AddCPTestResult(quest);



            return result;
        }

        public string AddReferrals(string USERID, string CHILDID)
        {
            string REFUSERID= HttpContext.Session.GetString("USERID").ToString();
            string result = ObjClsChildProblems.AddReferrals(USERID, CHILDID, REFUSERID);
            return result;
        }

        public string AddMessages(string USERID, string CHILDID,string MESSAGE)
        {
            string REFUSERID = HttpContext.Session.GetString("USERID").ToString();
            string result = ObjClsChildProblems.AddMessages(USERID, CHILDID,MESSAGE, REFUSERID);
            return result;
        }

        public string LoadChildProblemChartdata(string CHILDID)
        {
            DataTable DT = ObjClsChildProblems.LoadChildProblemChartdata(CHILDID);
            string res = DataTableToStringBuilder(DT);
            return res;
        }

        public string LoadMessagesData(string CHILDID,string USERID)
        {
            var user = HttpContext.Session.GetString("USERID");
            var type = HttpContext.Session.GetString("USERTYPE");

            List<Messages> DT = ObjClsChildProblems.LoadMessagesData(CHILDID,USERID,user, type);
            string RES = "";
            for (int i = 0; i <DT.Count; i++)
            {
                if (DT[i].FROMUSERID == user)
                {
                    RES = RES + "<div  style='margin-left:30Px' class='message user'><div class='message-content'>" + DT[i].MESSAGE+"</div></div>";
                }
                else
                {
                    RES = RES + "<div  class='message bot'><div class='message-content'>" + DT[i].MESSAGE + "</div></div>";
                }
            }

                return RES;
        }

        public string DataTableToStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        JSONString.Append(table.Rows[i][j].ToString()+",");
                    }
                    JSONString.Append("~");

                }
            }
            return JSONString.ToString();
        }


      
    }
}
