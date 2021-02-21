using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFastfood.Models;
using WebFastfood.Models.MyModels;
using WebFastfood.MyHelpers;

namespace WebFastfood.Controllers
{
    public class ProductsController : ApiController
    {

        // GET: api/Products
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }

        [Route("api/Products/GetProductsByCategory")]
        [HttpGet]
        public List<Product> GetProductsByCategory(string categoryP)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.PRODUCTs.Select(p => new Product()
            {
                id_product = p.id_product,
                title = p.title,
                description = p.description,
                category = p.category,
                price = p.price,
                available = p.available,
                picture = p.picture,
                id_admin = p.id_admin
            })
             .Where(p => p.available == true && p.category == categoryP).OrderBy(p=>p.title).ToList();
        }


        [Route("api/Products/GetProductById")]
        [HttpGet]
        public Product GetProductById(int id_product)
        {
            FastFoodEntities db = new FastFoodEntities();

            return db.PRODUCTs.Select(p => new Product()
            {
                id_product = p.id_product,
                title = p.title,
                description = p.description,
                category = p.category,
                price = p.price,
                available = p.available,
                picture = p.picture,
                id_admin = p.id_admin
            })
            .FirstOrDefault(p => p.available == true && p.id_product == id_product);
        }
    }
}
