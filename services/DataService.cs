using System.Collections.Generic;
using System.Linq;
using interview.models;
using System;
using System.Globalization;

namespace interview.services
{
    public class DataService
    {
        private List<Data> _data;

        public DataService(IEnumerable<Data> data)
        {
            _data = data.ToList();
        }

        public int GetIndividualsOverFifty()
        {
            return _data.Where(d => d.Age > 50)
                .Count();
        }

        public Data LastRegisteredStillActive()
        {
            return _data.Where(d => d.IsActive == true)
                .Select(d => d)
                .OrderByDescending(d => Convert.ToDateTime(d.Registered))
                .FirstOrDefault();
        }

        public IEnumerable<KeyValuePair<string, int>> GetCountsOfEachFavoriteFruit()
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            var counts = _data.GroupBy(d => d.FavoriteFruit)
                .Select(d => new
                {
                    Fruit = d.Key,
                    Count = d.Select(x => x.FavoriteFruit).Count()
                }).OrderByDescending(d => d.Count);

            if (!counts.Any())
                return result;

            foreach (var item in counts)
            {
                result.Add(new KeyValuePair<string, int>(item.Fruit, item.Count));
            }

            return result;
        }

        public string GetMostCommonEyeColor()
        {
            var countEyeColors = _data.GroupBy(d => d.EyeColor)
                .Select(d => new
                {
                    Color = d.Key,
                    Count = d.Select(x => x.EyeColor).Count()
                }).OrderByDescending(d => d.Count).Take(1);

            if (countEyeColors.Any())
            {
                return countEyeColors.FirstOrDefault()
                    .Color;
            }
            else 
            {
                return String.Empty;
            }
        }

        public decimal GetTotalBalanceAllIndividuals()
        {
            var test = _data.Select(d => ConvertStringCurrencyToDecimal(d.Balance))
                .Sum();

            return test;
        }

        public Name GetIndividualById(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            return _data.Where(d => d.Id == id)
                .Select(d => new Name() {
                    First = d.Name.First,
                    Last = d.Name.Last
                }).FirstOrDefault();
        }

        private decimal ConvertStringCurrencyToDecimal(string amount)
        {
            Decimal dec;
            Decimal.TryParse(amount, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out dec);

            return dec;
        }
    }
}