using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudaGram
{
    public class ConnectionStringHandler
    {
        private string SqlConnectionString { get; set; }     
        private string AzureStorageConnectionString { get; set; }

        public void retrieveConnectionStrings()
        {
            SqlConnectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR");
            if(SqlConnectionString == null)
            {
                SqlConnectionString = "UseDevelopmentStorage";
            }

            AzureStorageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            if(AzureStorageConnectionString == null)
            {
                AzureStorageConnectionString = "UseDevelopmentStorage";
            }
            //TODO: Write connection strings to appropriate elements in the config file
        }
    }
}