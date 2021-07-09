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

        public DataOperationService(string tableName)
        {
            var csAccount = AtsAccountCreator.CreateAccount();
            var tableClient = csAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference(tableName);
        }

        public List<SoloEntity> GetAll()
        {
            var entities = _cloudTable.ExecuteQuery(new TableQuery<SoloEntity>()).ToList();

            return entities;
        }

        public dynamic GetFiltered(string name, DateTime? date = null)
        {
            var query = new TableQuery<SoloEntity>().Where(TableQuery.GenerateFilterCondition(
                                    "name",
                                    QueryComparisons.Equal,
                                    name
                                  ));

            var entities = _cloudTable.ExecuteQuerySegmented(query, null);

            return entities;
        }
    }
}
