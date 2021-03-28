using System;

namespace PortfolioSiteExample.Shared.Objects
{
    // Converted with https://json2csharp.com
    public class Name
    {
        public string last { get; set; }
        public string first { get; set; }
    }

    public class Record
    {
        public string favoriteFruit { get; set; }
        public string greeting { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string registered { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public Name name { get; set; }
        public string eyeColor { get; set; }
        public int age { get; set; }
        public string balance { get; set; }
        public bool isActive { get; set; }
        public string id { get; set; }

        public DateTime RegisteredAsDateTime
        { 
            get
            {
                return DateTime.TryParse(this.registered, out DateTime result) ? result : DateTime.MinValue;
            }
        }

        public decimal BalanceAsDecimal
        {
            get
            {
                return decimal.TryParse(this.balance.Replace("$", string.Empty).Replace(",", string.Empty), out decimal result) ? result : 0;
            }
        }
    }
}
