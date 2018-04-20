using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BegoniaService.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double Amount { get; set; }
        [StringLength(20)]
        public string State { get; set; }
    }
}