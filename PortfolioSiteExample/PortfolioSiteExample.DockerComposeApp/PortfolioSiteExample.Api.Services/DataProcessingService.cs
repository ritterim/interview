using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSiteExample.Api.Services.Interfaces;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Shared.Enums;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PortfolioSiteExample.Api.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        static readonly object lockObj = new object();

        private readonly Context _context;
        private readonly Settings _settings;

        public DataProcessingService(Context context, IOptions<Settings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        public AnswerResponse GetAnswers(AnswerRequest answerRequest)
        {
            var answerResponse = new AnswerResponse();
            foreach (var question in answerRequest.Question)
            {
                answerResponse.Answer.Add(question, GetAnswer(question));
            }

            PersistAnswersIfEmpty(answerResponse);

            return answerResponse;
        }

        private string GetAnswer(Question question)
        {
            var result = string.Empty;

            InitializeRecordsIfEmpty();

            switch(question)
            {
                case Question.OverAge50:
                    var overAge50 = _context.Records.Where(x => x.Age > 50);
                    result = $"{overAge50.Count()}";
                    break;

                case Question.LastRegisteredActive:
                    var lastRegisteredActive = _context.Records.OrderByDescending(x => x.RegisteredDate).First(x => x.IsActive);
                    result = $"{lastRegisteredActive.LastName}, {lastRegisteredActive.FirstName}";
                    break;

                case Question.DistinctFavoriteFruitCounts:
                    var fruitList = new List<string>();
                    var distinctFavoriteFruits = _context.Records.Select(x => x.FavoriteFruit).Distinct().ToList();
                    foreach (var favoriteFruit in distinctFavoriteFruits)
                    {
                        fruitList.Add($"{favoriteFruit}: {_context.Records.Count(x => x.FavoriteFruit == favoriteFruit)}");
                    }

                    result = string.Join(",", fruitList);
                    break;

                case Question.MostCommonEyeColor:
                    var mostCommonEyeColor = _context.Records.GroupBy(x => x.EyeColor)
                                            .Select(group => new
                                            {
                                                EyeColor = group.Key,
                                                Count = group.Count()
                                            })
                                            .OrderByDescending(x => x.Count)
                                            .First().EyeColor;
                    result = $"{mostCommonEyeColor}";
                    break;

                case Question.TotalBalance:
                    var totalBalance = _context.Records.Sum(x => x.Balance);
                    result = string.Format(new CultureInfo("en-US"), "{0:c}", totalBalance);
                    break;

                case Question.UniqueIndividual:
                    var uniqueIndividual = _context.Records.First(x => x.Id == "5aabbca3e58dc67745d720b1");
                    result = $"{uniqueIndividual.LastName}, {uniqueIndividual.FirstName}";
                    break;
            }

            return result;
        }

        private void InitializeRecordsIfEmpty()
        {
            if (_context.Records.Count() == 0)
            {
                // Only permit one thread at a time
                lock (lockObj)
                {
                    // If the database table is still empty, then load and insert the records
                    if (_context.Records.Count() == 0)
                    {
                        var records = LoadRecords(_settings.DataFileName);
                        foreach (var record in records)
                        {
                            _context.Records.Add(new Record
                            {
                                Id = record.id,
                                FavoriteFruit = record.favoriteFruit,
                                Greeting = record.greeting,
                                Longitude = record.longitude,
                                Latitude = record.latitude,
                                RegisteredDate = record.RegisteredAsDateTime,
                                Address = record.address,
                                Phone = record.phone,
                                Email = record.email,
                                Company = record.company,
                                LastName = record.name.last,
                                FirstName = record.name.first,
                                EyeColor = record.eyeColor,
                                Age = record.age,
                                Balance = record.BalanceAsDecimal,
                                IsActive = record.isActive
                            });
                        }

                        _context.SaveChanges();
                    }
                }
            }
        }

        public void PersistAnswersIfEmpty(AnswerResponse answerResponse)
        {
            if (_context.Answers.Count() == 0)
            {
                // Only permit one thread at a time
                lock (lockObj)
                {
                    // If the database table is still empty, then load and insert the records
                    if (_context.Answers.Count() == 0)
                    {
                        foreach (var answer in answerResponse.Answer)
                        {
                            _context.Answers.Add(new Answer
                            {
                                QuestionEnum = answer.Key.ToString(),
                                AnswerText = answer.Value
                            });
                        }

                        _context.SaveChanges();
                    }
                }
            }
        }

        private static List<Shared.Objects.Record> LoadRecords(string dataFileName)
        {
            string rawJson = System.IO.File.ReadAllText(dataFileName);
            return JsonConvert.DeserializeObject<List<Shared.Objects.Record>>(rawJson);
        }
    }
}
