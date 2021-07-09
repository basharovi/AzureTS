using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AzureTS.API
{
    public class AtsAccountCreator
    {
        private const string CONNECTION_STRING_NAME = "AzureTableStorage";

        public static CloudStorageAccount CreateAccount()
        {
            CloudStorageAccount storageAccount;

            try
            {
                var connectionString = Startup.Configuration.GetConnectionString(CONNECTION_STRING_NAME);
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (FormatException)
            {
                Log.Error("Invalid storage account information provided." +
                    " Please confirm the AccountName and AccountKey are valid in " +
                    "the appsettings.json file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Log.Error("Invalid storage account information provided." +
                     " Please confirm the AccountName and AccountKey are valid in " +
                     "the appsettings.json file - then restart the application.");
                throw;
            }

            return storageAccount;
        }
    }
}
