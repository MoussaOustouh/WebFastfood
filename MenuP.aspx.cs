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
    public partial class MenuP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();
        private List<PRODUCT> l1 = null;
        private string category = "";
        private string search = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }

            Page.Header.Title = "Fast food - Menu";


            if (IsPostBack == false)
            {
                SearchTextBox.Text = "";
                this.category = "Beverages";
                CategoryTextBox.Text = this.category;
                l1 = ProductsDAO.GetPRODUCTsByCategory(this.category);
                ProductsListView.DataSource = l1;
                ProductsListView.DataBind();
            }

        }

        protected void BeveragesSection_Click(object sender, EventArgs e)
        {
            this.category = "Beverages";
            CategoryTextBox.Text = this.category;

            GetAndBindProducts(this.category);
        }

        protected void BurgersSection_Click(object sender, EventArgs e)
        {
            this.category = "Burgers";
            CategoryTextBox.Text = this.category;

            GetAndBindProducts(this.category);
        }

        protected void FullMealSection_Click(object sender, EventArgs e)
        {
            this.category = "Full Meal";
            CategoryTextBox.Text = this.category;

            GetAndBindProducts(this.category);
        }

        protected void PizzaSection_Click(object sender, EventArgs e)
        {
            this.category = "Pizza";
            CategoryTextBox.Text = this.category;

            GetAndBindProducts(this.category);
        }

        protected void SaladsSection_Click(object sender, EventArgs e)
        {
            this.category = "Salads";
            CategoryTextBox.Text = this.category;

            GetAndBindProducts(this.category);
        }


        private void GetAndBindProducts(string category)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                this.search = SearchTextBox.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.search))
            {
                l1 = ProductsDAO.GetPRODUCTsByCategory(category);
                ProductsListView.DataSource = l1;
                ProductsListView.DataBind();
            }
            else
            {
                l1 = ProductsDAO.GetPRODUCTsByCategorySearch(category, this.search.Trim());
                ProductsListView.DataSource = l1;
                ProductsListView.DataBind();
            }
        }



        protected void UnavailableSection_Click(object sender, EventArgs e)
        {
            CategoryTextBox.Text = "Unavailable";
            GetAndBindUnavailableProducts();
        }

        private void GetAndBindUnavailableProducts()
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                this.search = SearchTextBox.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.search))
            {
                l1 = ProductsDAO.GetUnavailablePRODUCTs();
                ProductsListView.DataSource = l1;
                ProductsListView.DataBind();
            }
            else
            {
                l1 = ProductsDAO.GetUnavailablePRODUCTsBySearch(this.search.Trim());
                ProductsListView.DataSource = l1;
                ProductsListView.DataBind();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            switch (CategoryTextBox.Text.Trim())
            {
                case "Beverages": GetAndBindProducts("Beverages"); break;
                case "Burgers": GetAndBindProducts("Burgers"); break;
                case "Full Meal": GetAndBindProducts("Full Meal"); break;
                case "Pizza": GetAndBindProducts("Pizza"); break;
                case "Salads": GetAndBindProducts("Salads"); break;
                case "Unavailable": GetAndBindUnavailableProducts(); break;
            }
        }
    }
}