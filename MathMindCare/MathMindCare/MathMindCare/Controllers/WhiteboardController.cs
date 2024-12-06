using MathMindCare.DAL;
using MathMindCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.IO;

namespace MathMindCare.Controllers
{

    public class WhiteboardController : Controller
    {
        ClsChildProblems ObjClsChildProblems = new ClsChildProblems();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveImage(string n1, string n2, string n3, string n4, string n5, string n6, string imageData, string WBQID)
        {

            var path = Path.GetFullPath("wwwroot");


            string fileNameWitPath = path + "\\img\\TestImages\\" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "") + ".png";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }
            //var path = Path.GetFullPath("TestImages");


            //string fileNameWitPath = path + "\\original.png";
            string fileNameTemplatePath1 = path + "\\img\\Numbers\\" + n1 + ".png";
            string fileNameTemplatePath2 = path + "\\img\\Numbers\\" + n2 + ".png";
            string fileNameTemplatePath3 = path + "\\img\\Numbers\\" + n3 + ".png";
            string fileNameTemplatePath4 = path + "\\img\\Numbers\\" + n4 + ".png";
            string fileNameTemplatePath5 = path + "\\img\\Numbers\\" + n5 + ".png";
            string fileNameTemplatePath6 = path + "\\img\\Numbers\\" + n6 + ".png";

            Mat mainImage = CvInvoke.Imread(fileNameWitPath, ImreadModes.Color);

            Mat templateImage1 = CvInvoke.Imread(fileNameTemplatePath1, ImreadModes.Color);
            Mat result1 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage1, result1, TemplateMatchingType.CcoeffNormed);
            double[] minValues1, maxValues1;
            Point[] minLocations1, maxLocations1;
            result1.MinMax(out minValues1, out maxValues1, out minLocations1, out maxLocations1);

            Mat templateImage2 = CvInvoke.Imread(fileNameTemplatePath2, ImreadModes.Color);
            Mat result2 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage2, result2, TemplateMatchingType.CcoeffNormed);
            double[] minValues2, maxValues2;
            Point[] minLocations2, maxLocations2;
            result2.MinMax(out minValues2, out maxValues2, out minLocations2, out maxLocations2);


            Mat templateImage3 = CvInvoke.Imread(fileNameTemplatePath3, ImreadModes.Color);
            Mat result3 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage3, result3, TemplateMatchingType.CcoeffNormed);
            double[] minValues3, maxValues3;
            Point[] minLocations3, maxLocations3;
            result3.MinMax(out minValues3, out maxValues3, out minLocations3, out maxLocations3);

            Mat templateImage4 = CvInvoke.Imread(fileNameTemplatePath4, ImreadModes.Color);
            Mat result4 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage4, result4, TemplateMatchingType.CcoeffNormed);
            double[] minValues4, maxValues4;
            Point[] minLocations4, maxLocations4;
            result4.MinMax(out minValues4, out maxValues4, out minLocations4, out maxLocations4);

            Mat templateImage5 = CvInvoke.Imread(fileNameTemplatePath5, ImreadModes.Color);
            Mat result5 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage5, result5, TemplateMatchingType.CcoeffNormed);
            double[] minValues5, maxValues5;
            Point[] minLocations5, maxLocations5;
            result5.MinMax(out minValues5, out maxValues5, out minLocations5, out maxLocations5);

            Mat templateImage6 = CvInvoke.Imread(fileNameTemplatePath6, ImreadModes.Color);
            Mat result6 = new Mat();
            CvInvoke.MatchTemplate(mainImage, templateImage6, result6, TemplateMatchingType.CcoeffNormed);
            double[] minValues6, maxValues6;
            Point[] minLocations6, maxLocations6;
            result6.MinMax(out minValues6, out maxValues6, out minLocations6, out maxLocations6);

            string testRemarks = "";
            string Score = "1";
            if (maxLocations4[0].X <= maxLocations2[0].X - templateImage4.Size.Width)
            {
                if (maxValues4[0] > .75)
                {
                    for (int i = 0; i < maxLocations4.Length; i++)
                    {
                        Rectangle rect = new Rectangle(maxLocations4[i], templateImage4.Size);
                        CvInvoke.Rectangle(mainImage, rect, new MCvScalar(0, 0, 255), 2);
                        testRemarks = testRemarks + "Dislocation of number " + n4 + " found.";
                        Score = "0";
                    }
                }
            }
            if (Score!="0" && maxLocations4[0].X <= maxLocations3[0].X - templateImage4.Size.Width)
            {
                if (maxValues4[0] > .75)
                {
                    for (int i = 0; i < maxLocations4.Length; i++)
                    {
                        Rectangle rect = new Rectangle(maxLocations4[i], templateImage4.Size);
                        CvInvoke.Rectangle(mainImage, rect, new MCvScalar(0, 0, 255), 2);
                        testRemarks = testRemarks + "Dislocation of number " + n4 + " found.";
                        Score = "0";
                    }
                }
            }

            if (maxLocations2[0].X <= maxLocations1[0].X - templateImage2.Size.Width)
            {
                if (maxValues2[0] > .75)
                {
                    for (int i = 0; i < maxLocations2.Length; i++)
                    {
                        Rectangle rect = new Rectangle(maxLocations2[i], templateImage2.Size);
                        CvInvoke.Rectangle(mainImage, rect, new MCvScalar(0, 0, 255), 2);
                        testRemarks = testRemarks + "Dislocation of number " + n2 + " found.";
                        Score = "0";
                    }
                }
            }
            if (maxValues5[0] < .75)
            {
                testRemarks = testRemarks + "Answer not correct.";
                Score = "0";
            }
            else if (maxValues6[0] < .75)
            {
                testRemarks = testRemarks + "Answer not correct.";
                Score = "0";
            }
            else if (maxLocations6[0].X <= maxLocations5[0].X - templateImage6.Size.Width)
            {
                if (maxValues6[0] > .75)
                {
                    for (int i = 0; i < maxLocations6.Length; i++)
                    {
                        Rectangle rect = new Rectangle(maxLocations6[i], templateImage6.Size);
                        CvInvoke.Rectangle(mainImage, rect, new MCvScalar(0, 0, 255), 2);
                        testRemarks = testRemarks + "Dislocation of number " + n6 + " found.";
                        Score = "0";
                    }
                }
            }
            //for (int i = 0; i < maxLocations4.Length; i++)
            //{
            //    Rectangle rect = new Rectangle(maxLocations4[i], templateImage4.Size);
            //    CvInvoke.Rectangle(mainImage, rect, new MCvScalar(0, 0, 255), 2);
            //}
            //for (int i = 0; i < maxLocations2.Length; i++)
            //{
            //    Rectangle rect = new Rectangle(maxLocations2[i], templateImage2.Size);
            //    CvInvoke.Rectangle(mainImage, rect, new MCvScalar(255, 0, 255), 2);
            //}
            CvInvoke.Imwrite(fileNameWitPath, mainImage);
            // CvInvoke.Imshow("Result", mainImage);
            // CvInvoke.WaitKey(0);


            string filename = System.IO.Path.GetFileName(fileNameWitPath);
            WhiteBoard quest = new WhiteBoard();
            string result = "";
            string CHILDID = HttpContext.Session.GetString("CHILDID").ToString();
            quest.CHILDID = CHILDID;
            quest.WDQID = WBQID;
            quest.IMAGELOCATION = filename;
            quest.DESCRIPTION = testRemarks;
            quest.SCORE = Score;
            result = ObjClsChildProblems.AddWBTestResult(quest);



            return Json(new { Status = filename, Record = "", Message = fileNameWitPath });
        }

    }
}
