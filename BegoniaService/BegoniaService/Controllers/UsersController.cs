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
using BegoniaService.Dots;

namespace BegoniaService.Controllers
{
    public class UsersController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        // GET: api/Users
        [Authorize]
        public IQueryable<UserInf> GetUsers()
        {
            var Users = from b in db.Users
                        select new UserInf()
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Account = b.Account,
                            State = b.State,
                            Gender = b.Gender,
                            Email = b.Email,
                            Phone = b.Phone,
                            License = b.License,
                            Identity = b.Identity,
                        };
            return Users;
        }

        // GET: api/Users/5
        [Authorize]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public IQueryable<UserInf> GetReaderUserBySearch([FromUri] string Name, [FromUri] string AccountNumber)
        {
            if (Name == null)
            {
                Name = "";
            }
            var temp = db.Users.Where(user => user.Name.Contains(Name));
            if (AccountNumber != null)
            {
                temp = temp.Where(user => user.Account.Contains(AccountNumber));
            }
            var rec = from user in temp
                      select new UserInf()
                      {
                          Id = user.Id,
                          Name = user.Name,
                          Account = user.Account,
                          State = user.State,
                          Gender = user.Gender,
                          Email = user.Email,
                          Phone = user.Phone,
                          License = user.License,
                          Identity = user.Identity,
                      };
            return rec;
        }

        // PUT: api/Users/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserState(int id, UserInf userInf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInf.Id)
            {
                return BadRequest();
            }

            User user = await db.Users.FindAsync(id);
            user.State = userInf.State;
            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [Authorize]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}