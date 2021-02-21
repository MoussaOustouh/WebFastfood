using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Helpers;

using WebFastfood.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebFastfood.MyHelpers;

namespace WebFastfood
{
    public partial class LoginP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session!=null && HttpContext.Current.Session["admin"]!=null)
            {
                Response.Redirect("menuP.aspx");
            }
            else if (IsPostBack == false)
            {
                
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Text;
            string cryptoPassword = Crypto.HashPassword(password);

            TADMIN a1;
                a1=db.TADMINs.FirstOrDefault(b => b.email == email);


            if (a1 != null)
            {
                if (Crypto.VerifyHashedPassword(a1.password, password))
                {
                    HttpContext.Current.Session["admin"] = a1;
                    Response.Redirect("menuP.aspx");
                }
                else
                {
                    MessageLabel.Text = "The email or password is incorrect.";
                }
            }
            else
            {
                MessageLabel.Text = "The email or password is incorrect.";
            }
        }
    }
}