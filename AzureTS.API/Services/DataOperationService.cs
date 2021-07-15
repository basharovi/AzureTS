using AzureTS.API.Additonal;
using AzureTS.API.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTS.API.Services
{
    public class DataOperationService
    {
        private readonly CloudTable _cloudTable;

        public DataOperationService(string tableName)
        {
            var csAccount = AtsAccountCreator.CreateAccount();
            var tableClient = csAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference(tableName);
        }

        public dynamic GetAll(string? name)
        {
            TableContinuationToken token = null;
            var entities = new List<SoloEntity>();

            var tableQuery = new TableQuery<SoloEntity>();

            if (!string.IsNullOrWhiteSpace(name))
                tableQuery = tableQuery.Where(TableQuery.GenerateFilterCondition(
                    "name", QueryComparisons.Equal, name));

            do
            {
                var queryResult = _cloudTable.ExecuteQuerySegmented(tableQuery, token);

                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;

            } while (token != null);

            return entities;
        }
    }
}
