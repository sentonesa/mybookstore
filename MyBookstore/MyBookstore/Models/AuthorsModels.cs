using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace MyBookstore.Models
{
    public class AuthorsModels
    {
        [Key]
        public int ID { get; set; }

        [Display(Name ="LastName")]
        [MaxLength(40, ErrorMessage = "Up to 40 characters only!")]
        [Required(ErrorMessage ="Required!")]
        public string LastName { get; set; }

        [Display(Name = "FirstName")]
        [MaxLength(20, ErrorMessage = "Up to 20 characters only!")]
        [Required(ErrorMessage = "Required!")]
        public string FirstName { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(12, ErrorMessage = "Up to 12 characters only!")]
        [Required(ErrorMessage = "Required!")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [MaxLength(40, ErrorMessage = "Up to 40 characters only!")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [MaxLength(20, ErrorMessage = "Up to 20 characters only!")]
        public string City { get; set; }

        [Display(Name = "State")]
        [MaxLength(2, ErrorMessage = "Up to 2 characters only!")]
        public string State { get; set; }

        [Display(Name = " Zip Code")]
        [MaxLength(5, ErrorMessage = "Up to 5 characters only!")]
        public string Zip { get; set; }


    }
}