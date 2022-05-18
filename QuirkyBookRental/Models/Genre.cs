using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Genre name")] //That the title of the table column
        public string Name { get; set; }
    }
}