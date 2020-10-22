using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fund.Web.Models
{
    public class Person
    {
        [Key]
        public int id{ get; set; }
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        [Required]
        public int idgroup { get; set; }
        [NotMapped]
        public string grupo { get; set; }


    }
}
