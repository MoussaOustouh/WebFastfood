using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;

namespace WebFastfooc.MyHelpers
{
    public class ClientDAO
    {
        public static Client GetClient(int id_client)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.CLIENTs.Select(c => new Client()
            {
                id_client= c.id_client,
                firstname = c.firstname,
                lastname = c.lastname,
                gender = c.gender,
                email = c.email,
                password = "",
                phone = c.phone,
                photo = c.photo,
                confirmation_code = c.confirmation_code
            }).FirstOrDefault(c => c.id_client == id_client);
        }
    }
}