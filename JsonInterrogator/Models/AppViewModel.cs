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

        private void BuildViewModel()
        {
            this.CountOverAge50 = this._people.Where(x => x.Age > 50).Count();
        }
    }
}
