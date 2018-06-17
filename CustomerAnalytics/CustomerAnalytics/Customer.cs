using System;

namespace CustomerAnalytics
{
    public class Customer
    {
        public uint Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime Registered { get; set; }
        public string FavoriteFruit { get; set; }
        public string EyeColor { get; set; }
        public decimal Balance { get; set; }
        public string Id { get; set; }
        public Name Name { get; set; }
    }
}