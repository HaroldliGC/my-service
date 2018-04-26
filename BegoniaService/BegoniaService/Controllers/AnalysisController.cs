using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BegoniaService.Models;
using BegoniaService.Dots;
using System.Threading.Tasks;

namespace BegoniaService.Controllers
{
    public class AnalysisController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        [Authorize]
        public IQueryable<BookByNumber> GetBookByNumber()
        {
            var book = from b in db.Books
                       orderby b.BorrowNumber descending
                       select new BookByNumber()
                       {
                           BookName = b.Name,
                           BorrowNumber = b.BorrowNumber,
                           BookIsbn = b.Isbn,
                       };
            var bookByNumber = book.Take(10);
            return bookByNumber;
        }

        [Authorize]
        public IQueryable<BookByReview> GetBookByReView()
        {
            var book = from r in db.BookReviews
                               group r by r.BookId
                               into g
                               select new 
                               {
                                   BookId = g.Key,
                                   BookNumber = g.Count(),
                                   BookName = (from b in db.Books where b.Id == g.Key select b.Name).FirstOrDefault(),
                                   BookIsbn = (from b in db.Books where b.Id == g.Key select b.Isbn).FirstOrDefault(),
                               };
            //var newBook = book.Take(10);
            var newBook = from b in book
                          orderby b.BookNumber descending
                               select new BookByReview()
                               {
                                   BookName = b.BookName,
                                   ReviewNumber = b.BookNumber,
                                   BookIsbn = b.BookIsbn
                               };
            var bookByReview = newBook.Take(10);
            return bookByReview;
        }

        private string getBookNameById( int bookId)
        {
            BegoniaServiceContext db = new BegoniaServiceContext();
            var book = db.Books.Where(b => b.Id == bookId);

            string bookName = book.ToArray()[0].Name;
            return bookName;
        }
    }

}
