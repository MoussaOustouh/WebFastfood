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
    public partial class DeliveryMenP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();
        private List<DELIVERY_MAN> l1 = null;
        private bool authorization = true;
        private string search = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }

            Page.Header.Title = "Fast food - Delivery men";


            if (IsPostBack == false)
            {
                SearchTextBox.Text = "";
                this.authorization = true;
                AuthorizationTextBox.Text = this.authorization.ToString();
                l1 = DeliveryManDAO.GetAuthorizedDELIVERY_MEN();
                DeliveryMenListView.DataSource = l1;
                DeliveryMenListView.DataBind();
            }
        }

        protected void AuthorizedSection_Click(object sender, EventArgs e)
        {
            this.authorization = true;
            AuthorizationTextBox.Text = this.authorization.ToString();

            GetAndBindAuthorizedDeliveryMen();
        }

        private void GetAndBindAuthorizedDeliveryMen()
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                this.search = SearchTextBox.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.search))
            {
                l1 = DeliveryManDAO.GetAuthorizedDELIVERY_MEN();
                DeliveryMenListView.DataSource = l1;
                DeliveryMenListView.DataBind();
            }
            else
            {
                l1 = DeliveryManDAO.GetAuthorizedDELIVERY_MENBySearch(this.search.Trim());
                DeliveryMenListView.DataSource = l1;
                DeliveryMenListView.DataBind();
            }
        }



        protected void NotAuthorizedSection_Click(object sender, EventArgs e)
        {
            this.authorization = false;
            AuthorizationTextBox.Text = this.authorization.ToString();

            GetAndBindNotAuthorizedDeliveryMen();
        }

        private void GetAndBindNotAuthorizedDeliveryMen()
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                this.search = SearchTextBox.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.search))
            {
                l1 = DeliveryManDAO.GetNotAuthorizedDELIVERY_MEN();
                DeliveryMenListView.DataSource = l1;
                DeliveryMenListView.DataBind();
            }
            else
            {
                l1 = DeliveryManDAO.GetNotAuthorizedDELIVERY_MENBySearch(this.search.Trim());
                DeliveryMenListView.DataSource = l1;
                DeliveryMenListView.DataBind();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(AuthorizationTextBox.Text.Trim());
            switch (AuthorizationTextBox.Text.Trim().ToLower())
            {
                case "true": GetAndBindAuthorizedDeliveryMen(); break;
                case "false": GetAndBindNotAuthorizedDeliveryMen(); break;
            }
        }
    }
}