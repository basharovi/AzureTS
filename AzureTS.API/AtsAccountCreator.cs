using System;
using Microsoft.Azure.Cosmos.Table;

namespace AzureTS.API
{
    public class AtsAccountCreator
    {
        public static CloudStorageAccount CreateAccount(string connectionString)
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided." +
                    " Please confirm the AccountName and AccountKey are valid in " +
                    "the appsettings.json file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided." +
                     " Please confirm the AccountName and AccountKey are valid in " +
                     "the appsettings.json file - then restart the application.");
                throw;
            }

            return storageAccount;
        }
    }
}
