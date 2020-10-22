using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fund.Web.Models
{
    public class Group
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Requerido")]
        public string name { get; set; }
       

    }
}
