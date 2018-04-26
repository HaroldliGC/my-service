using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using BegoniaService.Models;
using BegoniaService.Dots;
using System.Data.Entity.Infrastructure;

namespace BegoniaService.Controllers
{
    public class BusinessController : ApiController
    {
        private BegoniaServiceContext db = new BegoniaServiceContext();

        [Authorize]
        public async Task<Object> borrowBookByUser(OrderInfByUser orderInf)
        {
            OrderReturnMessage rec = new OrderReturnMessage() { type = "failed" };
            if (!ModelState.IsValid)
            {
                rec.message = "数据格式有误";
                return rec;
            }
            Book book = await db.Books.FindAsync(orderInf.BookId);
            if (book.BorrowNumber >= book.Number)
            {
                rec.message = "该书没有库存";
                return rec;
            }
            int orderId = db.Orders.Select(n => n.Id).ToList().Max() + 1;
            Order newOrder = new Order() { Id = orderId, BookId = orderInf.BookId, UserId = orderInf.UserId, StartDate = orderInf.StartDate, EndDate = orderInf.EndDate, ReturnDate = DateTime.Now, State = "renting" };
            book.BorrowNumber = book.BorrowNumber + 1;
            //更新书籍信息
            db.Entry(book).State = EntityState.Modified;
            //插入数据
            db.Orders.Add(newOrder);

            await db.SaveChangesAsync();

            rec.message = "订单生成成功";
            rec.order = newOrder;
            rec.type = "success";

            return rec;
        }

        [Authorize]
        public async Task<Object> borrowBookByManager(OrderInf orderInf)
        {
            OrderReturnMessage rec = new OrderReturnMessage() { type = "failed" };
            if (!ModelState.IsValid)
            {
                rec.message = "数据格式有误";
                return rec;
            }
            var book = db.Books.Where(b => b.Isbn == orderInf.Isbn);
            if (!book.Any())
            {
                rec.message = "该书不存在";
                return rec;
            }
            var user = db.Users.Where(u => u.License == orderInf.License);
            if (!user.Any())
            {
                rec.message = "该用户不存在";
                return rec;
            }
            int bookId = book.ToArray()[0].Id;
            int userId = user.ToArray()[0].Id;
            int orderId = db.Orders.Select(n => n.Id).ToList().Max() + 1;

            Order newOrder = new Order() { Id = orderId, BookId = bookId, UserId = userId, StartDate = orderInf.StartDate, EndDate = orderInf.EndDate, ReturnDate = DateTime.Now,  State = "renting" };
            Book oldBook = await db.Books.FindAsync(bookId);
            oldBook.BorrowNumber = oldBook.BorrowNumber + 1;
            //更新书籍信息
            db.Entry(oldBook).State = EntityState.Modified;
            //插入数据
            db.Orders.Add(newOrder);

            await db.SaveChangesAsync();

            rec.message = "订单生成成功";
            rec.order = newOrder;
            rec.type = "success";

            return rec;
        }

        [Authorize]
        public async Task<Object> PutreturnBookByManager(int id)
        {
            OrderReturnMessage rec = new OrderReturnMessage() { type = "failed" };
            var temp = db.Orders.Where(o => o.Id == id).Include(b => b.Book).Include(b => b.User);
            Order order = temp.ToArray()[0];
            //Order order = await db.Orders.FindAsync(id);
            if (order.State == "done" || order.State == "overdone")
            {
                rec.message = "该书已归还";
                return rec;
            }
            DateTime endDate = order.EndDate;
            DateTime returnDate = DateTime.Now;
            string state = "done";
            if (DateTime.Compare(returnDate,endDate) > 0)
            {
                state = "overdone";
            }
            Book book = await db.Books.FindAsync(order.BookId);
            book.BorrowNumber = book.BorrowNumber - 1;
            //更新书籍信息
            db.Entry(book).State = EntityState.Modified;
            //更新订单信息
            order.State = state;
            order.ReturnDate = returnDate;
            db.Entry(order).State = EntityState.Modified;

            await db.SaveChangesAsync();

            rec.type = "success";
            rec.order = order;
            rec.message = "订单更新成功";

            return rec;
        }

        [Authorize]
        public async Task<Object> PutrenewBook(int id)
        {
            OrderReturnMessage rec = new OrderReturnMessage() { type = "failed" };
            var temp = db.Orders.Where(o => o.Id == id).Include(b => b.Book).Include(b => b.User);
            Order order = temp.ToArray()[0];
            if (order.State == "done" || order.State == "overdone")
            {
                rec.message = "该书已归还";
                return rec;
            }
            DateTime endDate = order.EndDate;
            DateTime returnDate = DateTime.Now;
            if (DateTime.Compare(returnDate, endDate) > 0)
            {
                rec.message = "该订单已逾期";
                return rec;
            }
            order.EndDate = endDate.AddDays(30);
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            rec.type = "success";
            rec.order = order;
            rec.message = "续借成功";

            return rec;
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
