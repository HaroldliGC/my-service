using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BegoniaService.Models;

namespace BegoniaService.Controllers
{
    public class BookImagesController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        // GET: api/BookImages
        [Authorize]
        public IQueryable<BookImage> GetBookImages()
        {
            return db.BookImages;
        }

        // GET: api/BookImages/5
        [Authorize]
        [ResponseType(typeof(BookImage))]
        public async Task<IHttpActionResult> GetBookImage(int id)
        {
            BookImage bookImage = await db.BookImages.FindAsync(id);
            if (bookImage == null)
            {
                return NotFound();
            }

            return Ok(bookImage);
        }

        // PUT: api/BookImages/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookImage(int id, BookImage bookImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookImage.Id)
            {
                return BadRequest();
            }

            db.Entry(bookImage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookImageExists(id))
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

        // POST: api/BookImages
        [Authorize]
        public string PostBookImageByManager()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            string rec = "failed";

            if (file != null && file.ContentLength > 0)
            {
                //按照当前时间生成文件名
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString();
                //string fileName = Path.GetFileName(file.FileName);
                string Extent = Path.GetExtension(file.FileName);
                fileName += Extent;
                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                rec = "sucess";
            }
            return rec;
        }

        // POST: api/BookImages
        [Authorize]
        [ResponseType(typeof(BookImage))]
        public async Task<IHttpActionResult> PostBookImage(BookImage bookImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookImages.Add(bookImage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bookImage.Id }, bookImage);
        }

        // DELETE: api/BookImages/5
        [Authorize]
        [ResponseType(typeof(BookImage))]
        public async Task<IHttpActionResult> DeleteBookImage(int id)
        {
            BookImage bookImage = await db.BookImages.FindAsync(id);
            if (bookImage == null)
            {
                return NotFound();
            }

            db.BookImages.Remove(bookImage);
            await db.SaveChangesAsync();

            return Ok(bookImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookImageExists(int id)
        {
            return db.BookImages.Count(e => e.Id == id) > 0;
        }
    }
}