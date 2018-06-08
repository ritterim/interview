using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAnalytics.Tests
{
    [TestClass]
    public class CustomerAnalyzerTests
    {
        private CustomerAnalyzer sut;

        public CustomerAnalyzerTests()
        {
            sut = new CustomerAnalyzer();
        }

        [TestMethod]
        public void GetCountTheNumberOfPeopleOverAnAge_ThereAreNoRecordsPassed()
        {
            var result = sut.GetCountTheNumberOfPeopleOverAnAge(new List<Customer>(), 512);

            result.Should().Be(0);
        }

        [TestMethod]
        public void GetCountTheNumberOfPeopleOverAnAge_ThereIsOnePersonOverFiftyInTheSet()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Age = 51
                }
            };

            var result = sut.GetCountTheNumberOfPeopleOverAnAge(customers, 50);

            result.Should().Be(1);
        }

        [TestMethod]
        public void GetCountTheNumberOfPeopleOverAnAge_ThereIsOnePersonWhoIsFiftyInTheSet()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Age = 50
                }
            };

            var result = sut.GetCountTheNumberOfPeopleOverAnAge(customers, 50);

            result.Should().Be(0);
        }

        [TestMethod]
        public void GetCountTheNumberOfPeopleOverAnAge_ThereAreMultipleCustomersInTheList()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Age = 21
                },
                new Customer
                {
                    Age = 55
                },
                new Customer
                {
                    Age = 56
                },
                new Customer
                {
                    Age = 50
                }
            };

            var result = sut.GetCountTheNumberOfPeopleOverAnAge(customers, 21);

            result.Should().Be(3);
        }

        [TestMethod]
        public void GetNewestCustomerWhoIsStillActive_ThereAreNoCustomers()
        {
            var customers = new List<Customer>
            {
            };

            var result = sut.GetNewestCustomerWhoIsStillActive(customers);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetNewestCustomerWhoIsStillActive_ThereIsOnlyOneActiveCustomer()
        {
            var activeCustomer = new Customer
            {
                IsActive = true
            };

            var customers = new List<Customer>
            {
                new Customer
                {
                    IsActive = false,
                },
                activeCustomer
            };

            var result = sut.GetNewestCustomerWhoIsStillActive(customers);

            result.Should().BeSameAs(activeCustomer);
        }

        [TestMethod]
        public void GetNewestCustomerWhoIsStillActive_ThereIsNoActiveCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    IsActive = false
                }
            };

            var result = sut.GetNewestCustomerWhoIsStillActive(customers);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetNewestCustomerWhoIsStillActive_ShouldReturnNewestCustomer()
        {
            var mostRecentActiveCustomer = new Customer
            {
                IsActive = true,
                Registered = DateTime.UtcNow
            };

            var olderActiveCustomer = new Customer
            {
                IsActive = true,
                Registered = DateTime.UtcNow.AddDays(-1)
            };

            var customers = new List<Customer>
            {
                new Customer
                {
                    IsActive = false,
                },
                olderActiveCustomer,
                mostRecentActiveCustomer
            };

            var result = sut.GetNewestCustomerWhoIsStillActive(customers);

            result.Should().BeSameAs(mostRecentActiveCustomer);
        }

        [TestMethod]
        public void CountOfEachFavoriteFruit_ShouldReturnEmptyList()
        {
            var customers = new List<Customer>();

            var result = sut.CountOfEachFavoriteFruit(customers);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void CountOfEachFavoriteFruit_WithManyOfTheSameFruit()
        {
            var amazingFruit = RandomValue.String();

            var numberOfAmazingFruit = RandomValue.Int(200);

            var customers = RandomValue.List<Customer>(numberOfAmazingFruit);

            customers.ForEach(x => x.FavoriteFruit = amazingFruit);

            var result = sut.CountOfEachFavoriteFruit(customers);

            result.First().FavoriteFruit.Should().Be(amazingFruit);
            result.First().Count.Should().Be(numberOfAmazingFruit);
        }

        [TestMethod]
        public void CountOfEachFavoriteFruit_WithManyOfTheSameFruits()
        {
            var amazingFruit = RandomValue.String();
            var numberOfAmazingFruit = RandomValue.Int(200) + 1;
            var amazingFruitCustomers = RandomValue.List<Customer>(numberOfAmazingFruit);
            amazingFruitCustomers.ForEach(x => x.FavoriteFruit = amazingFruit);
            
            
            var terribleFruit = RandomValue.String();
            var numberOfPeopleWithPoorTaste = RandomValue.Int(2000) + 1;
            var peopleWithPoorTaste = RandomValue.List<Customer>(numberOfPeopleWithPoorTaste);
            peopleWithPoorTaste.ForEach(x => x.FavoriteFruit = terribleFruit);


            var customers = new List<Customer>();
            customers.AddRange(amazingFruitCustomers);
            customers.AddRange(peopleWithPoorTaste);

            //https://stackoverflow.com/a/3456788/2740086 
            var randomlyOrderedCustomers = customers.OrderBy(x => RandomValue.Int()).ToList();
            
            var result = sut.CountOfEachFavoriteFruit(randomlyOrderedCustomers);

            var amazingFruitResult = result.First(x => x.FavoriteFruit == amazingFruit);
            amazingFruitResult.Count.Should().Be(numberOfAmazingFruit);

            var terribleFruitResult = result.First(x => x.FavoriteFruit == terribleFruit);
            terribleFruitResult.Count.Should().Be(numberOfPeopleWithPoorTaste);
        }

        [TestMethod]
        public void MostCommonEyeColor_WithNonPassed()
        {
            var customers = new List<Customer>();

            var result = sut.MostCommonEyeColor(customers);

            result.Should().BeNull();
        }

        [TestMethod]
        public void MostCommonEyeColor_WithAllRandomData_ShouldReturnFirst()
        {
            var customers = RandomValue.List<Customer>();

            var result = sut.MostCommonEyeColor(customers);

            result.Should().Be(customers.First().EyeColor);
        }

        [TestMethod]
        public void MostCommonEyeColor_WithSomeSameness_ShouldReturnMax()
        {
            var popularEyeColor = RandomValue.String();

            var customers = new List<Customer>
            {
                new Customer
                {
                    EyeColor = RandomValue.String()
                },
                new Customer
                {
                    EyeColor = popularEyeColor
                },
                new Customer
                {
                    EyeColor = RandomValue.String()
                },
                new Customer
                {
                    EyeColor = popularEyeColor
                }
            };

            var result = sut.MostCommonEyeColor(customers);

            result.Should().Be(popularEyeColor);
        }

        [TestMethod]
        public void CalculateTotalBalance_NoCustomers()
        {
            var customers = new List<Customer>();

            var result = sut.CalculateTotalBalance(customers);

            result.Should().Be(0);
        }

        [TestMethod]
        public void CalculateTotalBalance_OneCustomer()
        {
            var customersBalance = 20.22254648m;

            var customers = new List<Customer>
            {
                new Customer
                {
                    Balance = customersBalance
                },
            };

            var result = sut.CalculateTotalBalance(customers);

            result.Should().Be(customersBalance);
        }

        [TestMethod]
        public void CalculateTotalBalance_MultipleCustomers()
        {

            var customers = new List<Customer>
            {
                new Customer
                {
                    Balance = .5m
                },
                new Customer
                {
                    Balance = 1.5m
                },
                new Customer
                {
                    Balance = 10.75m
                }
            };

            var result = sut.CalculateTotalBalance(customers);

            result.Should().Be(.5m + 1.5m + 10.75m);
        }

        [TestMethod]
        public void GetUsersFullName_NoUser()
        {

            var customers = new List<Customer>();

            var result = sut.GetUsersFullName(customers, "anId");

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetUsersFullName_ManyUsers()
        {
            var idToFind = RandomValue.String();
            var firstName = RandomValue.String();
            var lastName = RandomValue.String();

            var customers = RandomValue.List<Customer>();

            customers.Add(new Customer
            {
                Name = new Name
                {
                    First = firstName,
                    Last = lastName
                },
                Id = idToFind
            });

            customers.AddRange(RandomValue.List<Customer>());

            var result = sut.GetUsersFullName(customers, idToFind);

            result.Should().Be($"{lastName}, {firstName}");
        }
    }
}
