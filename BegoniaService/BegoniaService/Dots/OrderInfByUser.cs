using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoniaService.Dots
{
    public class OrderInfByUser
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}