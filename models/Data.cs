using System;

namespace interview.Models
{
    public class Data
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Greeting { get; set; }
        public string FavoriteFruit { get; set; }
        public Name Name { get; set; }
    }

    public class Name
    {
        public string Last { get; set; }
        public string First { get; set; }
    }
}