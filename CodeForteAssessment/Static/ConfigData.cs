using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForteAssessment.Static
{
    public class ConfigData
    {
        public static string BonusTestSite = "https://opensource-demo.orangehrmlive.com";
        public static string Screenshotdirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Screenshots/";
        public static string ReportDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Reports/";
    }
}
