using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFastfood.Models;
using WebFastfood.MyHelpers;

namespace WebFastfood
{
    public partial class AddDeliveryManP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }
            else if (IsPostBack == false)
            {

            }
            Page.Title = "Fast food - Add delivery man";
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            DELIVERY_MAN dM = new DELIVERY_MAN();

            dM.firstname = FirstNameTextBox.Text.Trim();
            dM.lastname = LastNameTextBox.Text.Trim();
            dM.gender = GenderRadioButtonList.SelectedValue;
            dM.phone = PhoneTextBox.Text.Trim();
            dM.email = EmailTextBox1.Text.Trim();
            dM.password = Crypto.HashPassword(PasswordTextBox1.Text);
            dM.photo = "delivery_man.png";
            dM.authorized = true;
            dM.id_admin = ((TADMIN)HttpContext.Current.Session["admin"]).id_admin;

            db.DELIVERY_MAN.Add(dM);
            db.SaveChanges();

            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            GenderRadioButtonList.ClearSelection();
            PhoneTextBox.Text = "";
            EmailTextBox1.Text = "";
            PasswordTextBox1.Text = "";
        }
    }
}