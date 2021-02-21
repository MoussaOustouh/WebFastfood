using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;

namespace WebFastfood.MyHelpers
{
    public class GeolocationDAO
    {

        public static List<Geolocation> GetOrderGeolocations(int id_order)
        {
            FastFoodEntities db = new FastFoodEntities();

            List<GEOLOCATION> list=db.GEOLOCATIONs.Where(g => g.id_order == id_order).ToList();

            return list.Select(g => new Geolocation()
            {
                id_geolocation = g.id_geolocation,
                latitude = g.latitude,
                longitude = g.longitude,
                datetime = g.datetime,
                id_order = g.id_order
            }).Where(g => g.id_order==id_order).ToList();

        }
    }
}