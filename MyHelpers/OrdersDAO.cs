using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;

namespace WebFastfood.MyHelpers
{
    public class OrdersDAO
    {
        public static List<TORDER> GetORDERsByState(string state)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.TORDERs.Where(o => o.order_state == state)
                .OrderBy(o => o.order_datetime)
                .ThenBy(o => o.CLIENT.firstname)
                .ThenBy(o => o.CLIENT.lastname).ToList();
        }


        public static List<TORDER> GetORDERsByStateAndDate(string state, string date)
        {
            FastFoodEntities db = new FastFoodEntities();

            DateTime minD = Convert.ToDateTime(date);
            DateTime maxD = Convert.ToDateTime(date);
            maxD = maxD.AddDays(1);

            return db.TORDERs.Where(o => o.order_state==state && o.order_datetime>=minD && o.order_datetime<maxD)
                .OrderBy(o => o.order_datetime)
                .ThenBy(o => o.CLIENT.firstname)
                .ThenBy(o => o.CLIENT.lastname).ToList();
        }

        public static List<TORDER> GetORDERsByStateAndSearch(string state, string search)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.TORDERs.Where(o => o.order_state == state)
                .Where(o => o.CLIENT.firstname.Contains(search) || o.CLIENT.lastname.Contains(search))
                .OrderBy(o => o.order_datetime)
                .ThenBy(o => o.CLIENT.firstname)
                .ThenBy(o => o.CLIENT.lastname).ToList();
        }

        public static List<TORDER> GetORDERsByStateAndSearchAndDate(string state, string search, string date)
        {
            FastFoodEntities db = new FastFoodEntities();

            DateTime minD = Convert.ToDateTime(date);
            DateTime maxD = Convert.ToDateTime(date);
            maxD = maxD.AddDays(1);

            return db.TORDERs.Where(o => o.order_state == state)
                .Where(o => (o.CLIENT.firstname.Contains(search) || o.CLIENT.lastname.Contains(search)) && o.order_datetime >= minD && o.order_datetime < maxD)
                .OrderBy(o => o.order_datetime)
                .ThenBy(o => o.CLIENT.firstname)
                .ThenBy(o => o.CLIENT.lastname).ToList();
        }
    }
}