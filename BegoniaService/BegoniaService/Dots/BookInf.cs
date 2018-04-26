using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoniaService.Dots
{
    public class BookInf
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Press { get; set; }
        public string Author { get; set; }
        public string Info { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
        public int BorrowNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}