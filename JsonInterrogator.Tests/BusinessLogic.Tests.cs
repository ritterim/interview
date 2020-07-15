using System.Collections.Generic;
using System.Linq;
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
            var people = new List<Person> {
                new Person { Age = RandomTestValues.RandomValue.Int(50) },
                new Person { Age = RandomTestValues.RandomValue.Int(50) },
                new Person { Age = RandomTestValues.RandomValue.Int(200, 50) },
                new Person { Age = RandomTestValues.RandomValue.Int(200, 50) },
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
                new Person { IsActive = RandomTestValues.RandomValue.Bool() },
                new Person { IsActive = RandomTestValues.RandomValue.Bool() },
                new Person { IsActive = RandomTestValues.RandomValue.Bool() },
                new Person { IsActive = RandomTestValues.RandomValue.Bool() },
                lastActivePerson,
                new Person { IsActive = false },
            };

            // Act
            var result = people.GetLastActivePerson();

            // Assert
            Assert.AreSame(result, lastActivePerson);
        }

        [Test]
        public void ReturnsAFruitReportWithCounts()
        {
            // Arrange
            const string KIWI = "kiwi";
            const string ORANGE = "orange";
            const string GRAPES = "grapes";

            var people = new List<Person> {
                new Person { FavoriteFruit = KIWI},
                new Person { FavoriteFruit = KIWI},
                new Person { FavoriteFruit = KIWI},
                new Person { FavoriteFruit = ORANGE},
                new Person { FavoriteFruit = GRAPES},
                new Person { FavoriteFruit = KIWI},
                new Person { FavoriteFruit = ORANGE},
            };

            // Act
            var result = people.GetFruitReport();

            // Assert
            Assert.AreEqual(4, result.Single(x => x.Description == KIWI).Count);
            Assert.AreEqual(2, result.Single(x => x.Description == ORANGE).Count);
            Assert.AreEqual(1, result.Single(x => x.Description == GRAPES).Count);
        }

        [Test]
        public void ReturnCommonEyeColor()
        {
            // Arrange
            string common = RandomTestValues.RandomValue.String();

            var people = new List<Person> {
                new Person { EyeColor = common },
                new Person { EyeColor = common },
                new Person { EyeColor = common },
                new Person { EyeColor = RandomTestValues.RandomValue.String() },
                new Person { EyeColor = common },
                new Person { EyeColor = RandomTestValues.RandomValue.String() },
                new Person { EyeColor = common },
                new Person { EyeColor = RandomTestValues.RandomValue.String() },
                new Person { EyeColor = common },
                new Person { EyeColor = common },
            };

            // Act
            var result = people.GetCommonEyeColor();

            // Assert
            Assert.AreEqual(common, result);
        }

        [Test]
        public void ReturnsTotalBalance()
        {
            // Arrange
            var people = new List<Person> {
                new Person { Balance = "$1" },
                new Person { Balance = "$10" },
                new Person { Balance = "$13" },
            };

            // Act
            var result = people.GetTotalBalance();

            // Assert
            Assert.AreEqual(24m, result);
        }

        [Test]
        public void Test()
        {
            // Arrange
            string selected = RandomTestValues.RandomValue.String();
            var expectedPerson = new Person { Id = selected, Name = new Name { First = "First", Last = "Last" } };

            var people = new List<Person> {
                new Person { Id = RandomTestValues.RandomValue.String() },
                new Person { Id = RandomTestValues.RandomValue.String() },
                new Person { Id = RandomTestValues.RandomValue.String() },
                expectedPerson,
                new Person { Id = RandomTestValues.RandomValue.String() },
            };

            // Act
            var result = people.GetFullNameById(selected);

            // Assert
            Assert.AreEqual($"{expectedPerson.Name.Last}, {expectedPerson.Name.First}", result);
        }
    }
}