using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BegoniaService.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Isbn must be filled in")]
        [StringLength(20)]
        public string Isbn { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "The book name must be filled in")]
        public string Name { get; set; }
        [StringLength(200)]
        public string Type { get; set; }
        [StringLength(200)]
        public string Press { get; set; }
        [StringLength(100)]
        public string Author { get; set; }
        [StringLength(4000)]
        public string Info { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
        public int BorrowNumber { get; set; }

    }
}