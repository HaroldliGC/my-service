using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BegoniaService.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [StringLength(4000)]
        public string Review { get; set; }
        public DateTime Date { get; set; }
    }
}