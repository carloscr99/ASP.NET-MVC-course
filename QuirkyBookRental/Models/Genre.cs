using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuirkyBookRental.Models
{
    public class Genre
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int Name { get; set; }
    }
}