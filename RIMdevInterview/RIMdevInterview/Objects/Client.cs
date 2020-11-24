using System;
using System.Collections.Generic;
using System.Text;

namespace RIMdevInterview.Objects
{
    class Client
    {
        public FullName Name { get; set; }
        public String Id { get; set; }
        public String FavoriteFruit { get; set; }
        public String Greeting { get; set; }
        public String Longitude { get; set; }
        public String Latitude { get; set; }
        public DateTime Registered { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Company { get; set; }
        public String EyeColor { get; set; }
        public String Balance { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }

        public Client()
        {
            this.Address = null;
            this.Age = 0;
            this.Balance = null;
            this.Company = null;
            this.Email = null;
            this.EyeColor = null;
            this.FavoriteFruit = null;
            this.Greeting = null;
            this.Id = null;
            this.IsActive = false;
            this.Latitude = null;
            this.Longitude = null;
            this.Name = new FullName();
            this.Phone = null;
            this.Registered = new DateTime();
        }
    }
}
