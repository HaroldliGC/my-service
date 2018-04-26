using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BegoniaService.Models;

namespace BegoniaService.Controllers
{
    public class OrdersController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        // GET: api/Orders
        [Authorize]
        public IQueryable<Order> GetOrders()
        {
            return db.Orders.Include(b => b.Book).Include(b => b.User);
        }

        // GET: api/Orders/5
        [Authorize]
        [ResponseType(typeof(Order))]
        public async Task<Order> GetOrder(int id)
        {
            var temp = db.Orders.Where(b => b.Id == id).Include(b => b.Book).Include(b => b.User);
            var order = temp.ToArray()[0];
            return order;
        }

        // GET:
        [Authorize]
        [ResponseType(typeof(Order))]
        public IQueryable<Order> GetUnreturnOrderByUserId([FromUri] int userId)
        {
            string renting = "renting";
            var rec = db.Orders.Where(o => o.User.Id == userId && o.State == renting).Include(b => b.Book);
            return rec;
        }

        // GET:
        [Authorize]
        [ResponseType(typeof(Order))]
        public IQueryable<Order> GetReturnOrderByUserId([FromUri] int userId)
        {
            string renting = "renting";
            var rec = db.Orders.Where(o => o.User.Id == userId && o.State != renting).Include(b => b.Book);
            return rec;
        }

        // GET:
        [Authorize]
        [ResponseType(typeof(Order))]
        public IQueryable<Order> GetBusinessOrderBySearch([FromUri] string UserName, [FromUri] string BookName)
        {
            if (UserName == null)
            {
                UserName = "";
            }
            if (BookName == null)
            {
                BookName = "";
            }
            var temp = db.Orders.Where(order => order.User.Name.Contains(UserName)).Include(b => b.Book).Include(b => b.User);
            var rec = temp.Where(order => order.Book.Name.Contains(BookName)).Include(b => b.Book).Include(b => b.User);

            return rec;
        }

        // PUT: api/Orders/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [Authorize]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [Authorize]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}