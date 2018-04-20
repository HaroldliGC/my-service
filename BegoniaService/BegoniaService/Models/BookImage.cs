using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoniaService.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ImageURL { get; set; }
    }
}