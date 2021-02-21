using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Helpers;

using WebFastfood.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using WebFastfood.Models.MyModels;
using WebFastfood.MyHelpers;
using System.Security.Claims;

namespace WebFastfood.Controllers
{
    public class AccountsController : ApiController
    {
        // GET: api/Accounts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Accounts/5
        public CLIENT Get(int id)
        {
            FastFoodEntities db = new FastFoodEntities();
            return db.CLIENTs.FirstOrDefault(c => c.id_client == id);
        }

        // POST: api/Accounts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Accounts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Accounts/5
        public void Delete(int id)
        {
        }


        //--- Client -----------------------------------------------------------------------------------
        [Route("api/Accounts/ClientCheckEmail")]
        [HttpPost]
        public JObject ClientCheckEmail([FromBody] CLIENT c)
        {
            FastFoodEntities db = new FastFoodEntities();

            JObject j;
            try {
                CLIENT c1 = db.CLIENTs.FirstOrDefault(b => b.email == c.email);

                if (c1 == null)
                {
                    j = new JObject();
                    j.Add(new JProperty("email_already_used", false));
                    j.Add(new JProperty("Error", false));

                    return j;
                }
                else
                {
                    j = new JObject();
                    j.Add(new JProperty("email_already_used", true));
                    j.Add(new JProperty("Error", false));

                    return j;
                }
            }
            catch (Exception e)
            {
                j = new JObject();
                j.Add(new JProperty("Error", true));

                return j;
            }
        }

        [Route("api/Accounts/ClientSignUp")]
        [HttpPost]
        public JObject ClientSignUp([FromBody] CLIENT c)
        {
            FastFoodEntities db = new FastFoodEntities();

            JObject j;
            try
            {
                c.password = Crypto.HashPassword(c.password);
                c.photo = "client.png";
                db.CLIENTs.Add(c);
                db.SaveChanges();

                j = new JObject();
                j.Add(new JProperty("Error", false));

                return j;
            }
            catch (Exception e)
            {
                j = new JObject();
                j.Add(new JProperty("Error", true));

                return j;
            }
        }

        [Route("api/Accounts/ClientSignIn")]
        [HttpPost]
        public JObject clientSignIn([FromBody] Client client)
        {
            FastFoodEntities db = new FastFoodEntities();

            JObject j;
            try
            {
                Client c1=db.CLIENTs.Select(c => new Client() {
                    id_client=c.id_client,
                    firstname=c.firstname,
                    lastname=c.lastname,
                    gender=c.gender,
                    email=c.email,
                    password=c.password,
                    phone=c.phone,
                    photo=c.photo,
                    confirmation_code=c.confirmation_code
                }).FirstOrDefault(c => c.email == client.email);
            
                j = new JObject();
                j.Add(new JProperty("API error", false));

                if (c1!=null)
                {

                    if (Crypto.VerifyHashedPassword(c1.password, client.password))
                    {
                        j.Add(new JProperty("LoggedIn", true));
                        j.Add(new JProperty("Message", "Connected successfully."));


                        // add JWT
                        List<Claim> listClaims = new List<Claim>();
                        listClaims.Add(new Claim("user_type", "Client"));
                        listClaims.Add(new Claim("id_client", c1.id_client.ToString()));

                        j.Add(new JProperty("JWT", MyJWT.GenerateJWT(listClaims)));


                        // add id_user
                        j.Add(new JProperty("id_client", c1.id_client.ToString()));


                        // add user info
                        c1.password = "";
                        j.Add("user_info", c1.toJObject());

                    }
                    else
                    {
                        j.Add(new JProperty("LoggedIn", false));
                        j.Add(new JProperty("Message", "The information is incorrect."));
                    }
                }
                else
                {
                    j.Add(new JProperty("LoggedIn", false));
                    j.Add(new JProperty("Message", "The information is incorrect."));
                }

                return j;
            }
            catch (Exception e)
            {
                j = new JObject();
                j.Add(new JProperty("API error", true));
                j.Add(new JProperty("Message", "There is a problem try again."));

                return j;
            }
        }




        [Route("api/Accounts/DeliveryManSignIn")]
        [HttpPost]
        public JObject DeliveryManSignIn([FromBody] Delivery_man delivery_Man)
        {
            FastFoodEntities db = new FastFoodEntities();

            JObject j;
            try
            {
                Delivery_man d1 = db.DELIVERY_MAN.Select(d => new Delivery_man()
                {
                    id_delivery_man = d.id_delivery_man,
                    firstname = d.firstname,
                    lastname = d.lastname,
                    gender = d.gender,
                    email = d.email,
                    password = d.password,
                    phone = d.phone,
                    photo = d.photo,
                    transport = d.transport,
                    matricule = d.matricule,
                    latitude = d.latitude,
                    longitude = d.latitude,
                    authorized = d.authorized,
                    state = d.state,
                    id_admin = d.id_admin
                }).FirstOrDefault(d => d.email == delivery_Man.email);

                j = new JObject();
                j.Add(new JProperty("API error", false));

                if (d1 != null)
                {

                    if (Crypto.VerifyHashedPassword(d1.password, delivery_Man.password))
                    {
                        if (d1.authorized)
                        {
                            j.Add(new JProperty("LoggedIn", true));
                            j.Add(new JProperty("Message", "Connected successfully."));


                            // add JWT
                            List<Claim> listClaims = new List<Claim>();
                            listClaims.Add(new Claim("user_type", "Delivery man"));
                            listClaims.Add(new Claim("id_delivery_man", d1.id_delivery_man.ToString()));

                            j.Add(new JProperty("JWT", MyJWT.GenerateJWT(listClaims)));


                            // add id_user
                            j.Add(new JProperty("id_delivery_man", d1.id_delivery_man.ToString()));


                            // add user info
                            d1.password = "";
                            j.Add("user_info", d1.toJObject());
                        }
                        else
                        {
                            j.Add(new JProperty("LoggedIn", false));
                            j.Add(new JProperty("Message", "You are not authorized to deliver."));
                        }

                    }
                    else
                    {
                        j.Add(new JProperty("LoggedIn", false));
                        j.Add(new JProperty("Message", "The information is incorrect."));
                    }
                }
                else
                {
                    j.Add(new JProperty("LoggedIn", false));
                    j.Add(new JProperty("Message", "The information is incorrect."));
                }

                return j;
            }
            catch (Exception e)
            {
                j = new JObject();
                j.Add(new JProperty("API error", true));
                j.Add(new JProperty("Message", "There is a problem try again."));

                return j;
            }
        }






        [Route("api/Accounts/GetClient")]
        [HttpGet]
        public Client GetClient(int id_client)
        {
            FastFoodEntities db = new FastFoodEntities();

            return  db.CLIENTs.Select(c => new Client()
            {
                id_client = c.id_client,
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



        [Route("api/Accounts/GetClientById")]
        [HttpGet]
        public Client GetClientById(int id_client)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.CLIENTs.Select(c => new Client()
            {
                id_client = c.id_client,
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


        [Route("api/Accounts/GetDeliveryManById")]
        [HttpGet]
        public Delivery_man GetDeliveryManById(int id_delivery_man)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.DELIVERY_MAN.Select(d => new Delivery_man(){
                id_delivery_man=d.id_delivery_man,
                firstname=d.firstname,
                lastname=d.lastname,
                gender=d.gender,
                email=d.email,
                password=d.password,
                phone=d.phone,
                photo=d.photo,
                transport=d.transport,
                matricule=d.matricule,
                latitude=d.latitude,
                longitude=d.latitude,
                authorized=d.authorized,
                state=d.state,
                id_admin=d.id_admin
            }).FirstOrDefault(d => d.id_delivery_man == id_delivery_man);
        }




        [Route("api/Accounts/PutChangeClientInfo")]
        [HttpPut]
        public JObject PutChangeClientInfo([FromBody] Client client)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();

            CLIENT C1 = db.CLIENTs.FirstOrDefault(c => c.id_client == client.id_client);
            CLIENT C2 = db.CLIENTs.FirstOrDefault(c => c.email == client.email && c.id_client != client.id_client);

            if (C2!=null && C2.email == client.email)
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Change the email address"));
                jRes.Add(new JProperty("Message", "This email address is in use by another account."));
            }
            else if (Crypto.VerifyHashedPassword(C1.password, client.password))
            {
                jRes.Add(new JProperty("Error", false));

                C1.firstname = client.firstname;
                C1.lastname = client.lastname;
                C1.gender = client.gender;
                C1.email = client.email;
                C1.phone = client.phone;

                db.SaveChanges();

                Client c1 = db.CLIENTs.Select(c => new Client()
                {
                    id_client = c.id_client,
                    firstname = c.firstname,
                    lastname = c.lastname,
                    gender = c.gender,
                    email = c.email,
                    password = c.password,
                    phone = c.phone,
                    photo = c.photo,
                    confirmation_code = c.confirmation_code
                }).FirstOrDefault(c => c.id_client == C1.id_client);

                //add JWT
                List<Claim> listClaims = new List<Claim>();
                listClaims.Add(new Claim("user_type", "Client"));
                listClaims.Add(new Claim("id_client", c1.id_client.ToString()));

                jRes.Add(new JProperty("JWT", MyJWT.GenerateJWT(listClaims)));


                // add id_user
                jRes.Add(new JProperty("id_client", c1.id_client.ToString()));


                // add user info
                c1.password = "";
                jRes.Add("user_info", c1.toJObject());

            }
            else
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Message"));
                jRes.Add(new JProperty("Message", "The password is incorrect."));
            }

            return jRes;
        }



        [Route("api/Accounts/PutChangeClientPassword")]
        [HttpPut]
        public JObject PutChangeClientPassword([FromBody] Client client)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();

            CLIENT C1 = db.CLIENTs.FirstOrDefault(c => c.id_client == client.id_client);

            if (Crypto.VerifyHashedPassword(C1.password, client.password))
            {
                try
                {
                    jRes.Add(new JProperty("Error", false));
                    C1.password = Crypto.HashPassword(client.confirmation_code);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    jRes.Add(new JProperty("Error", true));
                    jRes.Add(new JProperty("TitleMessage", "Message"));
                    jRes.Add(new JProperty("Message", "Connection failed."));
                }
            }
            else
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Message"));
                jRes.Add(new JProperty("Message", "The password is incorrect."));
            }

            return jRes;
        }




        [Route("api/Accounts/PutChangeDeliveryManInfo")]
        [HttpPut]
        public JObject PutChangeDeliveryManInfo([FromBody] Delivery_man dm)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();

            DELIVERY_MAN D1 = db.DELIVERY_MAN.FirstOrDefault(d => d.id_delivery_man == dm.id_delivery_man);
            DELIVERY_MAN D2 = db.DELIVERY_MAN.FirstOrDefault(d => d.email == dm.email && d.id_delivery_man != dm.id_delivery_man) ;

            if (D2 != null && D2.email == dm.email)
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Change the email address"));
                jRes.Add(new JProperty("Message", "This email address is in use by another account."));
            }
            else if (Crypto.VerifyHashedPassword(D1.password, dm.password))
            {
                jRes.Add(new JProperty("Error", false));

                D1.firstname = dm.firstname;
                D1.lastname = dm.lastname;
                D1.gender = dm.gender;
                D1.email = dm.email;
                D1.phone = dm.phone;

                db.SaveChanges();

                Delivery_man d1 = db.DELIVERY_MAN.Select(d => new Delivery_man()
                {
                    id_delivery_man = d.id_delivery_man,
                    firstname = d.firstname,
                    lastname = d.lastname,
                    gender = d.gender,
                    email = d.email,
                    password = d.password,
                    phone = d.phone,
                    photo = d.photo,
                    transport = d.transport,
                    matricule = d.matricule,
                    latitude = d.latitude,
                    longitude = d.latitude,
                    authorized = d.authorized,
                    state = d.state,
                    id_admin = d.id_admin
                }).FirstOrDefault(d => d.id_delivery_man == D1.id_delivery_man);


                // add JWT
                List<Claim> listClaims = new List<Claim>();
                listClaims.Add(new Claim("user_type", "Delivery man"));
                listClaims.Add(new Claim("id_delivery_man", d1.id_delivery_man.ToString()));

                jRes.Add(new JProperty("JWT", MyJWT.GenerateJWT(listClaims)));


                // add id_user
                jRes.Add(new JProperty("id_delivery_man", d1.id_delivery_man.ToString()));


                // add user info
                d1.password = "";
                jRes.Add("user_info", d1.toJObject());



            }
            else
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Message"));
                jRes.Add(new JProperty("Message", "The password is incorrect."));
            }

            return jRes;
        }



        [Route("api/Accounts/PutChangeDeliveryManPassword")]
        [HttpPut]
        public JObject PutChangeDeliveryManPassword([FromBody] Delivery_man dm)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();

            DELIVERY_MAN D1 = db.DELIVERY_MAN.FirstOrDefault(d => d.id_delivery_man == dm.id_delivery_man);

            if (Crypto.VerifyHashedPassword(D1.password, dm.password))
            {
                try
                {
                    D1.password = Crypto.HashPassword(dm.confirmation_code);
                    db.SaveChanges();
                    jRes.Add(new JProperty("Error", false));
                }
                catch(Exception e)
                {
                    jRes.Add(new JProperty("Error", true));
                    jRes.Add(new JProperty("TitleMessage", "Message"));
                    jRes.Add(new JProperty("Message", "Connection failed."));
                }

            }
            else
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Message"));
                jRes.Add(new JProperty("Message", "The password is incorrect."));
            }

            return jRes;
        }


        [Route("api/Accounts/PutChangeDeliveryManTransport")]
        [HttpPut]
        public JObject PutChangeDeliveryManTransport([FromBody] Delivery_man dm)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();

            DELIVERY_MAN D1 = db.DELIVERY_MAN.FirstOrDefault(d => d.id_delivery_man == dm.id_delivery_man);

            try
            {
                D1.transport = dm.transport;
                D1.matricule = dm.matricule;
                db.SaveChanges();

                jRes.Add(new JProperty("Error", false));
            }
            catch(Exception e)
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("TitleMessage", "Message"));
                jRes.Add(new JProperty("Message", "Connection failed."));
            }

            return jRes;
        }










    }
}
