using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudaGram
{
    public class ConnectionStringHandler
    {   
        public string GetAzureSqlConnectionString()
        {
            string AzureSqlConnectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR");
            if (AzureSqlConnectionString == null) //Default to development storage if the environment variables are not found
            {
                return "UseDevelopmentStorage=true";
            }

            return AzureSqlConnectionString;
        }

        public string GetAzureStorageConnectionString()
        {
            string AzureStorageConnectionString = System.Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            if (AzureStorageConnectionString == null) //Default to development storage if the environment variables are not found
            {
                return "UseDevelopmentStorage=true";
            }

            return AzureStorageConnectionString;
        }
    }
}