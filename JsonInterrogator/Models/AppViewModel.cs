using System.Collections.Generic;
using System.Linq;

namespace JsonInterrogator.Models
{
    public class AppViewModel
    {
        const string SELECTED_ID = "5aabbca3e58dc67745d720b1";
        private IEnumerable<Person> _people;
        public AppViewModel(IEnumerable<Person> people)
        {
            this._people = people;
            this.BuildViewModel();
        }
        public int CountOverAge50 { get; private set; }
        public Person LastActivePerson { get; private set; }
        public IEnumerable<ReportViewModel> FruitReport { get; private set; }
        public string CommonEyeColor { get; private set; }
        public decimal TotalBalance { get; private set; }
        public string FullNameById { get; private set; }

        private void BuildViewModel()
        {
            this.CountOverAge50 = this._people.GetCountOverAge50();
            this.LastActivePerson = this._people.Last(x => x.IsActive);
            this.FruitReport = this._people.GroupBy(x => x.FavoriteFruit)
                .Select(x => new ReportViewModel(x.Key, x.Count()));
            this.CommonEyeColor = this._people.GroupBy(x => x.EyeColor).Select(x => new { EyeColor = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count).First().EyeColor;
            this.TotalBalance = this._people.Sum(x => x.ConvertedBalance);
            this.FullNameById = this._people.Single(x => x.Id == SELECTED_ID).Name.FullName;
        }
    }

    public class ReportViewModel
    {
        public ReportViewModel(string key, int count)
        {
            this.Description = key;
            this.Count = count;
        }

        public string Description { get; private set; }
        public int Count { get; private set; }
    }
}
