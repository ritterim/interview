using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioSiteExample.Data.Models
{
    public class Record
    {
        [Key]
        public string Id { get; set; }

        public string FavoriteFruit { get; set; }

        public string Greeting { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public DateTime RegisteredDate { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string EyeColor { get; set; }

        public int Age { get; set; }

        public decimal Balance { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
