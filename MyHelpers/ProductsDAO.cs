using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;

namespace WebFastfood.MyHelpers
{

    public class ProductsDAO
    {

        public static List<PRODUCT> GetPRODUCTsByCategory(string category)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.PRODUCTs.Where(p => p.category == category && p.available == true).OrderBy(p => p.title).ToList();
        }

        public static List<PRODUCT> GetPRODUCTsByCategorySearch(string category, string search)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.PRODUCTs.Where(p => p.category == category && p.available == true).Where(p => p.title.Contains(search) || p.description.Contains(search)).OrderBy(p => p.title).ToList();
        }

        public static List<PRODUCT> GetUnavailablePRODUCTs()
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.PRODUCTs.Where(p => p.available == false).OrderBy(p => p.title).ToList();
        }

        public static List<PRODUCT> GetUnavailablePRODUCTsBySearch(string search)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.PRODUCTs.Where(p => p.available == false).Where(p => p.title.Contains(search) || p.description.Contains(search)).OrderBy(p => p.title).ToList();
        }
    }
}