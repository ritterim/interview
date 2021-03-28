using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSiteExample.Api.Services.Interfaces;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Shared.Enums;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PortfolioSiteExample.Api.Services
{
    public class DataProcessingService : IDataProcessingService
    {
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
                    var builder = new StringBuilder();
                    var distinctFavoriteFruits = _context.Records.Select(x => x.FavoriteFruit).Distinct().ToList();
                    foreach (var favoriteFruit in distinctFavoriteFruits)
                    {
                        builder.Append($"{favoriteFruit}: {_context.Records.Count(x => x.FavoriteFruit == favoriteFruit)}");
                    }

                    result = builder.ToString();
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
                    result = $"{totalBalance.ToString("c")}";
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

        private static List<Shared.Objects.Record> LoadRecords(string dataFileName)
        {
            var records = new List<Shared.Objects.Record>();

            try
            {
                string rawJson = System.IO.File.ReadAllText(dataFileName);
                records = JsonConvert.DeserializeObject<List<Shared.Objects.Record>>(rawJson);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return records;
        }
    }
}
