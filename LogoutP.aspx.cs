using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFastfood
{
    public partial class LogoutP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Fast food - Logout";

            HttpContext.Current.Session.Remove("admin");
            Response.Redirect("~/loginP.aspx");
        }
    }
}