using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFastfood.Models;
using WebFastfood.MyHelpers;

namespace WebFastfood
{
    public partial class ProductP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();
        private PRODUCT p;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }


            int id = 0;
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !Int32.TryParse(Request.QueryString["id"], out id))
            {
                Response.Redirect("~/MenuP.aspx");
            }

            p = db.PRODUCTs.FirstOrDefault(pr => pr.id_product == id);

            if (p == null)
            {
                Response.Redirect("~/MenuP.aspx");
            }

            Page.Header.Title = string.Format("Fast food - {0}", p.title);

            if (IsPostBack==false)
            {
                TitleTextBox.Text = p.title;
                CategoryDropDownList.Text = p.category;
                PriceTextBox.Text = p.price.ToString();
                DescriptionTextArea.Text = p.description;
                ProductImage.ImageUrl = string.Format("~/pictures/products/{0}", p.picture);
                AvailableCheckBox.Checked = p.available;
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            string folder = "~/pictures/products/";

            if (!string.IsNullOrEmpty(PictureUpload.FileName))
            {
                p.picture = MyHelper.SavePicture(PictureUpload, this, folder);
                ProductImage.ImageUrl = string.Format("~/pictures/products/{0}", p.picture);
            }

            p.title = TitleTextBox.Text.Trim();
            p.description = DescriptionTextArea.Text.Trim();
            p.category = CategoryDropDownList.Text.Trim();
            p.price = (decimal)Convert.ToDouble(PriceTextBox.Text.Trim());

            p.available = AvailableCheckBox.Checked;

            p.id_admin = ((TADMIN)HttpContext.Current.Session["admin"]).id_admin;

            db.SaveChanges();
        }
    }
}