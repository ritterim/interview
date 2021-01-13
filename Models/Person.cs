using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimDevInterview.Models
{
  class Person
  {
    public string ID { get; set; }
    public bool IsActive { get; set; }
    public string Balance { get; set; }
    public decimal BalanceAsDecimal {
      get {
        return decimal.Parse(Balance, System.Globalization.NumberStyles.Currency);
      }
    }
    public int Age { get; set; }
    public string EyeColor { get; set; }
    public Name Name { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime Registered { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Greeting { get; set; }
    public string FavoriteFruit { get; set; }
  }
}
