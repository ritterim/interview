using System;

namespace CustomerAnalytics.ConsoleApp
{
    public class CustomerDto
    {
        public uint Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime Registered { get; set; }
        public string FavoriteFruit { get; set; }
        public string EyeColor { get; set; }
        public string Balance { get; set; }
        public string Id { get; set; }
        public Name Name { get; set; }
    }
}
