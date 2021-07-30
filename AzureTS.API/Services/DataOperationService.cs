using AzureTS.API.Additonal;
using AzureTS.API.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;

namespace AzureTS.API.Services
{
    public class DataOperationService
    {
        private readonly CloudTable _cloudTable;

        public DataOperationService(string tableName)
        {
            var csAccount = AtsAccountCreator.GetValidatedStorageAccount();
            var tableClient = csAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference(tableName);
        }

        public List<SoloEntity> GetAll(string? name, string? dateTime)
        {
            TableContinuationToken token = null;
            var entities = new List<SoloEntity>();

            var time = string.Empty;

            if (!string.IsNullOrWhiteSpace(dateTime))
                time = Convert.ToDateTime(dateTime).ToString("yyyy-MM-ddTHH:mm:ssK");

            var tableQuery = GenerateTheTableQuery(name, time);

            do
            {
                var queryResult = _cloudTable.ExecuteQuerySegmented(tableQuery, token);

                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;

            } while (token != null);

            return entities;
        }

        private static TableQuery<SoloEntity> GenerateTheTableQuery(string? name, string? dateTime)
        {
            var tableQuery = new TableQuery<SoloEntity>();

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(dateTime))
            {
                var filter = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("name", QueryComparisons.Equal, name),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("time", QueryComparisons.Equal, dateTime));

                tableQuery = tableQuery.Where(filter);
            }
            else if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(dateTime))
            {
                string filter;

                if (dateTime == null)
                    filter = TableQuery.GenerateFilterCondition("name", QueryComparisons.Equal, name);
                else
                    filter = TableQuery.GenerateFilterCondition("time", QueryComparisons.Equal, dateTime);

                tableQuery = tableQuery.Where(filter);
            }

            return tableQuery;
        }
    }
}
