using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudaGram
{
    public class ConnectionStringHandler
    {   
        public string GetAzureSqlConnectionString() //Gets connstring for Azure SQL if set as an environment variable. If it is not, default to development storage
        {
            string AzureSqlConnectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR") ?? "DefaultConnection";
            return AzureSqlConnectionString;           
        }

        public string GetAzureStorageConnectionString() //Gets connstring for Azure Storage if set as an environment variable. If it is not, default to development storage
        {
            string AzureStorageConnectionString = System.Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? "UseDevelopmentStorage=true";
            return AzureStorageConnectionString;
        }
    }
}