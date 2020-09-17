using System.Collections.Generic;
using System.Linq;
using interview.models;
using System;
using System.Globalization;

namespace interview.services
{
    public class DataService
    {
        private IEnumerable<Data> _data;

        public DataService(IEnumerable<Data> data)
        {
            _data = data;
        }

        public int GetIndividualsOverUnderAge(int age, bool over)
        {
            return _data.Where(d => over ? d.Age > age : d.Age < age)
                .Count();
        }

        public IEnumerable<Data> GetIndividuals(bool active)
        {
            return _data.Where(d => active ? d.IsActive == true : d.IsActive == false)
                .Select(d => d)
                .OrderByDescending(d => Convert.ToDateTime(d.Registered));
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

        public Data GetIndividualById(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            return _data.Where(d => d.Id == id)
                .FirstOrDefault();
        }

        private decimal ConvertStringCurrencyToDecimal(string amount)
        {
            Decimal dec;
            Decimal.TryParse(amount, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out dec);

            return dec;
        }
    }
}