using FluentAssertions;
using RandomTestValues;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CustomerAnalytics.Tests
{
    public class CustomerAnalyserTests
    {
        private CustomerAnalyser sut;

        public CustomerAnalyserTests()
        {
            sut = new CustomerAnalyser();
        }

        [Fact]
        public void GetCountTheNumberOfPeopleOverAnAge_ThereAreNoRecordsPassed()
        {
            var result = sut.GetCountTheNumberOfPeopleOverAnAge(new List<Customer>(), 512);

            result.Should().Be(0);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
        public void GetNewestCustomerWhoIsStillActive_ThereAreNoCustomers()
        {
            var customers = new List<Customer>();

            var result = sut.GetNewestCustomerWhoIsStillActive(customers);

            result.Should().BeNull();
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
        public void CountOfEachFavoriteFruit_ShouldReturnEmptyList()
        {
            var customers = new List<Customer>();

            var result = sut.CountOfEachFavoriteFruit(customers);

            result.Should().BeEmpty();
        }

        [Fact]
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

        [Fact]
        public void CountOfEachFavoriteFruit_WithManyOfTheSameFruit_DifferentCasing()
        {
            var amazingFruit = RandomValue.String().ToLowerInvariant();

            var numberOfAmazingFruit = RandomValue.Int(200);

            var customers = RandomValue.List<Customer>(numberOfAmazingFruit);

            customers.ForEach(x => x.FavoriteFruit = amazingFruit);
            customers.Add(new Customer
            {
                FavoriteFruit = amazingFruit.ToUpperInvariant()
            });

            var result = sut.CountOfEachFavoriteFruit(customers);

            result.First().FavoriteFruit.Should().Be(amazingFruit);
            result.First().Count.Should().Be(numberOfAmazingFruit + 1);
        }

        [Fact]
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

        [Fact]
        public void MostCommonEyeColor_WithNonPassed()
        {
            var customers = new List<Customer>();

            var result = sut.MostCommonEyeColor(customers);

            result.Should().BeNull();
        }

        [Fact]
        public void MostCommonEyeColor_WithAllRandomData_ShouldReturnFirst()
        {
            var customers = RandomValue.List<Customer>();

            var result = sut.MostCommonEyeColor(customers);

            result.Should().Be(customers.First().EyeColor);
        }

        [Fact]
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

        [Fact]
        public void MostCommonEyeColor_WithSomeSameness_ShouldReturnMax_WithDifferentAz()
        {
            var popularEyeColor = RandomValue.String().ToLowerInvariant();
            var oneLessPopularEyeColor = RandomValue.String();

            var customers = new List<Customer>
            {
                new Customer
                {
                    EyeColor = oneLessPopularEyeColor
                },
                new Customer
                {
                    EyeColor = popularEyeColor
                },
                new Customer
                {
                    EyeColor = oneLessPopularEyeColor
                },
                new Customer
                {
                    EyeColor = popularEyeColor
                },
                new Customer
                {
                    EyeColor = oneLessPopularEyeColor
                },
                new Customer
                {
                    EyeColor = popularEyeColor.ToUpperInvariant()
                },
                new Customer
                {
                    EyeColor = popularEyeColor.ToUpperInvariant()
                }
            };

            var result = sut.MostCommonEyeColor(customers);

            result.Should().Be(popularEyeColor);
        }

        [Fact]
        public void CalculateTotalBalance_NoCustomers()
        {
            var customers = new List<Customer>();

            var result = sut.CalculateTotalBalance(customers);

            result.Should().Be(0);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void GetUsersFullName_NoUser()
        {

            var customers = new List<Customer>();

            var result = sut.GetUsersFullName(customers, "anId");

            result.Should().BeNull();
        }

        [Fact]
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

        [Fact]
        public void GetUsersFullName_ManyUsers_HandleCasing()
        {
            var idToFind = RandomValue.String().ToUpperInvariant();
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

            var result = sut.GetUsersFullName(customers, idToFind.ToLowerInvariant());

            result.Should().Be($"{lastName}, {firstName}");
        }
    }
}
