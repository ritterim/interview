using System.Collections.Generic;
using System.Linq;

namespace JsonInterrogator.Models
{
    public class AppViewModel
    {
        private IEnumerable<Person> _people;
        public AppViewModel(IEnumerable<Person> people)
        {
            this._people = people;
            this.BuildViewModel();
        }
        public int CountOverAge50 { get; private set; }
        public Person LastActivePerson { get; private set; }
        public IEnumerable<ReportViewModel> FruitReport { get; private set; }

        public decimal TotalBalance { get; private set; }

        private void BuildViewModel()
        {
            this.CountOverAge50 = this._people.Where(x => x.Age > 50).Count();
            this.LastActivePerson = this._people.Last(x => x.IsActive);
            this.FruitReport = this._people.GroupBy(x => x.FavoriteFruit)
                .Select(x => new ReportViewModel(x.Key, x.Count()));
            this.TotalBalance = this._people.Sum(x => x.ConvertedBalance);
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
