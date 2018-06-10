using System.Collections.Generic;
using System.Linq;

namespace CustomerAnalytics
{
    public class CustomerAnalyser
    {
        public int GetCountTheNumberOfPeopleOverAnAge(ICollection<Customer> customers, uint ageLimit)
        {
            return customers.Count(x => x.Age > ageLimit);
        }

        public Customer GetNewestCustomerWhoIsStillActive(ICollection<Customer> customers)
        {
            return customers.OrderByDescending(x => x.Registered).FirstOrDefault(x => x.IsActive);
        }

        public ICollection<FavoriteFruitFrequency> CountOfEachFavoriteFruit(ICollection<Customer> customers)
        {
            return customers
                .GroupBy(x => x.FavoriteFruit.ToLowerInvariant())
                .Select(x => new FavoriteFruitFrequency { Count = x.Count(), FavoriteFruit = x.Key })
                .ToList();
        }

        public string MostCommonEyeColor(ICollection<Customer> customers)
        {
            if (customers.Count == 0)
            {
                return null;
            }

            return customers
                .GroupBy(x => x.EyeColor.ToLowerInvariant())
                .OrderByDescending(x => x.Count())
                .First()
                .First()
                .EyeColor;
        }

        public decimal CalculateTotalBalance(ICollection<Customer> customers)
        {
            return customers.Sum(x => x.Balance);
        }

        public string GetUsersFullName(ICollection<Customer> customers, string id)
        {
            var customer = customers.FirstOrDefault(x => x.Id.ToLowerInvariant() == id.ToLowerInvariant());

            if (customer == null)
            {
                return null;
            }

            return $"{customer.Name.Last}, {customer.Name.First}";
        }
    }
}
