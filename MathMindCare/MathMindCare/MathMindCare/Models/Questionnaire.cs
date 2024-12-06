namespace MathMindCare.Models
{
    public class Questionnaire
    {
        public string CHILDID { get; set; }
        public string QID { get; set; }
        public string QUESTION { get; set; }
        public string ANS1 { get; set; }
        public string ANS2 { get; set; }
        public string ANS3 { get; set; }
        public string ANS4 { get; set; }
        public string POINTS1 { get; set; }
        public string POINTS2 { get; set; }
        public string POINTS3 { get; set; }
        public string POINTS4 { get; set; }
        public string ACTIVE { get; set; }
        public string ANS { get; set; }
        public List<Questionnaire> questionnaire { get; set; }
    }

    
}
