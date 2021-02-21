using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFastfood.Models;
using WebFastfood.MyHelpers;

namespace WebFastfood
{
    public partial class AddProductP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }
            Page.Header.Title = "Fast food - Add";
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (PictureUpload.PostedFile != null && PictureUpload.PostedFile.FileName != "")
            {
                string folder = "~/pictures/products/";
                
                string pName = MyHelper.SavePicture(PictureUpload, this, folder);

                PRODUCT p = new PRODUCT();
                p.title = TitleTextBox.Text.Trim();
                p.description = DescriptionTextArea.Text.Trim();
                p.category = CategoryDropDownList.Text.Trim();
                p.price = (decimal)Convert.ToDouble(PriceTextBox.Text.Trim());
                p.picture = pName;
                p.available = true;
                p.id_admin = ((TADMIN)HttpContext.Current.Session["admin"]).id_admin;
                
                db.PRODUCTs.Add(p);
                db.SaveChanges();

                TitleTextBox.Text = "";
                DescriptionTextArea.Text = "";
                CategoryDropDownList.Text = "";
                PriceTextBox.Text = "";
                product_img.ImageUrl = "";
            }
        }
    }
}