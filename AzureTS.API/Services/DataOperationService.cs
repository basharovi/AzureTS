using AzureTS.API.Additonal;
using AzureTS.API.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureTS.API.Services
{
    public class DataOperationService
    {
        private readonly CloudTable _cloudTable;
        private readonly CloudTableClient _tableClient;

        public DataOperationService()
        {
            var csAccount = AtsAccountCreator.GetValidatedStorageAccount();
            _tableClient = csAccount.CreateCloudTableClient();
        }

        public DataOperationService(string tableName) : this()
        {
            _cloudTable = _tableClient.GetTableReference(tableName);
        }

        public List<SoloEntity> GetAll(string? name, string? fromDate, string? toDate)
        {
            TableContinuationToken token = null;
            var entities = new List<SoloEntity>();

            var tableQuery = GenerateTheTableQuery(name,
                GetFormattedTime(fromDate), GetFormattedTime(toDate));

            do
            {
                var queryResult = _cloudTable.ExecuteQuerySegmented(tableQuery, token);

                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;

            } while (token != null);

            return entities;
        }

        public List<string> GetAllNames()
        {
            var result = GetAll(null, null, null)
                .GroupBy(x => x.Name)
                .Select(x => x.FirstOrDefault())
                .Select(x => x.Name)
                .ToList();

            return result;
        }

        private static TableQuery<SoloEntity> GenerateTheTableQuery(string? name, string? fromDate, string? toDate)
        {
            var tableQuery = new TableQuery<SoloEntity>();

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(fromDate))
            {
                var filter = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("name", QueryComparisons.Equal, name),
                    TableOperators.And,
                    TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("time", QueryComparisons.GreaterThanOrEqual, fromDate),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("time", QueryComparisons.LessThanOrEqual, toDate)));

                tableQuery = tableQuery.Where(filter);
            }
            else if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(fromDate))
            {
                string filter;

                if (!string.IsNullOrWhiteSpace(name))
                    filter = TableQuery.GenerateFilterCondition("name", QueryComparisons.Equal, name);
                else
                {
                    filter = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("time", QueryComparisons.GreaterThanOrEqual, fromDate),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("time", QueryComparisons.LessThanOrEqual, toDate));
                }

                tableQuery = tableQuery.Where(filter);
            }

            return tableQuery;
        }

        private static string GetFormattedTime(string dateTime)
        {
            if (string.IsNullOrWhiteSpace(dateTime))
                return dateTime;

            var formattedDateTime = Convert.ToDateTime(dateTime).
                                        ToString(Constants.DateTimeFormat);

            return formattedDateTime;
        }
    }
}
