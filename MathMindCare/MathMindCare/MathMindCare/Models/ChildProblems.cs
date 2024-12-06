namespace MathMindCare.Models
{
    public class ResultModel
    {
        public string CHILDID { get; set; }
        public string NAME { get; set; }
        public string DOB { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string PHNO { get; set; }
        public string EMAIL { get; set; }
        public string REFUSERID { get; set; }
        public string EARNEPOINTS { get; set; }
        public string TOTALSCORE { get; set; }
        public List<ChildProblems> childProblems { get; set; }
        public List<WhiteBoard> whiteBoard { get; set; }
        public List<DoctorsList> doctorsList { get; set; }
        public List<QuestionnaireResult> questionnaireResult { get; set; }
    }

    public class ChildProblems
    {
        public int COUNT { get; set; }
        public string CHILDID { get; set; }
        public string QID { get; set; }
        public string QUESTION { get; set; }
        public string ANS1 { get; set; }
        public string ANS2 { get; set; }
        public string ANS3 { get; set; }
        public string ANS4 { get; set; }
       
        public string MULTCORRECTANS { get; set; }

        public string TEXTCORRECTANS { get; set; }
        public string IMAGEFIELD { get; set; }
        public string ANS { get; set; }
        public string CORRECTANS { get; set; }
        public double MAXCOUNT { get; set; }
        public double CURCOUNT { get; set; }
        public string PERCENT { get; set; }
        public string WEIGHTAGE { get; set; }
        public string TOTALSCORE { get; set; }
        public string SCORED { get; set; }
        public string ANSTEXT { get; set;    }
        public string CORRECTANSTEXT { get; set; }

    }

    public class WhiteBoard
    {
        public string CHILDID { get; set; }
        public string WDQID { get; set; }
        public string QUESTION { get; set; }
        public string LETTER1 { get; set; }
        public string LETTER2 { get; set; }
        public string LETTER3 { get; set; }
        public string LETTER4 { get; set; }
        public string LETTER5 { get; set; }
        public string LETTER6 { get; set; }
        public string SCORE { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGELOCATION { get; set; }
        public int COUNT { get; set; }

    }


    public class QuestionnaireResult
    {
        public string CHILDID { get; set; }
        public string QID { get; set; }
        public string QUESTION { get; set; }
        public string ANS { get; set; }
        public int SLNO { get; set; }
        public string POINTS { get; set; }
 

    }

    public class DoctorsList
    {
        public string USERID { get; set; }
        public string NAME { get; set; }
    }

    public class Messages
    {
        public string USERID { get; set; }
        public string NAME { get; set; }
        public string USERNAME { get; set; }
        public string CHILDID { get; set; }
        public string FROMUSERID { get; set; }
        public string MESSAGE { get; set; }
        public string CDATE { get; set; }
        public string SEARCHSTR { get; set; }

        public List<Messages> USERIDS{ get; set; }
        public List<Messages> MESSAGELIST { get; set; }
    }

}
