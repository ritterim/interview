using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimDevInterview.Models
{
  class Name
  {
    public string First { get; set; }
    public string Last { get; set; }
    public string FullName {
      get {
        return $"{First} {Last}";
      }
    }
    public string FullNameLastFirst {
      get {
        return $"{Last}, {First}";
      }
    }
  }
}
