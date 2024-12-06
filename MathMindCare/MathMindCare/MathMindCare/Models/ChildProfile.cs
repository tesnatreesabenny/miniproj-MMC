namespace MathMindCare.Models
{
    public class ChildProfile
    {
        public string USERID { get; set; }
        public string CHILDID { get; set; }
        public string NAME { get; set; }
        public string DOB { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }        
        public string PHNO { get; set; }
        public string EMAIL { get; set; }
        public string SEARCHSTR { get; set; }
        public string VIEWED { get; set; }
        public string REFUSERID { get; set; }
        public string AGECATEGORY { get; set; }
        public List<ChildProfile> childProfileList { get; set; }
    }
}
