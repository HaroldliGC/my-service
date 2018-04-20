namespace BegoniaService.Migrations
{
    using BegoniaService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BegoniaService.Models.BegoniaServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BegoniaService.Models.BegoniaServiceContext context)
        {
            DateTime[] startDates = new DateTime[] { new DateTime(2018, 1, 1), new DateTime(2017, 11, 21), new DateTime(2018, 2, 1), new DateTime(2018, 1, 1), new DateTime(2018, 1, 15) };
            DateTime[] endDates = new DateTime[] { new DateTime(2018, 2, 1), new DateTime(2018, 1, 21), new DateTime(2018, 5, 1), new DateTime(2018, 2, 1), new DateTime(2018, 2, 15) };
            DateTime[] returnDates = new DateTime[] { new DateTime(2018, 1, 29), new DateTime(2018, 1, 1), new DateTime(2018, 1, 1), new DateTime(2018, 2, 28), new DateTime(2018, 2, 1) };

            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Name = "LiMai", Gender = "man", State = "normal", Account = "1029147576@qq.com", Password = "123456", Phone = "15002993081", Email = "1029147576@qq.com", License="610426199606050011",  Identity="manager"},
                new User() { Id = 2, Name = "SunHang", Gender = "man", State = "normal", Account = "7827576@qq.com", Password = "123456", Phone = "13636735987", Email = "7827576@qq.com", License = "610426199606050012", Identity = "user" },
                new User() { Id = 3, Name = "LiJiao", Gender = "woman", State = "normal", Account = "532892348@qq.com", Password = "123456", Phone = "13772591819", Email = "532892348@qq.com", License = "610426199606050013", Identity = "user" }
            );
            context.BookReviews.AddOrUpdate(x => x.Id,
                new BookReview() { Id = 1, BookId = 1, UserId = 1, Review = "这本书真好看，哈哈", Date = startDates[0]},
                new BookReview() { Id = 2, BookId = 1, UserId = 2, Review = "从这本书中我学习到了很多新的知识，赞一个", Date = startDates[1] },
                new BookReview() { Id = 3, BookId = 2, UserId = 1, Review = "这本书真好看，给了我很多启发，哈哈", Date = startDates[2] }
            );
            context.BookImages.AddOrUpdate(x => x.Id,
                new BookImage() { Id = 1, BookId = 1, ImageURL = "D:\\GitHub\\my-service\\BegoniaService\\BegoniaService\\App_Data\\uploads\\pic1.jpeg" },
                new BookImage() { Id = 2, BookId = 2, ImageURL = "D:\\GitHub\\my-service\\BegoniaService\\BegoniaService\\App_Data\\uploads\\pic2.jpeg" },
                new BookImage() { Id = 3, BookId = 3, ImageURL = "D:\\GitHub\\my-service\\BegoniaService\\BegoniaService\\App_Data\\uploads\\pic3.jpeg" }
            );
            context.Orders.AddOrUpdate(x => x.Id,
                new Order() { Id = 1, BookId = 1, UserId = 1, StartDate = startDates[0], EndDate = endDates[0], ReturnDate = returnDates[0], State = "done" },
                new Order() { Id = 2, BookId = 2, UserId = 2, StartDate = startDates[1], EndDate = endDates[1], ReturnDate = returnDates[1], State = "overdue" },
                new Order() { Id = 3, BookId = 1, UserId = 3, StartDate = startDates[2], EndDate = endDates[2], ReturnDate = returnDates[2], State = "renting" },
                new Order() { Id = 4, BookId = 3, UserId = 2, StartDate = startDates[3], EndDate = endDates[3], ReturnDate = returnDates[3], State = "overdone" },
                new Order() { Id = 5, BookId = 1, UserId = 2, StartDate = startDates[4], EndDate = endDates[4], ReturnDate = returnDates[4], State = "done" }
            );
            context.Tickets.AddOrUpdate(x => x.Id,
                new Ticket() { Id = 1, OrderId = 5, Amount = 2.0, State="paid"}
            );
            context.Books.AddOrUpdate(x => x.Id,
        new Book()
        {
            Id = 1,
            Isbn = "978-7-115-39060-8",
            Name = "JavaScripet函数式编程",
            Type = "计算机",
            Press = "人民邮电出版社",
            Price = 49.00,
            Info = "本书内容全面，示例丰富，适合想要了解函数式编程的JavaScri程序员和学习JavaScript的函数式程序员阅读",
            Number = 120,
            Author = "Michael Fogus",
            BorrowNumber = 60,
        },
        new Book()
        {
            Id = 2,
            Isbn = "978-7-115-31008-8",
            Name = "编写可维护的JavaScripet",
            Type = "计算机",
            Press = "人民邮电出版社",
            Price = 55.00,
            Info = "本书适合前段开发工程师、JavaScript程序员和学习JavaScript编程的读者阅读，也适合项目负责人阅读",
            Number = 110,
            Author = "Nicholas C. Zakas",
            BorrowNumber = 45,
        },
        new Book()
        {
            Id = 3,
            Isbn = "978-7-508-35594-8",
            Name = "CSS权威指南",
            Type = "计算机",
            Press = "中国电力出版社",
            Price = 58.00,
            Info = "本书详细介绍了各个CSS属性，及属性间的相互作用",
            Number = 220,
            Author = "Nicholas C. Zakas",
            BorrowNumber = 100,
        },
        new Book()
        {
            Id = 4,
            Isbn = "978-7-508-35594-9",
            Name = "C#权威指南",
            Type = "计算机",
            Press = "中国电力出版社",
            Price = 28.80,
            Info = "本书详细介绍了C#",
            Number = 20,
            Author = "Eric A Meyer",
            BorrowNumber = 11,
        },
        new Book()
        {
            Id = 5,
            Isbn = "978-7-121-27657-6",
            Name = "ES6标准入门",
            Type = "计算机/Web开发/JavaScript",
            Press = "电子工业出版社",
            Price = 69,
            Info = "本书介绍了ES6的标准",
            Number = 165,
            Author = "阮一峰",
            BorrowNumber = 30,
        }
        );
        }
    }
}
