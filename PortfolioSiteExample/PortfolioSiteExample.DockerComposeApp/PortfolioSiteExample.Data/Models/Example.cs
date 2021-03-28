using System.ComponentModel.DataAnnotations;

namespace PortfolioSiteExample.Data.Models
{
    public class Example
    {
        [Key]
        public int ExampleId { get; set; }

        public string Test { get; set; }
    }
}
