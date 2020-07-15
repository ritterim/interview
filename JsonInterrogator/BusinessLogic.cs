using System.Collections.Generic;
using System.Linq;
using JsonInterrogator.Models;

namespace JsonInterrogator
{
    public static class BusinessLogic
    {
        public static int GetCountOverAge50(this IEnumerable<Person> people)
        {
            return people.Where(x => x.Age > 50).Count();
        }
    }
}
