
using System.Data;
using System.Data.SqlClient;
using MathMindCare.DAL;
using MathMindCare.Models;

namespace MathMindCare.DAL
{
    public class ClsChildProblems
    {
        DataFactory _dataFactory = new DataFactory();


        public string AddCPTestResult(ChildProblems profile)
        {
            string res = "";
            using (SqlConnection con = _dataFactory.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Opening the database connection if it's closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Connection = con;

                    cmd.CommandText = "spInsertCPTestResult";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CHILDID", profile.CHILDID);
                    cmd.Parameters.AddWithValue("@QID", profile.QID);
                    cmd.Parameters.AddWithValue("@ANS", profile.ANS);
                    cmd.Parameters.AddWithValue("@CORRECTANS", profile.CORRECTANS);
                    cmd.ExecuteNonQuery();
                    res = "Updated";

                }
            }
            return res;
        }

        public string AddWBTestResult(WhiteBoard profile)
        {
            string res = "";
            using (SqlConnection con = _dataFactory.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Opening the database connection if it's closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Connection = con;

                    cmd.CommandText = "spInsertWBTestResult";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CHILDID", profile.CHILDID);
                    cmd.Parameters.AddWithValue("@WDQID", profile.WDQID);
                    cmd.Parameters.AddWithValue("@IMAGELOCATION", profile.IMAGELOCATION);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", profile.DESCRIPTION);
                    cmd.Parameters.AddWithValue("@SCORE", profile.SCORE);
                    cmd.ExecuteNonQuery();
                    res = "Updated";
                }
            }
            return res;
        }

        public string AddReferrals(string USERID,string CHILDID,string REFUSERID)
        {
            string res = "";
            using (SqlConnection con = _dataFactory.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Connection = con;

                    cmd.CommandText = "spInsertReferrals";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CHILDID", CHILDID);
                    cmd.Parameters.AddWithValue("@USERID", USERID);
                    cmd.Parameters.AddWithValue("@REFUSERID", REFUSERID);
                    cmd.ExecuteNonQuery();
                    res = "Report is sent";
                }
            }
            return res;
        }

        public string AddMessages(string USERID, string CHILDID, string MESSAGE,string FROMUSERID)
        {
            string res = "";
            using (SqlConnection con = _dataFactory.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Connection = con;

                    cmd.CommandText = "spInsertMessages";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CHILDID", CHILDID);
                    cmd.Parameters.AddWithValue("@USERID", USERID);
                    cmd.Parameters.AddWithValue("@MESSAGE", MESSAGE);
                    cmd.Parameters.AddWithValue("@FROMUSERID", FROMUSERID);
                    cmd.ExecuteNonQuery();
                    res = "Message is sent";
                }
            }
            return res;
        }


        public string UpdateReferralViewStatus(string USERID, string CHILDID)
        {
            string res = "";
            using (SqlConnection con = _dataFactory.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Connection = con;

                    cmd.CommandText = "UPDATE REFERRALS SET VIEWED=1 WHERE USERID='"+ USERID+ "' AND CHILDID="+CHILDID;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    res = "Updated";
                }
            }
            return res;
        }

        public List<ChildProblems> LoadNextQuestion(string QID, string ChildID, string type)
        {
            //string result = "";
            List<ChildProblems> quest = new List<ChildProblems>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (type != "0")
                        {
                            cmd.CommandText = "select a.QID,a.QUESTION,a.ANS1,a.ANS2,a.ANS3,a.ANS4,a.MULTCORRECTANS,a.TEXTCORRECTANS,a.IMAGEFIELD,B.ANS,B.CORRECTANS from CHILDPROBLEM a left outer join (select QID,CHILDID,ANS,CORRECTANS FROM  CPTESTRESULT where CHILDID=" + ChildID + ") B ON A.QID=B.QID WHERE a.QID>" + QID + " and a.AGECATEGORY <=(SELECT ISNULL(AGECATEGORY,1) FROM CHILDPROFILES WHERE CHILDID="+ChildID+")  order by a.QID";

                        }
                        else
                        {
                            cmd.CommandText = "select a.QID,a.QUESTION,a.ANS1,a.ANS2,a.ANS3,a.ANS4,a.MULTCORRECTANS,a.TEXTCORRECTANS,a.IMAGEFIELD,B.ANS,B.CORRECTANS from CHILDPROBLEM a left outer join (select QID,CHILDID,ANS,CORRECTANS FROM  CPTESTRESULT where CHILDID=" + ChildID + ") B ON A.QID=B.QID where a.QID<" + QID + " and a.AGECATEGORY <=(SELECT ISNULL(AGECATEGORY,1) FROM CHILDPROFILES WHERE CHILDID="+ChildID+")  order by a.QID desc";
                        }
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                int count = 0;
                                while (sdr.Read())
                                {
                                    count = count + 1;
                                    if (count == 1)
                                    {
                                        ChildProblems a = new ChildProblems();

                                        a.QID = sdr["QID"].ToString();
                                        a.QUESTION = sdr["QUESTION"].ToString();
                                        a.ANS1 = sdr["ANS1"].ToString();
                                        a.ANS2 = sdr["ANS2"].ToString();
                                        a.ANS3 = sdr["ANS3"].ToString();
                                        a.ANS4 = sdr["ANS4"].ToString();
                                        a.MULTCORRECTANS = sdr["MULTCORRECTANS"].ToString();
                                        a.TEXTCORRECTANS = sdr["TEXTCORRECTANS"].ToString();
                                        a.IMAGEFIELD = sdr["IMAGEFIELD"].ToString();
                                        a.MAXCOUNT = GetQuestionCount(a.QID, ChildID, "1");
                                        a.CURCOUNT = GetQuestionCount(a.QID, ChildID, "0");
                                        a.PERCENT = (a.CURCOUNT / a.MAXCOUNT * 100).ToString() + "%";
                                        a.ANS = sdr["ANS"].ToString();
                                        a.CORRECTANS = sdr["CORRECTANS"].ToString();
                                        quest.Add(a);
                                    }
                                }
                                if (quest.Count > 0)
                                {
                                    //if (type == "0")
                                    //    count = 0;
                                    quest[0].COUNT = count;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {

                throw;
            }



            return quest;



        }

        public double GetQuestionCount(string QID, string ChildID, string allquestions)
        {
            //string result = "";
            int res = 0;
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (allquestions != "0")
                        {
                            cmd.CommandText = "select Count(*) as cnt from CHILDPROBLEM a where  a.AGECATEGORY <=(SELECT ISNULL(AGECATEGORY,1) FROM CHILDPROFILES WHERE CHILDID=" + ChildID + ") ";
                        }
                        else
                        {
                            cmd.CommandText = "select Count(*) as cnt from CHILDPROBLEM a where a.QID<=" + QID + " and a.AGECATEGORY <=(SELECT ISNULL(AGECATEGORY,1) FROM CHILDPROFILES WHERE CHILDID=" + ChildID + ")";
                        }
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    res = Convert.ToInt32(sdr["cnt"]);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {

                throw;
            }



            return res;



        }

        public List<WhiteBoard> LoadWBNextQuestion(string WDQID, string ChildID, string type)
        {
            //string result = "";
            List<WhiteBoard> quest = new List<WhiteBoard>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (type != "0")
                        {
                            cmd.CommandText = "select WDQID,QUESTION,LETTER1,LETTER2,LETTER3,LETTER4,LETTER5,LETTER6 from WHITEBAORDQUESTIONS where WDQID>" + WDQID + " order by WDQID";
                        }
                        else
                        {
                            cmd.CommandText = "select WDQID,QUESTION,LETTER1,LETTER2,LETTER3,LETTER4,LETTER5,LETTER6 from WHITEBAORDQUESTIONS where WDQID<" + WDQID + " order by WDQID desc";
                        }
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                int count = 0;
                                while (sdr.Read())
                                {
                                    count = count + 1;
                                    if (count == 1)
                                    {
                                        WhiteBoard a = new WhiteBoard();

                                        a.WDQID = sdr["WDQID"].ToString();
                                        a.QUESTION = sdr["QUESTION"].ToString();
                                        a.LETTER1 = sdr["LETTER1"].ToString();
                                        a.LETTER2 = sdr["LETTER2"].ToString();
                                        a.LETTER3 = sdr["LETTER3"].ToString();
                                        a.LETTER4 = sdr["LETTER4"].ToString();
                                        a.LETTER5 = sdr["LETTER5"].ToString();
                                        a.LETTER6 = sdr["LETTER6"].ToString();
                                        quest.Add(a);
                                    }
                                }
                                if (quest.Count > 0)
                                {
                                    quest[0].COUNT = count;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

       


        public List<ChildProblems> LoadChileProblemResult(string ChildID)
        {
            //string result = "";
            List<ChildProblems> quest = new List<ChildProblems>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select a.QID,a.QUESTION,A.WEIGHTAGE,a.ANS1,a.ANS2,a.ANS3,a.ANS4,a.MULTCORRECTANS,a.TEXTCORRECTANS,a.IMAGEFIELD,B.ANS,B.CORRECTANS from CHILDPROBLEM a left outer join CPTESTRESULT B ON A.QID=B.QID where B.CHILDID=" + ChildID + " order by QID";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Int32 totalscore = 0;
                                Int32 SCORED = 0;
                                while (sdr.Read())
                                {
                                    ChildProblems a = new ChildProblems();

                                    a.QID = sdr["QID"].ToString();
                                    a.QUESTION = sdr["QUESTION"].ToString();
                                    a.ANS1 = sdr["ANS1"].ToString();
                                    a.ANS2 = sdr["ANS2"].ToString();
                                    a.ANS3 = sdr["ANS3"].ToString();
                                    a.ANS4 = sdr["ANS4"].ToString();
                                    a.MULTCORRECTANS = sdr["MULTCORRECTANS"].ToString();
                                    a.TEXTCORRECTANS = sdr["TEXTCORRECTANS"].ToString();
                                    a.IMAGEFIELD = sdr["IMAGEFIELD"].ToString();
                                    a.ANS = sdr["ANS"].ToString();
                                    a.CORRECTANS = sdr["CORRECTANS"].ToString();
                                    a.WEIGHTAGE= sdr["WEIGHTAGE"].ToString();
                                    if (a.TEXTCORRECTANS != null && a.TEXTCORRECTANS != "")
                                    {
                                        a.ANSTEXT = a.ANS.ToString();
                                        a.CORRECTANSTEXT= a.TEXTCORRECTANS.ToString();
                                    }
                                        
                                    else
                                    {
                                        if(a.ANS=="1")
                                            a.ANSTEXT = a.ANS1;
                                        else if (a.ANS == "2")
                                            a.ANSTEXT = a.ANS2;
                                        else if (a.ANS == "3")
                                            a.ANSTEXT = a.ANS3;
                                        else if (a.ANS == "4")
                                            a.ANSTEXT = a.ANS4;

                                        if (a.MULTCORRECTANS == "1")
                                            a.CORRECTANSTEXT = a.ANS1;
                                        else if (a.MULTCORRECTANS == "2")
                                            a.CORRECTANSTEXT = a.ANS2;
                                        else if (a.MULTCORRECTANS == "3")
                                            a.CORRECTANSTEXT = a.ANS3;
                                        else if (a.MULTCORRECTANS == "4")
                                            a.CORRECTANSTEXT = a.ANS4;

                                    }
                                    totalscore = totalscore + Convert.ToInt32(a.WEIGHTAGE);
                                    if (a.ANS == a.CORRECTANS)
                                    {
                                        SCORED = SCORED + Convert.ToInt32(a.WEIGHTAGE);
                                    }
                                    quest.Add(a);
                                }
                                if (totalscore > 0)
                                {
                                    quest[0].TOTALSCORE = totalscore.ToString();
                                    quest[0].SCORED = SCORED.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

        public List<WhiteBoard> LoadWhiteBpardResult(string ChildID)
        {
            //string result = "";
            List<WhiteBoard> quest = new List<WhiteBoard>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT a.WDQID,a.QUESTION,a.LETTER1,a.LETTER2,a.LETTER3,a.LETTER4,a.LETTER5,a.LETTER6,B.IMAGELOCATION,B.DESCRIPTION,B.SCORE FROM WHITEBAORDQUESTIONS a inner join WHITEBOARDANSWERS b on a.WDQID=b.WDQID WHERE B.CHILDID=" + ChildID + " order by WDQID";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    WhiteBoard a = new WhiteBoard();

                                    a.WDQID = sdr["WDQID"].ToString();
                                    a.QUESTION = sdr["QUESTION"].ToString();
                                    a.LETTER1 = sdr["LETTER1"].ToString();
                                    a.LETTER2 = sdr["LETTER2"].ToString();
                                    a.LETTER3 = sdr["LETTER3"].ToString();
                                    a.LETTER4 = sdr["LETTER4"].ToString();
                                    a.LETTER5 = sdr["LETTER5"].ToString();
                                    a.LETTER6 = sdr["LETTER6"].ToString();
                                    a.IMAGELOCATION = sdr["IMAGELOCATION"].ToString();
                                    a.DESCRIPTION = sdr["DESCRIPTION"].ToString();
                                    a.SCORE = sdr["SCORE"].ToString();
                                    quest.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

        public List<QuestionnaireResult> LoadQuestionareResult(string ChildID)
        {
            //string result = "";
            List<QuestionnaireResult> quest = new List<QuestionnaireResult>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT a.QID,a.QUESTION,CASE WHEN B.ANS=1 THEN ANS1 WHEN B.ANS=2 THEN A.ANS2 WHEN B.ANS=3 THEN A.ANS3 WHEN B.ANS=4 THEN A.ANS4 END AS ANS,CASE WHEN B.ANS=1 THEN POINTS1 WHEN B.ANS=2 THEN A.POINTS2 WHEN B.ANS=3 THEN A.POINTS3 WHEN B.ANS=4 THEN A.POINTS4 END AS POINTS  FROM QUESTIONNAIRE a inner join (select QID,CHILDID,ANS,POINTS FROM QUESTRESULT WHERE CHILDID="+ChildID+") B ON A.QID=B.QID";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                int SLNO = 1;
                                while (sdr.Read())
                                {
                                    QuestionnaireResult a = new QuestionnaireResult();

                                    a.QID = sdr["QID"].ToString();
                                    a.QUESTION = sdr["QUESTION"].ToString();
                                    a.ANS = sdr["ANS"].ToString();
                                    a.POINTS = sdr["POINTS"].ToString();
                                    a.SLNO = SLNO;
                                    quest.Add(a);
                                    SLNO = SLNO + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

        public List<DoctorsList> LoadDoctorsList(string USERID)
        {
            //string result = "";
            List<DoctorsList> quest = new List<DoctorsList>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT USERID,NAME FROM LOGIN WHERE LOGINTYPE=2 AND ACTIVE=1 AND USERID<>'"+USERID+"'";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    DoctorsList a = new DoctorsList();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    
                                    quest.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

        public DataTable LoadChildProblemChartdata(string ChildID)
        {
            //string result = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("x", typeof(int));
            dt.Columns.Add("y", typeof(int));
            dt.Columns.Add("label");
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT  B.CHILDID,A.QUESTTYPEDESC,((SUM(CASE WHEN B.ANS=B.CORRECTANS THEN 1 ELSE 0 END)*100)/(COUNT(b.QID))) AS score FROM  CHILDPROBLEM A LEFT JOIN CPTESTRESULT B ON A.QID=B.QID WHERE CHILDID="+ChildID+" GROUP BY  B.CHILDID,A.QUESTTYPEDESC order by A.QUESTTYPEDESC";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                int count = 1;
                                while (sdr.Read())
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["x"] = count;
                                    dr["y"] =Convert.ToInt32(sdr["score"]);
                                    dr["label"]= sdr["QUESTTYPEDESC"].ToString();
                                    dt.Rows.Add(dr);
                                    count = count + 1;
                                  
                                    }
                                }
                               
                            }
                        }
                    }

            }

            catch (Exception e)
            {

                throw;
            }



            return dt;



        }

        public List<Messages> LoadChatNameList(string CHILDID, string USERID,string USERTYPE)
        {
            //string result = "";
            List<Messages> quest = new List<Messages>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (USERTYPE == "1")
                        {
                            cmd.CommandText = "SELECT A.USERID,A.NAME FROM LOGIN A INNER JOIN REFERRALS B ON A.USERID=B.USERID WHERE A.LOGINTYPE=2 AND A.ACTIVE=1 AND B.CHILDID='" + CHILDID + "' AND B.REFUSERID='"+ USERID+"'";
                        }
                        else if(USERTYPE == "2")
                        {
                            cmd.CommandText = "SELECT A.USERID,A.NAME FROM LOGIN A INNER JOIN REFERRALS B ON A.USERID=B.REFUSERID WHERE A.LOGINTYPE=1 AND A.ACTIVE=1 AND B.CHILDID='" + CHILDID + "' AND B.USERID='"+ USERID+"'";
                        }
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    Messages a = new Messages();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();

                                    quest.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

        public List<Messages> LoadMessagesData(string CHILDID, string USERID,string FROMUSERID, string USERTYPE)
        {
            //string result = "";
            List<Messages> quest = new List<Messages>();
            try
            {
                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (USERTYPE == "1")
                        {
                            cmd.CommandText = "SELECT A.USERID,A.FROMUSERID,A.MESSAGE,A.DATE,A.READSTATUS FROM MESSAGES A WHERE A.CHILDID='" + CHILDID + "' AND ( (A.USERID='" + USERID + "' AND A.FROMUSERID='" + FROMUSERID + "') OR (A.USERID='" + FROMUSERID + "' AND A.FROMUSERID='" + USERID + "')) order by A.DATE";
                        }
                        else if (USERTYPE == "2")
                        {
                            //cmd.CommandText = "SELECT A.USERID,A.NAME FROM LOGIN A INNER JOIN REFERRALS B ON A.USERID=B.USERID WHERE A.LOGINTYPE=2 AND A.ACTIVE=1 AND B.CHILDID='" + CHILDID + "'";
                            cmd.CommandText = "SELECT A.USERID,A.FROMUSERID,A.MESSAGE,A.DATE,A.READSTATUS FROM MESSAGES A WHERE A.CHILDID='" + CHILDID + "' AND ( (A.USERID='" + USERID + "' AND A.FROMUSERID='" + FROMUSERID + "') OR (A.USERID='" + FROMUSERID + "' AND A.FROMUSERID='" + USERID + "')) order by A.DATE";
                        }
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    Messages a = new Messages();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.CDATE = sdr["DATE"].ToString();
                                    a.FROMUSERID= sdr["FROMUSERID"].ToString();
                                    a.MESSAGE = sdr["MESSAGE"].ToString();
                                    quest.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return quest;
        }

  

    }
}
