using System.Data;
using System.Data.SqlClient;
using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MathMindCare.DAL
{
    public class ClsChildProfile
    {
        DataFactory _dataFactory = new DataFactory();
        public string AddChild(ChildProfile profile)
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

                    cmd.CommandText = "spInsertChildprofiles";              //stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID",profile.USERID);
                    cmd.Parameters.AddWithValue("@CHILDID", profile.CHILDID);
                    cmd.Parameters.AddWithValue("@NAME1",profile.NAME);
                    cmd.Parameters.AddWithValue("@DOB", profile.DOB);
                    cmd.Parameters.AddWithValue("@GENDER", profile.GENDER);
                    cmd.Parameters.AddWithValue("@PHNO", profile.PHNO);
                    cmd.Parameters.AddWithValue("@EMAIL", profile.EMAIL);
                    cmd.Parameters.AddWithValue("@ADDRESS", profile.ADDRESS);
                    cmd.Parameters.AddWithValue("@AGECATEGORY", profile.AGECATEGORY);
                    
                    cmd.ExecuteNonQuery();
                    res = "Details Saved";

                }
            }



            return res;



        }


        public List<ChildProfile> LoadProfileList(string UserID,string text)
        {
            //string result = "";
            List<ChildProfile> profile = new List<ChildProfile>();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select CHILDID, USERID,NAME,DOB,GENDER,ADDRESS,PHNO,EMAIL from CHILDPROFILES where userid='" + UserID + "' and name like '%"+text+"%'";
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
                                    string dob = "";
                                    if (sdr["DOB"].ToString() != "")
                                    {
                                        dob = Convert.ToDateTime(sdr["DOB"]).ToString("dd-MM-yyyy");
                                    }

                                    ChildProfile a = new ChildProfile();
                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.CHILDID = sdr["CHILDID"].ToString();
                                    a.DOB = dob;
                                    a.GENDER = sdr["GENDER"].ToString();
                                    a.PHNO = sdr["PHNO"].ToString();
                                    a.EMAIL = sdr["EMAIL"].ToString();


                                    profile.Add(a);
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



            return profile;

        }

        public List<ChildProfile> LoadAssignedProfileList(string UserID)
        {
            //string result = "";
            List<ChildProfile> profile = new List<ChildProfile>();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select a.CHILDID, a.USERID,a.NAME,a.DOB,a.GENDER,a.ADDRESS,a.PHNO,a.EMAIL,B.VIEWED from CHILDPROFILES a inner join REFERRALS b on a.childid=b.childid where b.userid='" + UserID + "' ORDER BY CREATEDON DESC";
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
                                    string dob = "";
                                    if (sdr["DOB"].ToString() != "")
                                    {
                                        dob = Convert.ToDateTime(sdr["DOB"]).ToString("dd-MM-yyyy");
                                    }

                                    ChildProfile a = new ChildProfile();
                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.CHILDID = sdr["CHILDID"].ToString();
                                    a.DOB = dob;
                                    a.GENDER = sdr["GENDER"].ToString();
                                    a.PHNO = sdr["PHNO"].ToString();
                                    a.EMAIL = sdr["EMAIL"].ToString();

                                    a.VIEWED = sdr["VIEWED"].ToString();
                                    profile.Add(a);
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



            return profile;

        }


        public ChildProfile LoadProfile(string childID,string UserID)
        {
            //string result = "";
            ChildProfile profile = new ChildProfile();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select CHILDID, USERID,NAME,DOB,GENDER,ADDRESS,PHNO,EMAIL,AGECATEGORY from CHILDPROFILES where USERID='" + UserID+"' AND childID =" + childID;
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
                                    string dob = "";
                                    if (sdr["DOB"].ToString() != "")
                                    {
                                        dob = Convert.ToDateTime(sdr["DOB"]).ToString("yyyy-MM-dd");
                                    }
                                    ChildProfile a = new ChildProfile();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.CHILDID = sdr["CHILDID"].ToString();
                                    a.DOB = dob;
                                    a.GENDER = sdr["GENDER"].ToString();
                                    a.PHNO = sdr["PHNO"].ToString();
                                    a.EMAIL = sdr["EMAIL"].ToString();
                                    a.AGECATEGORY = sdr["AGECATEGORY"].ToString();

                                    profile =a;
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



            return profile;

        }

        public ChildProfile GETChildProfile(string childID,string USERID)
        {
            //string result = "";
            ChildProfile profile = new ChildProfile();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select A.CHILDID, A.USERID,A.NAME,A.DOB,A.GENDER,A.ADDRESS,A.PHNO,A.EMAIL,B.REFUSERID from CHILDPROFILES A LEFT JOIN (select USERID,REFUSERID,CHILDID from REFERRALS where USERID='"+ USERID +"' and CHILDID="+ childID + ") B ON A.CHILDID=B.CHILDID  where A.childID =" + childID+"";
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
                                    string dob = "";
                                    if (sdr["DOB"].ToString() != "")
                                    {
                                        dob = Convert.ToDateTime(sdr["DOB"]).ToString("yyyy-MM-dd");
                                    }
                                    ChildProfile a = new ChildProfile();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.CHILDID = sdr["CHILDID"].ToString();
                                    a.DOB = dob;
                                    a.GENDER = sdr["GENDER"].ToString();
                                    a.PHNO = sdr["PHNO"].ToString();
                                    a.EMAIL = sdr["EMAIL"].ToString();
                                    a.REFUSERID = sdr["REFUSERID"].ToString();

                                    profile = a;
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



            return profile;

        }


    }
}

