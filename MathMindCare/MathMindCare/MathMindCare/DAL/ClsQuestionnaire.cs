
using System.Data;
using System.Data.SqlClient;
using MathMindCare.DAL;
using MathMindCare.Models;


namespace MathMindCare.DAL
{
    public class ClsQuestionnaire
    {
        DataFactory _dataFactory = new DataFactory();
        public string AddQuest(Questionnaire quest)
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

                    cmd.CommandText = "spManagequestionnaire";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QID", quest.QID);
                    cmd.Parameters.AddWithValue("@QUESTION", quest.QUESTION);
                    cmd.Parameters.AddWithValue("@ANS1", quest.ANS1);
                    cmd.Parameters.AddWithValue("@ANS2", quest.ANS2);
                    cmd.Parameters.AddWithValue("@ANS3", quest.ANS3);
                    cmd.Parameters.AddWithValue("@ANS4", quest.ANS4);
                    cmd.Parameters.AddWithValue("@POINTS1", quest.POINTS1);
                    cmd.Parameters.AddWithValue("@POINTS2", quest.POINTS2);
                    cmd.Parameters.AddWithValue("@POINTS3", quest.POINTS3);
                    cmd.Parameters.AddWithValue("@POINTS4", quest.POINTS4);
                    cmd.Parameters.AddWithValue("@ACTIVE", quest.ACTIVE);

                    cmd.ExecuteNonQuery();
                    res = "Added Successfully";


                }
            }



            return res;



        }




        public List<Questionnaire> LoadQuestion(string QID,string ChildID)
        {
            //string result = "";
            List<Questionnaire> quest = new List<Questionnaire>();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select A.QID,A.QUESTION,A.ANS1,A.ANS2,A.ANS3,A.ANS4,A.POINTS1,A.POINTS2,A.POINTS3,A.POINTS4,A.ACTIVE,B.ANS from QUESTIONNAIRE A LEFT JOIN (SELECT QID,ANS,CHILDID FROM QUESTRESULT WHERE CHILDID="+ChildID+") B ON A.QID=B.QID where A.active=1";
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
                                    
                                    Questionnaire a = new Questionnaire();

                                    a.QID = sdr["QID"].ToString();
                                    a.QUESTION = sdr["QUESTION"].ToString();
                                    a.ANS1 = sdr["ANS1"].ToString();
                                    a.ANS2 = sdr["ANS2"].ToString();
                                    a.ANS3 = sdr["ANS3"].ToString();
                                    a.ANS4 = sdr["ANS4"].ToString();
                                    a.POINTS1 = sdr["POINTS1"].ToString();
                                    a.POINTS2 = sdr["POINTS2"].ToString();
                                    a.POINTS3 = sdr["POINTS3"].ToString();
                                    a.POINTS4 = sdr["POINTS4"].ToString();
                                    a.ACTIVE = sdr["ACTIVE"].ToString();
                                    a.ANS = sdr["ANS"].ToString();
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


        public string AddQuestResult(QuestionnaireResult result)
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

                    cmd.CommandText = "spInsertQuestionnaireResult";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CHILDID", result.CHILDID);
                    cmd.Parameters.AddWithValue("@QID", result.QID);
                    cmd.Parameters.AddWithValue("@ANS", result.ANS);
                    cmd.Parameters.AddWithValue("@POINTS", result.POINTS);
                    cmd.ExecuteNonQuery();
                    res = "Updated";

                }
            }



            return res;



        }

        public Questionnaire GetQuestion(string QID)
        {
            //string result = "";
            Questionnaire quest = new Questionnaire();

            try
            {

                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select QID,QUESTION,ANS1,ANS2,ANS3,ANS4,POINTS1,POINTS2,POINTS3,POINTS4,ACTIVE from QUESTIONNAIRE where QID="+ QID;
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



                                    quest.QID = sdr["QID"].ToString();
                                    quest.QUESTION = sdr["QUESTION"].ToString();
                                    quest.ANS1 = sdr["ANS1"].ToString();
                                    quest.ANS2 = sdr["ANS2"].ToString();
                                    quest.ANS3 = sdr["ANS3"].ToString();
                                    quest.ANS4 = sdr["ANS4"].ToString();
                                    quest.POINTS1 = sdr["POINTS1"].ToString();
                                    quest.POINTS2 = sdr["POINTS2"].ToString();
                                    quest.POINTS3 = sdr["POINTS3"].ToString();
                                    quest.POINTS4 = sdr["POINTS4"].ToString();
                                    quest.ACTIVE = sdr["ACTIVE"].ToString();

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
        public string DelQuest(string questionid)
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

                    cmd.CommandText = "spDeleteQuestion";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QID", questionid);
                    

                    cmd.ExecuteNonQuery();
                    res = "Deleted Successfully";


                }
            }



            return res;



        }
    }
}
