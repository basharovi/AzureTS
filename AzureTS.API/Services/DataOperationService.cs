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

        public List<SoloEntity> GetAll()
        {
            TableContinuationToken token = null;

            var entities = new List<SoloEntity>();
            var tableQuery = new TableQuery<SoloEntity>();

            do
            {
                var queryResult = _cloudTable.ExecuteQuerySegmented(tableQuery, token);

                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;

            } while (token != null);

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

            //
            var entities_123 = _cloudTable.ExecuteQuery(new TableQuery<SoloEntity>()).Where(x => x.Name == name).ToList();

            //

            return entities;
        }
    }
}
