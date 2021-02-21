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
    public partial class DeliveryManP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();
        private DELIVERY_MAN dM;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }

            int id = 0;
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !Int32.TryParse(Request.QueryString["id"], out id))
            {
                Response.Redirect("~/DeliveryMenP.aspx");
            }

            dM = db.DELIVERY_MAN.FirstOrDefault(dm => dm.id_delivery_man == id);

            if (dM == null)
            {
                Response.Redirect("~/DeliveryMenP.aspx");
            }

            Page.Header.Title = string.Format("Fast food - {0} {1}", dM.firstname, dM.lastname);

            if (IsPostBack == false)
            {
                FirstNameTextBox.Text = dM.firstname;
                LastNameTextBox.Text = dM.lastname;
                GenderRadioButtonList.SelectedValue=dM.gender;
                PhoneTextBox.Text = dM.phone;
                EmailTextBox1.Text = dM.email;
                PasswordTextBox1.Text = "";
                if (!string.IsNullOrEmpty(dM.photo))
                {
                    DeliveryManImage.ImageUrl = "~/pictures/delivery_men/" + dM.photo;
                }
                else
                {
                    DeliveryManImage.ImageUrl = "~/pictures/delivery_men/delivery_man.png";
                }

                switch (dM.transport)
                {
                    case "car.png": TransportImage.ImageUrl = "~/pictures/app/car.png"; break;
                    case "moto.png": TransportImage.ImageUrl = "~/pictures/app/moto.png"; break;
                    default: TransportImage.ImageUrl= "~/pictures/app/unknown.png"; break;
                }

                MatriculeTextBox.Text = dM.matricule;

                AuthorizedCheckBox.Checked = dM.authorized;
            }

        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            dM.firstname = FirstNameTextBox.Text.Trim();
            dM.lastname = LastNameTextBox.Text.Trim();
            dM.gender = GenderRadioButtonList.SelectedValue;
            dM.phone = PhoneTextBox.Text.Trim();
            dM.email = EmailTextBox1.Text.Trim();

            if (!string.IsNullOrEmpty(PasswordTextBox1.Text))
            {
                dM.password = Crypto.HashPassword(PasswordTextBox1.Text);
            }

            dM.photo = "delivery_man.png";
            dM.authorized = AuthorizedCheckBox.Checked;
            dM.id_admin = ((TADMIN)HttpContext.Current.Session["admin"]).id_admin;

            db.SaveChanges();
        }
    }
}