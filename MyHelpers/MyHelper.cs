using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebFastfood.MyHelpers
{
    public class MyHelper
    {

        public static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string GenerateProductImageName()
        {
            DateTime localDate = DateTime.Now;
            return "Product - " + localDate.ToString("yyyy-dd-MM HH-mm-ss") + " - " + GenerateRandomNumber(111111, 999999);
        }

        public static string GenerateClientImageName()
        {
            DateTime localDate = DateTime.Now;
            return "Client - " + localDate.ToString("yyyy-dd-MM HH-mm-ss") + " - " + GenerateRandomNumber(111111, 999999);
        }

        public static string GenerateDeliveryManImageName()
        {
            DateTime localDate = DateTime.Now;
            return "Delivery man - " + localDate.ToString("yyyy-dd-MM HH-mm-ss") + " - " + GenerateRandomNumber(111111, 999999);
        }

        public static string GenerateAdminImageName()
        {
            DateTime localDate = DateTime.Now;
            return "Admin - " + localDate.ToString("yyyy-dd-MM HH-mm-ss") + " - " + GenerateRandomNumber(111111, 999999);
        }


        public static string SavePicture(System.Web.UI.WebControls.FileUpload PictureUpload, System.Web.UI.Page page, string folder)
        {
            string uploedeFileName = PictureUpload.FileName;
            string folderPath = page.Server.MapPath(folder);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string pName = MyHelper.GenerateProductImageName();
            string pExt = System.IO.Path.GetExtension(uploedeFileName);
            string pPath = folderPath + pName + pExt;

            PictureUpload.SaveAs(pPath); string s = folder + pName + pExt;

            return pName + pExt;
        }

        public static string ToDateTimeFormat(string date)
        {
            String[] p = date.Split('-');
            if (p.Length==3 && p[0].Length==4  && p[1].Length==2 && p[2].Length==2)
            {
                return string.Format("{0}/{1}/{2}", p[1], p[2], p[0]);
            }

            return "";
        }
    }
}