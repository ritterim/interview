using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CustomerAnalytics.ConsoleApp
{
    public static class DtoToDomainMapper
    {
        public static Customer MapToCustomer(this CustomerDto customerDto)
        {
            return new Customer
            {
                Age = customerDto.Age,
                IsActive = customerDto.IsActive,
                Registered = customerDto.Registered,
                FavoriteFruit = customerDto.FavoriteFruit,
                EyeColor = customerDto.EyeColor,
                Balance = ParseBalance(customerDto.Balance),
                Id = customerDto.Id,
                Name = customerDto.Name
            };
        }

        public static ICollection<Customer> MapToCustomers(this ICollection<CustomerDto> customerDtos)
        {
            return customerDtos.Select(x => x.MapToCustomer()).ToList();
        }

        private static decimal ParseBalance(string balanceString)
        {
            return decimal.Parse(balanceString, NumberStyles.Currency);
        }
    }
}
