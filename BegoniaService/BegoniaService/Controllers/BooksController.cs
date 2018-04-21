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
    public class BooksController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        // GET: api/Books
        [Authorize]
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Books/5
        [Authorize]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        //GET:api/Books/name
        [Authorize]
        public IQueryable<Book> GetBookBySearch([FromUri] BookSearchInf bookSearchInf)
        {
            if (bookSearchInf.Name == null)
            {
                bookSearchInf.Name = "";
            }
            var temp = db.Books.Where(book => book.Name.Contains(bookSearchInf.Name));
            if (bookSearchInf.Isbn != null)
            {
                temp = temp.Where(book => book.Isbn.Contains(bookSearchInf.Isbn));
            }
            if (bookSearchInf.Author != null)
            {
                temp = temp.Where(book => book.Author.Contains(bookSearchInf.Author));
            }
            if (bookSearchInf.Press != null)
            {
                temp = temp.Where(book => book.Press.Contains(bookSearchInf.Press));
            }
            if (bookSearchInf.Type != null)
            {
                temp = temp.Where(book => book.Type.Contains(bookSearchInf.Type));
            }

            return temp;
        }

        // PUT: api/Books/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [Authorize]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var temp = db.Books.Where(b => b.Isbn == book.Isbn);
            if (!temp.Any())
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
            }
            return StatusCode(HttpStatusCode.NoContent);

        }

        // DELETE: api/Books/5
        [Authorize]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}