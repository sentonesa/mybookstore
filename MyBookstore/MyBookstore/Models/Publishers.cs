using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookstore.Models
{
    public class Publishers
    {
        [Key]
        public int ID { get; set; }

       

        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = "Up to 20 characters only!")]
        [Required(ErrorMessage = "Required!")]
        public string Name { get; set; }
    }
}