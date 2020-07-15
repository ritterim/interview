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
    }
}