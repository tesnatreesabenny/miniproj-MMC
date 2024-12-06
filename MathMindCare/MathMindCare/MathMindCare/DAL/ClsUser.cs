using System;
using System.Data;
using System.Data.SqlClient;
using MathMindCare.DAL;
using MathMindCare.Models;

namespace MathMindCare.DAL
{


    public class ClsUser
    {
        DataFactory _dataFactory = new DataFactory();

        public Users validateUser(string UserID, string PassWord, string LoginType)
        {
            string result = "";
            Users usr = new Users();

            try
            {
                // Establishing connection to the database using the DataFactory instance
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
                        cmd.CommandText = "spValidateLogin";              //stored procedure name
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("USERID", UserID);
                        cmd.Parameters.AddWithValue("PASSWORD", PassWord);
                        cmd.Parameters.AddWithValue("LOGINTYPE", LoginType);


                        // Executing the stored procedure and reading the results using SqlDataReader
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    if (sdr["STATUS"].ToString() == "1" && PassWord == DecodeFrom64((sdr["PASSWORD"].ToString())))
                                    {
                                        usr.USERID = UserID;
                                        usr.PASSWORD = PassWord;
                                        usr.LOGINTYPE = sdr["LOGINTYPE"].ToString();
                                        usr.NAME = sdr["NAME"].ToString();
                                    }
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



            return usr;

        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public string RegisterUser(string UserID, string PassWord, string LoginType, string EmailID, string Name, string Phone)
        {
            string result = "";
            result = isUserExists(UserID);
            if (result != "1")
            {
                try
                {

                    using (SqlConnection con = _dataFactory.GetDBConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd.Connection = con;
                            cmd.CommandText = "spInsertLogin";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("USERID", UserID);
                            cmd.Parameters.AddWithValue("PASSWORD", EncodePasswordToBase64(PassWord));
                            cmd.Parameters.AddWithValue("LOGINTYPE", LoginType);
                            cmd.Parameters.AddWithValue("EMAILID", EmailID);
                            cmd.Parameters.AddWithValue("NAME", Name);
                            cmd.Parameters.AddWithValue("PHONE", Phone);
                            cmd.Parameters.AddWithValue("ACTIVE", "1");

                            cmd.ExecuteNonQuery();
                            result = "Account created successfully";
                        }
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
            }


            return result;

        }

        public string Changepwd(string UserID, string PassWord)
        {
            string result = "";


            try
            {

                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "spChangePassword";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("USERID", UserID);
                        cmd.Parameters.AddWithValue("PASSWORD", EncodePasswordToBase64(PassWord));

                        cmd.ExecuteNonQuery();
                        result = "Password changed!";
                    }
                }


            }
            catch (Exception e)
            {

                throw;
            }



            return result;

        }
        //public string Doctorprofile(string UserID, string Specialization, string Clinicadd, string EmailID, string Name, string Phone, string Address, string Photo, string Experience)
        public string Doctorprofile(DoctorProfile dr)
        {
            string result = "";


            try
            {

                using (SqlConnection con = _dataFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "spDoctorProfile";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("USERID", dr.USERID);
                        cmd.Parameters.AddWithValue("SPECIALIZATION", dr.SPECIALIZATION);
                        cmd.Parameters.AddWithValue("EMAILID", dr.EMAILID);
                        cmd.Parameters.AddWithValue("CLINICADDRESS", dr.CLINICADDRESS);
                        cmd.Parameters.AddWithValue("ADDRESS", dr.ADDRESS);
                        cmd.Parameters.AddWithValue("NAME", dr.NAME);
                        cmd.Parameters.AddWithValue("PHONE", dr.PHONE);
                        if (dr.PHOTO != null)
                            cmd.Parameters.AddWithValue("PHOTO", dr.PHOTO);
                        else
                            cmd.Parameters.AddWithValue("PHOTO", "");
                        cmd.Parameters.AddWithValue("EXPERIENCE", dr.EXPERIENCE);

                        cmd.ExecuteNonQuery();
                        result = "Profile Updated";
                    }
                }


            }
            catch (Exception e)
            {

                throw;
            }



            return result;

        }


        public DoctorProfile LoadDoctorProfile(string USERID)
        {
            //string result = "";
            DoctorProfile quest = new DoctorProfile();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "select USERID,SPECIALIZATION,EMAILID,CLINICADDRESS,ADDRESS,NAME,PHONE,PHOTO,EXPERIENCE FROM DOCTORPROFILE WHERE USERID='" + USERID + "'";
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

                                    DoctorProfile a = new DoctorProfile();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.SPECIALIZATION = sdr["SPECIALIZATION"].ToString();
                                    a.EMAILID = sdr["EMAILID"].ToString();
                                    a.CLINICADDRESS = sdr["CLINICADDRESS"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.PHONE = sdr["PHONE"].ToString();
                                    a.PHOTO = sdr["PHOTO"].ToString();
                                    a.EXPERIENCE = sdr["EXPERIENCE"].ToString();
                                    quest = a;
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


        public List<DoctorProfile> LoadDoctorProfileList(string USERID,string CHILDID)
        {
            //string result = "";
            List<DoctorProfile> quest = new List<DoctorProfile>();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "select USERID,SPECIALIZATION,EMAILID,CLINICADDRESS,ADDRESS,NAME,PHONE,PHOTO,EXPERIENCE FROM DOCTORPROFILE";
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

                                    DoctorProfile a = new DoctorProfile();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.SPECIALIZATION = sdr["SPECIALIZATION"].ToString();
                                    a.EMAILID = sdr["EMAILID"].ToString();
                                    a.CLINICADDRESS = sdr["CLINICADDRESS"].ToString();
                                    a.ADDRESS = sdr["ADDRESS"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.PHONE = sdr["PHONE"].ToString();
                                    a.PHOTO = sdr["PHOTO"].ToString();
                                    a.EXPERIENCE = sdr["EXPERIENCE"].ToString();
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

        public List<Users> LoadUsersList()
        {
            //string result = "";
            List<Users> quest = new List<Users>();

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "select USERID,NAME,EMAILID,ACTIVE,LOGINTYPE FROM LOGIN";
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

                                    Users a = new Users();

                                    a.USERID = sdr["USERID"].ToString();
                                    a.NAME = sdr["NAME"].ToString();
                                    a.EMAILID = sdr["EMAILID"].ToString();
                                    a.ACTIVE = sdr["ACTIVE"].ToString();
                                    if(a.ACTIVE=="True")
                                        a.ACTIVEDESC = "ACTIVE";
                                    else
                                        a.ACTIVEDESC = "DISABLED";
                                    a.LOGINTYPE = sdr["LOGINTYPE"].ToString();
                                   
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

        public string DisableUser(string USERID,string STATUS)
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
                    if(STATUS=="True")
                        cmd.CommandText = "UPDATE LOGIN SET ACTIVE=0 WHERE USERID='" + USERID + "'";
                    else
                        cmd.CommandText = "UPDATE LOGIN SET ACTIVE=1 WHERE USERID='" + USERID + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if(STATUS=="True")
                    res = "User disabled";
                    else
                        res = "User enabled";
                }
            }
            return res;
        }

        public string isUserExists(string USERID)
        {
            string result = "";
            

            try
            {


                using (SqlConnection con = _dataFactory.GetDBConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "select USERID FROM LOGIN WHERE USERID='" + USERID + "'";
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                result = "1";
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {

                throw;
            }



            return result;



        }

    }



}
