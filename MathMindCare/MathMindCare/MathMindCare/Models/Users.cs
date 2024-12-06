

namespace MathMindCare.Models
{
    public class Users
    {
        public string USERID { get; set; }
        public string PASSWORD { get; set; }
        public string LOGINTYPE { get; set; }
        public string EMAILID { get; set; }
        public string ACTIVE { get; set; }
        public string ACTIVEDESC { get; set; }
        public string NAME { get; set; }
        public List<Users> userList { get; set; }

    }

    public class DoctorProfile
    {
        public string USERID { get; set; }
        public string SPECIALIZATION { get; set; }
        public string EMAILID { get; set; }
        public string CLINICADDRESS { get; set; }
        public string ADDRESS { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; }
        public string PHOTO { get; set; }
        public string EXPERIENCE { get; set; }
        public string CHILDID { get; set; }
        public List<DoctorProfile> doctorProfile { get; set; }
    }
}
