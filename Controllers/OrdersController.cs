using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFastfooc.MyHelpers;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;
using WebFastfood.MyHelpers;

namespace WebFastfood.Controllers
{
    public class OrdersController : ApiController
    {

        // GET: api/Orders
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Orders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }



        [Route("api/Orders/GetClientOrdersByState")]
        [HttpGet]
        public List<TOrder> GetClientOrdersByState(int id_client, string state)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            List<TORDER> listT = db.TORDERs.Where(o => o.id_client == id_client && o.order_state == state).OrderByDescending(o => o.order_datetime).ThenByDescending(o => o.id_order).ToList();

            return listT.Select(o => new TOrder()
            {
                id_order = o.id_order,
                order_state = o.order_state,
                latitude = o.latitude,
                longitude = o.longitude,
                order_datetime = o.order_datetime,
                order_code = o.order_code,
                delivery_state = o.delivery_state,
                received_datetime = o.received_datetime,
                id_delivery_man = o.id_delivery_man,
                id_client = o.id_client
            }).ToList();
        }


        [Route("api/Orders/GetClientOrdersFullInfoByState")]
        [HttpGet]
        public List<OrderFullInfo> GetClientOrdersFullInfoByState(int id_client, string state)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            List<TORDER> list = db.TORDERs.Where(o => o.id_client == id_client && o.order_state == state).OrderByDescending(o => o.order_datetime).ThenByDescending(o => o.id_order).ToList();

            List <OrderFullInfo> ordersFullInfo = list.Select(o => new OrderFullInfo()
            {
                id_order = o.id_order,
                order_state = o.order_state,
                latitude = o.latitude,
                longitude = o.longitude,
                order_datetime = o.order_datetime,
                order_code = o.order_code,
                delivery_state = o.delivery_state,
                received_datetime = o.received_datetime,
                id_delivery_man = o.id_delivery_man,
                id_client = o.id_client,
                client=new Client()
                    {
                        id_client = o.CLIENT.id_client,
                        firstname = o.CLIENT.firstname,
                        lastname = o.CLIENT.lastname,
                        gender = o.CLIENT.gender,
                        email = o.CLIENT.email,
                        password = "",
                        phone = o.CLIENT.phone,
                        photo = o.CLIENT.photo,
                        confirmation_code = o.CLIENT.confirmation_code
                    }

            }).ToList();


            ordersFullInfo.ForEach(o => {
                if (o.id_delivery_man==null)
                {
                    o.delivery_man = new Delivery_man();
                }
                else
                {
                    o.delivery_man = DeliveryManDAO.GetDeliveryMan((int)o.id_delivery_man);
                }

                List<Geolocation> geolocations = GeolocationDAO.GetOrderGeolocations(o.id_order);
                if (geolocations==null)
                {
                    o.geolocations = new List<Geolocation>();
                }
                else
                {
                    o.geolocations = geolocations;
                }

                List<Order_content> orderContents = OrderContentDAO.GetOrderContents(o.id_order);
                if (orderContents==null)
                {
                    o.order_contents = new List<Order_content>();
                }
                else
                {
                    o.order_contents = orderContents;
                }
            });


            return ordersFullInfo;
        }







        [Route("api/Orders/GetOrderToDeliver")]
        [HttpGet]
        public List<OrderFullInfo> GetOrderToDeliver()
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            List<TORDER> list = db.TORDERs.Where(o => o.order_state == OrderStates.order_to_deliver).OrderBy(o => o.order_datetime).ThenBy(o => o.id_order).ToList();

            List<OrderFullInfo> ordersFullInfo = list.Select(o => new OrderFullInfo()
            {
                id_order = o.id_order,
                order_state = o.order_state,
                latitude = o.latitude,
                longitude = o.longitude,
                order_datetime = o.order_datetime,
                order_code = "",
                delivery_state = o.delivery_state,
                received_datetime = o.received_datetime,
                id_delivery_man = o.id_delivery_man,
                id_client = o.id_client,
                client = new Client()
                {
                    id_client = o.CLIENT.id_client,
                    firstname = o.CLIENT.firstname,
                    lastname = o.CLIENT.lastname,
                    gender = o.CLIENT.gender,
                    email = o.CLIENT.email,
                    password = "",
                    phone = o.CLIENT.phone,
                    photo = o.CLIENT.photo,
                    confirmation_code = o.CLIENT.confirmation_code
                }

            }).ToList();


            ordersFullInfo.ForEach(o => {
                if (o.id_delivery_man == null)
                {
                    o.delivery_man = new Delivery_man();
                }
                else
                {
                    o.delivery_man = DeliveryManDAO.GetDeliveryMan((int)o.id_delivery_man);
                }

                List<Geolocation> geolocations = GeolocationDAO.GetOrderGeolocations(o.id_order);
                if (geolocations == null)
                {
                    o.geolocations = new List<Geolocation>();
                }
                else
                {
                    o.geolocations = geolocations;
                }

                List<Order_content> orderContents = OrderContentDAO.GetOrderContents(o.id_order);
                if (orderContents == null)
                {
                    o.order_contents = new List<Order_content>();
                }
                else
                {
                    o.order_contents = orderContents;
                }
            });


            return ordersFullInfo;
        }







        [Route("api/Orders/GetOrderOnTheWayByDeliveryManId")]
        [HttpGet]
        public List<OrderFullInfo> GetOrderOnTheWayByDeliveryManId(int id_delivery_man)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            List<TORDER> list = db.TORDERs.Where(o => o.order_state == OrderStates.order_on_the_way && o.id_delivery_man == id_delivery_man).OrderBy(o => o.order_datetime).ThenBy(o => o.id_order).ToList();

            List<OrderFullInfo> ordersFullInfo = list.Select(o => new OrderFullInfo()
            {
                id_order = o.id_order,
                order_state = o.order_state,
                latitude = o.latitude,
                longitude = o.longitude,
                order_datetime = o.order_datetime,
                order_code = "",
                delivery_state = o.delivery_state,
                received_datetime = o.received_datetime,
                id_delivery_man = o.id_delivery_man,
                id_client = o.id_client,
                client = new Client()
                {
                    id_client = o.CLIENT.id_client,
                    firstname = o.CLIENT.firstname,
                    lastname = o.CLIENT.lastname,
                    gender = o.CLIENT.gender,
                    email = o.CLIENT.email,
                    password = "",
                    phone = o.CLIENT.phone,
                    photo = o.CLIENT.photo,
                    confirmation_code = o.CLIENT.confirmation_code
                }

            }).ToList();


            ordersFullInfo.ForEach(o => {
                if (o.id_delivery_man == null)
                {
                    o.delivery_man = new Delivery_man();
                }
                else
                {
                    o.delivery_man = DeliveryManDAO.GetDeliveryMan((int)o.id_delivery_man);
                }

                List<Geolocation> geolocations = GeolocationDAO.GetOrderGeolocations(o.id_order);
                if (geolocations == null)
                {
                    o.geolocations = new List<Geolocation>();
                }
                else
                {
                    o.geolocations = geolocations;
                }

                List<Order_content> orderContents = OrderContentDAO.GetOrderContents(o.id_order);
                if (orderContents == null)
                {
                    o.order_contents = new List<Order_content>();
                }
                else
                {
                    o.order_contents = orderContents;
                }
            });


            return ordersFullInfo;
        }






        [Route("api/Orders/GetOrderDeliveredByDeliveryManId")]
        [HttpGet]
        public List<OrderFullInfo> GetOrderDeliveredByDeliveryManId(int id_delivery_man)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            List<TORDER> list = db.TORDERs.Where(o => o.order_state == OrderStates.order_has_been_delivered && o.id_delivery_man == id_delivery_man).OrderByDescending(o => o.order_datetime).ToList();

            List<OrderFullInfo> ordersFullInfo = list.Select(o => new OrderFullInfo()
            {
                id_order = o.id_order,
                order_state = o.order_state,
                latitude = o.latitude,
                longitude = o.longitude,
                order_datetime = o.order_datetime,
                order_code = "",
                delivery_state = o.delivery_state,
                received_datetime = o.received_datetime,
                id_delivery_man = o.id_delivery_man,
                id_client = o.id_client,
                client = new Client()
                {
                    id_client = o.CLIENT.id_client,
                    firstname = o.CLIENT.firstname,
                    lastname = o.CLIENT.lastname,
                    gender = o.CLIENT.gender,
                    email = o.CLIENT.email,
                    password = "",
                    phone = o.CLIENT.phone,
                    photo = o.CLIENT.photo,
                    confirmation_code = o.CLIENT.confirmation_code
                }

            }).ToList();


            ordersFullInfo.ForEach(o => {
                if (o.id_delivery_man == null)
                {
                    o.delivery_man = new Delivery_man();
                }
                else
                {
                    o.delivery_man = DeliveryManDAO.GetDeliveryMan((int)o.id_delivery_man);
                }

                List<Geolocation> geolocations = GeolocationDAO.GetOrderGeolocations(o.id_order);
                if (geolocations == null)
                {
                    o.geolocations = new List<Geolocation>();
                }
                else
                {
                    o.geolocations = geolocations;
                }

                List<Order_content> orderContents = OrderContentDAO.GetOrderContents(o.id_order);
                if (orderContents == null)
                {
                    o.order_contents = new List<Order_content>();
                }
                else
                {
                    o.order_contents = orderContents;
                }
            });


            return ordersFullInfo;
        }






        [Route("api/Orders/GetOrderById")]
        [HttpGet]
        public TOrder GetOrderById(int id_order)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order == id_order);

            TOrder order = new TOrder();
            order.id_order = torder.id_order;
            order.order_state = torder.order_state;
            order.latitude = torder.latitude;
            order.longitude = torder.longitude;
            order.order_datetime = torder.order_datetime;
            order.order_code = torder.order_code;
            order.delivery_state = torder.delivery_state;
            order.received_datetime = torder.received_datetime;
            order.id_delivery_man = torder.id_delivery_man;
            order.id_client = torder.id_client;

            return order;
        }


        [Route("api/Orders/GetClientOrderFullInfoById")]
        [HttpGet]
        public OrderFullInfo GetClientOrderFullInfoById(int id_order)
        {
            FastFoodEntities db = new FastFoodEntities();

            // khasni n nsift JWT o n checkeh

            TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order == id_order);


            OrderFullInfo orderFullInfo = new OrderFullInfo();
            if (torder!=null)
            {
                orderFullInfo.id_order = torder.id_order;
                orderFullInfo.order_state = torder.order_state;
                orderFullInfo.latitude = torder.latitude;
                orderFullInfo.longitude = torder.longitude;
                orderFullInfo.order_datetime = torder.order_datetime;
                orderFullInfo.order_code = torder.order_code;
                orderFullInfo.delivery_state = torder.delivery_state;
                orderFullInfo.received_datetime = torder.received_datetime;
                orderFullInfo.id_delivery_man = torder.id_delivery_man;
                orderFullInfo.id_client = torder.id_client;
                orderFullInfo.client = new Client()
                {
                    id_client = torder.CLIENT.id_client,
                    firstname = torder.CLIENT.firstname,
                    lastname = torder.CLIENT.lastname,
                    gender = torder.CLIENT.gender,
                    email = torder.CLIENT.email,
                    password = "",
                    phone = torder.CLIENT.phone,
                    photo = torder.CLIENT.photo,
                    confirmation_code = torder.CLIENT.confirmation_code
                };
            }


            if (orderFullInfo.id_delivery_man != null)
            {
                orderFullInfo.delivery_man = DeliveryManDAO.GetDeliveryMan((int)orderFullInfo.id_delivery_man);
            }

            List<Geolocation> geolocations = GeolocationDAO.GetOrderGeolocations(orderFullInfo.id_order);
            if (geolocations != null)
            {
                orderFullInfo.geolocations = geolocations;
            }

            List<Order_content> orderContents = OrderContentDAO.GetOrderContents(orderFullInfo.id_order);
            if (orderContents != null)
            {
                orderFullInfo.order_contents = orderContents;
            }

            return orderFullInfo;
        }



        [Route("api/Orders/NewOrder")]
        [HttpPost]
        public OrderFullInfo NewOrder([FromBody] Client c)
        {
            // khassni nzidt JWT checking

            FastFoodEntities db = new FastFoodEntities();

            TORDER torder = new TORDER();
            torder.order_state = OrderStates.choosing_food;
            torder.id_client = c.id_client;

            db.TORDERs.Add(torder);
            db.SaveChanges();

            OrderFullInfo order = new OrderFullInfo();

            order.id_order = torder.id_order;
            order.order_state = torder.order_state;
            order.latitude = torder.latitude;
            order.longitude = torder.longitude;
            order.order_datetime = torder.order_datetime;
            order.order_code = torder.order_code;
            order.delivery_state = torder.delivery_state;
            order.received_datetime = torder.received_datetime;
            order.id_delivery_man = torder.id_delivery_man;
            order.id_client = torder.id_client;

            order.delivery_man = new Delivery_man();
            order.client = ClientDAO.GetClient(torder.id_client);
            
            return order;
        }



        [Route("api/Orders/DeleteOrder")]
        [HttpDelete]
        public bool DeleteOrder(int id_order)
        {
            // khassni nzidt JWT checking bach ghir mol l'order howa lli i9der imse7ha

            FastFoodEntities db = new FastFoodEntities();

            TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order == id_order && o.order_state == OrderStates.choosing_food);
            if (torder != null)
            {
                //bach n7yed lmochkil d Delete cascade

                List<ORDER_CONTENT> listOC = db.ORDER_CONTENT.Where(oc => oc.id_order == id_order).ToList();
                listOC.ForEach(oc => db.ORDER_CONTENT.Remove(oc));
                db.SaveChanges();

                List<GEOLOCATION> listGeo = db.GEOLOCATIONs.Where(oc => oc.id_order == id_order).ToList();
                listGeo.ForEach(g => db.GEOLOCATIONs.Remove(g));
                db.SaveChanges();

                db.TORDERs.Remove(torder);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        [Route("api/Orders/GetOrderContentsByIdOrder")]
        [HttpGet]
        public List<Order_content> GetOrderContentsByIdOrder(int id_order)
        {
            // khasni n nsift JWT o n checkeh


            return OrderContentDAO.GetOrderContents(id_order);
        }


        [Route("api/Orders/GetOrderContent")]
        [HttpGet]
        public Order_content GetOrderContent(int id_order, int id_product)
        {
            // khasni n nsift JWT o n checkeh

            return OrderContentDAO.GetOrderContent(id_order, id_product);
        }


        [Route("api/Orders/PostOrderContent")]
        [HttpPost]
        public JObject PostOrderContent([FromBody] Order_content oc)
        {
            // khasni n nsift JWT o n checkeh

            JObject jRes = new JObject();
            try
            {
                ORDER_CONTENT OC = new ORDER_CONTENT();
                OC.id_order = oc.id_order;
                OC.id_product = oc.id_product;
                OC.quantity = oc.quantity;
                OC.price = oc.price;

                FastFoodEntities db = new FastFoodEntities();

                db.ORDER_CONTENT.Add(OC);
                db.SaveChanges();

                jRes.Add(new JProperty("message", true));
            }
            catch(Exception e)
            {
                jRes.Add(new JProperty("message", false));
            }

            return jRes;
        }


        [Route("api/Orders/PutOrderContent")]
        [HttpPut]
        public JObject PutOrderContent([FromBody] Order_content oc)
        {
            // khasni n nsift JWT o n checkeh

            JObject jRes = new JObject();
            try
            {
                FastFoodEntities db = new FastFoodEntities();

                ORDER_CONTENT OrderC = db.ORDER_CONTENT.FirstOrDefault(OC =>OC.id_order == oc.id_order && OC.id_product == oc.id_product);

                OrderC.quantity = oc.quantity;
                OrderC.price = oc.price;

                db.SaveChanges();

                jRes.Add(new JProperty("message", true));
            }
            catch (Exception e)
            {
                jRes.Add(new JProperty("message", false));
            }

            return jRes;
        }


        [Route("api/Orders/DeleteOrderContent")]
        [HttpDelete]
        public JObject DeleteOrderContent(int id_order, int id_product)
        {
            // khasni n nsift JWT o n checkeh

            JObject jRes = new JObject();
            try
            {
                FastFoodEntities db = new FastFoodEntities();

                ORDER_CONTENT OrderC = db.ORDER_CONTENT.FirstOrDefault(oc => oc.id_order == id_order && oc.id_product == id_product);

                if (OrderC != null)
                {
                    db.ORDER_CONTENT.Remove(OrderC);
                    db.SaveChanges();
                }

                jRes.Add(new JProperty("message", true));
            }
            catch (Exception e)
            {
                jRes.Add(new JProperty("message", false));
            }

            return jRes;
        }




        [Route("api/Orders/ConfirmOrder")]
        [HttpPost]
        public JObject ConfirmOrder([FromBody] OrderFullInfo ofi)
        {
            // khasni n nsift JWT o n checkeh

            JObject jRes = new JObject();
            try
            {
                FastFoodEntities db = new FastFoodEntities();

                TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order==ofi.id_order);

                if (torder != null)
                {
                    torder.latitude = ofi.latitude;
                    torder.longitude = ofi.longitude;
                    torder.order_state = OrderStates.order_to_deliver;
                    torder.order_code = MyHelper.GenerateRandomNumber(111111, 999999).ToString();
                    torder.order_datetime = DateTime.Now;

                    db.SaveChanges();

                    jRes.Add(new JProperty("message", true));
                }
                else
                {
                    jRes.Add(new JProperty("message", false));
                }
            }
            catch (Exception e)
            {
                jRes.Add(new JProperty("message", false));
            }

            return jRes;
        }





        [Route("api/Orders/PutDeliveryManWillDeliverAnOrder")]
        [HttpPut]
        public JObject PutDeliveryManWillDeliverAnOrder([FromBody] OrderFullInfo ofi)
        {
            JObject jRes = new JObject();

            try
            {
                FastFoodEntities db = new FastFoodEntities();

                TORDER torder=db.TORDERs.FirstOrDefault(o => o.id_delivery_man == ofi.id_delivery_man && o.order_state==OrderStates.order_on_the_way);
                if (torder != null)
                {
                    jRes.Add(new JProperty("Error", true));
                    jRes.Add(new JProperty("MessageTitele", "Failed"));
                    jRes.Add(new JProperty("Message", "You have already an order to deliver it first."));
                }
                else
                {
                    torder=db.TORDERs.FirstOrDefault(o => o.id_order == ofi.id_order);

                    torder.id_delivery_man = ofi.id_delivery_man;
                    torder.order_state = OrderStates.order_on_the_way;
                    db.SaveChanges();

                    jRes.Add(new JProperty("Error", false));
                    jRes.Add(new JProperty("MessageTitele", "Success"));
                    jRes.Add(new JProperty("Message", "Go deliver it."));
                }

            }
            catch (Exception e)
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("MessageTitele", "Failed"));
                jRes.Add(new JProperty("Message", "Failed, try again."));
            }

            return jRes;
        }


        [Route("api/Orders/PutDeliveryManCancelDeliverinAnOrder")]
        [HttpPut]
        public JObject PutDeliveryManCancelDeliverinAnOrder([FromBody] OrderFullInfo ofi)
        {
            JObject jRes = new JObject();

            try
            {
                FastFoodEntities db = new FastFoodEntities();

                TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order == ofi.id_order);
                if (torder != null)
                {
                    torder.id_delivery_man = null;
                    torder.order_state = OrderStates.order_to_deliver;
                    db.SaveChanges();

                    jRes.Add(new JProperty("Error", false));
                    jRes.Add(new JProperty("MessageTitele", "Canceled"));
                    jRes.Add(new JProperty("Message", "Canceled."));
                }
                else
                {

                    jRes.Add(new JProperty("Error", true));
                    jRes.Add(new JProperty("MessageTitele", "Failed"));
                    jRes.Add(new JProperty("Message", "Failed, try again."));
                }

            }
            catch (Exception e)
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("MessageTitele", "Failed"));
                jRes.Add(new JProperty("Message", "Failed, try again."));
            }

            return jRes;
        }



        [Route("api/Orders/PutDelivringComplete")]
        [HttpPut]
        public JObject PutDelivringComplete([FromBody] OrderFullInfo ofi)
        {
            FastFoodEntities db = new FastFoodEntities();
            JObject jRes = new JObject();
            TORDER torder = db.TORDERs.FirstOrDefault(o => o.id_order == ofi.id_order);

            if(torder.order_code == ofi.order_code)
            {
                torder.order_state = OrderStates.order_has_been_delivered;
                torder.delivery_state = 100;
                torder.received_datetime = DateTime.Now;

                db.SaveChanges();

                jRes.Add(new JProperty("Error", false));
                jRes.Add(new JProperty("MessageTitele", "Success"));
                jRes.Add(new JProperty("Message", "The delivery was successful."));
            }
            else
            {
                jRes.Add(new JProperty("Error", true));
                jRes.Add(new JProperty("MessageTitele", "Failed"));
                jRes.Add(new JProperty("Message", "The order code is incorrect (You will get the order code from the clients when you give them the orders)."));
            }

            return jRes;
        }


    }
}
