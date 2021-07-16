using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AzureTS.API.Additonal
{
    public class AtsAccountCreator
    {
        public static CloudStorageAccount GetValidatedStorageAccount()
        {
            CloudStorageAccount storageAccount;

            try
            {
                var connectionString = Startup.Configuration
                    .GetConnectionString(Constants.ConncetionStringName);

                storageAccount = CloudStorageAccount.Parse(connectionString);

                Log.Information("Conncetion String Format is OK!");
            }
            catch (FormatException)
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
