using System.Collections.Generic;

namespace JsonInterrogator.Models
{
    public class AppViewModel
    {
        const string SELECTED_ID = "5aabbca3e58dc67745d720b1";
        private IEnumerable<Person> _people;
        public AppViewModel(IEnumerable<Person> people)
        {
            this._people = people;
        }
        public int CountOverAge50 => _people.GetCountOverAge50();
        public Person LastActivePerson => _people.GetLastActivePerson();
        public IEnumerable<ReportViewModel> FruitReport => _people.GetFruitReport();
        public string CommonEyeColor => _people.GetCommonEyeColor();
        public decimal TotalBalance => _people.GetTotalBalance();
        public string FullNameById => _people.GetFullNameById(SELECTED_ID);

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
