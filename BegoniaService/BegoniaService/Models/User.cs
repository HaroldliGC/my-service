using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BegoniaService.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Account must be filled in")]
        [StringLength(50)]
        public string Account { get; set; }
        [Required(ErrorMessage = "The password must be filled in")]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        [StringLength(20)]
        public string Gender { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string License { get; set; }
        [StringLength(20)]
        public string Identity { get; set; }
    }
}