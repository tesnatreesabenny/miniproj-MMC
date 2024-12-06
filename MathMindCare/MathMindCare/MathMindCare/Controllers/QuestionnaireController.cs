
using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace MathMindCare.Controllers
{
    public class QuestionnaireController : Controller
    {
   
        //ClsUser u = new ClsUser();
        ClsQuestionnaire ObjClsQuestionnaire = new ClsQuestionnaire();
        

        public string AddQuestionnaire(Questionnaire quest)
        {
            string result = "";
   
            
            
                result = ObjClsQuestionnaire.AddQuest(quest);

            

            return result;
        }

        public string DeleteQuestionnaire(string questionid)
        {
            string result = "";



            result = ObjClsQuestionnaire.DelQuest(questionid);



            return result;
        }


        public string AddQuestResult(QuestionnaireResult quest)
        {
            string result = "";
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            quest.CHILDID = CHILDID;

            result = ObjClsQuestionnaire.AddQuestResult(quest);



            return result;
        }


    }
}
