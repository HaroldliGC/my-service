using BegoniaService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoniaService.Dots
{
    public class OrderReturnMessage
    {
        public string message { get; set; }
        public Order order { get; set; }
        public string type { get; set; }
    }
}