using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;

namespace WebFastfood.MyHelpers
{
    public class OrderContentDAO
    {

        public static List<Order_content> GetOrderContents(int id_order)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.ORDER_CONTENT.Select(o => new Order_content()
            {
                id_order = o.id_order,
                id_product = o.id_product,
                productTitle = o.PRODUCT.title,
                quantity = o.quantity,
                price = o.price
            }).Where(oc => oc.id_order == id_order).OrderBy(oc => oc.productTitle).ToList();
        }

        public static Order_content GetOrderContent(int id_order, int id_product)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.ORDER_CONTENT.Select(o => new Order_content()
            {
                id_order = o.id_order,
                id_product = o.id_product,
                productTitle = o.PRODUCT.title,
                quantity = o.quantity,
                price = o.price
            }).FirstOrDefault(oc => oc.id_order == id_order && oc.id_product==id_product);
        }
    }
}