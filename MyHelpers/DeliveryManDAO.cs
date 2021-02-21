using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;

namespace WebFastfood.MyHelpers
{
    public class DeliveryManDAO
    {

        public static List<DELIVERY_MAN> GetAuthorizedDELIVERY_MEN()
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.DELIVERY_MAN.Where(d => d.authorized == true).OrderBy(d => d.firstname).ThenBy(d => d.lastname).ToList();
        }

        public static List<DELIVERY_MAN> GetAuthorizedDELIVERY_MENBySearch(string search)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.DELIVERY_MAN.Where(d => d.authorized == true).Where(d => d.firstname.Contains(search) || d.lastname.Contains(search) || d.email.Contains(search)).OrderBy(d => d.firstname).ThenBy(d => d.lastname).ToList();
        }

        public static List<DELIVERY_MAN> GetNotAuthorizedDELIVERY_MEN()
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.DELIVERY_MAN.Where(d => d.authorized == false).OrderBy(d => d.firstname).ThenBy(d => d.lastname).ToList();
        }

        public static List<DELIVERY_MAN> GetNotAuthorizedDELIVERY_MENBySearch(string search)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.DELIVERY_MAN.Where(d => d.authorized == false).Where(d => d.firstname.Contains(search) || d.lastname.Contains(search) || d.email.Contains(search)).OrderBy(d => d.firstname).ThenBy(d => d.lastname).ToList();
        }


        public static Delivery_man GetDeliveryMan(int id_delivery_man)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.DELIVERY_MAN.Select(d => new Delivery_man()
            {
                id_delivery_man = d.id_delivery_man,
                firstname = d.firstname,
                lastname = d.lastname,
                gender = d.gender,
                email = d.email,
                password = "",
                phone = d.phone,
                photo = d.photo,
                transport = d.transport,
                matricule = d.matricule,
                latitude = d.latitude,
                longitude = d.latitude,
                authorized = d.authorized,
                state = d.state,
                id_admin = d.id_admin
            }).FirstOrDefault(d => d.id_delivery_man==id_delivery_man);
        }

    }
}