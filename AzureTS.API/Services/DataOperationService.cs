using AzureTS.API.Models;
using Microsoft.Azure.Cosmos.Table;
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
    }
}
