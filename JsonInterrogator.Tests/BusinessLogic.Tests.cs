using System.Collections.Generic;
using JsonInterrogator.Models;
using NUnit.Framework;

namespace JsonInterrogator.Tests
{
    public class Tests
    {
        [Test]
        public void ReturnsCountOfPeopleOver50()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Age = 20 },
                new Person { Age = 50 },
                new Person { Age = 51 },
                new Person { Age = 52 },
            };

            // Act
            var result = people.GetCountOverAge50();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ReturnsLastActivePerson()
        {
            // Arrange
            var lastActivePerson = new Person { IsActive = true };

            var people = new List<Person> {
                new Person { IsActive = false },
                new Person { IsActive = false },
                new Person { IsActive = true },
                new Person { IsActive = false },
                lastActivePerson,
                new Person { IsActive = false },
            };

            // Act
            var result = people.GetLastActivePerson();

            // Assert
            Assert.AreSame(result, lastActivePerson);
        }
    }
}