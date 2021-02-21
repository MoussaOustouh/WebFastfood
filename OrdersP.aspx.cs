using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFastfood.Models;
using WebFastfood.MyHelpers;

namespace WebFastfood
{
    public partial class OrdersP : System.Web.UI.Page
    {
        private FastFoodEntities db = new FastFoodEntities();
        private List<TORDER> l1 = null;
        private string order_state = "";
        private string search = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["admin"] == null)
            {
                Response.Redirect("~/LoginP.aspx");
            }

            Page.Header.Title = "Fast food - Orders";


            if (IsPostBack == false)
            {
                SearchTextBox.Text = "";
                this.order_state = OrderStates.order_to_deliver;
                OrdersStateTextBox.Text = this.order_state;
                l1 = OrdersDAO.GetORDERsByState(this.order_state);
                OrdersListView.DataSource = l1;
                OrdersListView.DataBind();
            }
        }

        protected void OrdersListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                GridView OrderContentGridView = e.Item.FindControl("OrderContentGridView") as GridView;

                TextBox IdOrderTextBox = e.Item.FindControl("IdOrderTextBox") as TextBox;

                int idOrder = Convert.ToInt32(IdOrderTextBox.Text);

                List<ORDER_CONTENT> lOC= l1.FirstOrDefault(o => o.id_order == idOrder)
                    .ORDER_CONTENT.OrderBy(oc => oc.PRODUCT.title).ToList();

                OrderContentGridView.DataSource = lOC;
                OrderContentGridView.DataBind();

                Label TotalPriceLabel = e.Item.FindControl("TotalPriceLabel") as Label;
                float tp = (float)lOC.Select(oc => oc.price * oc.quantity).Sum();

                TotalPriceLabel.Text = tp.ToString("0.00");


                Panel DeliveryManPanel = e.Item.FindControl("DeliveryManPanel") as Panel;
                Panel TransportPanel = e.Item.FindControl("TransportPanel") as Panel;
                Panel ReceivedTimePanel = e.Item.FindControl("ReceivedTimePanel") as Panel;

                if (this.order_state==OrderStates.order_on_the_way)
                {
                    DeliveryManPanel.Visible = true;
                    TransportPanel.Visible = true;
                    ReceivedTimePanel.Visible = false;
                }
                else if (this.order_state==OrderStates.order_has_been_delivered)
                {
                    DeliveryManPanel.Visible = true;
                    TransportPanel.Visible = false;
                    ReceivedTimePanel.Visible = true;
                }
            }
        }

        protected void OrdersToDeliverSection_Click(object sender, EventArgs e)
        {
            this.order_state = OrderStates.order_to_deliver;
            OrdersStateTextBox.Text = this.order_state;

            GetAndBindOrders(this.order_state);
        }

        protected void OrdersOnTheWaySection_Click(object sender, EventArgs e)
        {
            this.order_state = OrderStates.order_on_the_way;
            OrdersStateTextBox.Text = this.order_state;

            GetAndBindOrders(this.order_state);
        }

        protected void OrdersHaveBeenDeliveredSection_Click(object sender, EventArgs e)
        {
            this.order_state = OrderStates.order_has_been_delivered;
            OrdersStateTextBox.Text = this.order_state;

            GetAndBindOrders(this.order_state);
        }

        protected void DateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OrdersStateTextBox.Text = this.order_state;

            GetAndBindOrders(this.order_state);
        }

        protected void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            OrdersStateTextBox.Text = this.order_state;

            GetAndBindOrders(this.order_state);
        }


        private void GetAndBindOrders(string order_state)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                this.search = SearchTextBox.Text.Trim();
            }

            if (DateCheckBox.Checked && !string.IsNullOrEmpty(DateTextBox.Text.Trim()))
            {
                if (string.IsNullOrEmpty(this.search))
                {
                    l1 = OrdersDAO.GetORDERsByStateAndDate(order_state, DateTextBox.Text.Trim());
                    OrdersListView.DataSource = l1;
                    OrdersListView.DataBind();
                }
                else
                {
                    l1 = OrdersDAO.GetORDERsByStateAndSearchAndDate(order_state, this.search.Trim(), DateTextBox.Text.Trim());
                    OrdersListView.DataSource = l1;
                    OrdersListView.DataBind();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.search))
                {
                    l1 = OrdersDAO.GetORDERsByState(order_state);
                    OrdersListView.DataSource = l1;
                    OrdersListView.DataBind();
                }
                else
                {
                    l1 = OrdersDAO.GetORDERsByStateAndSearch(order_state, this.search.Trim());
                    OrdersListView.DataSource = l1;
                    OrdersListView.DataBind();
                }
            }
            
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            switch (OrdersStateTextBox.Text.Trim())
            {
                case "Order to deliver": this.order_state = "Order to deliver"; GetAndBindOrders(this.order_state); break;
                case "The order is on the way": this.order_state = "The order is on the way"; GetAndBindOrders(this.order_state); break;
                case "The order has been delivered": this.order_state = "The order has been delivered"; GetAndBindOrders(this.order_state); break;
            }
        }
    }
}