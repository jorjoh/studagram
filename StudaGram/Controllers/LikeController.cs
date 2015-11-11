using Microsoft.WindowsAzure.Storage.Table;
using StudaGram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudaGram.Controllers
{
    public class LikeController : ApiController
    {
        [HttpPost, Route("Api/Like/{id}")]
        public IHttpActionResult Post (string id)
        {
            BlobStorageServices _blobStorageService = new BlobStorageServices();
            var TableClient = _blobStorageService.GetAzureTableAccount();
            var Table = _blobStorageService.GetTable(TableClient);

            var query = new TableQuery<UploadedImage>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Images"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id)));

            var result = Table.ExecuteQuery(query).Single();
            result.Likes = result.Likes+1;

            var UpdateOperation = TableOperation.InsertOrReplace(result);
            Table.Execute(UpdateOperation);

            return Ok(new { Likes = result.Likes });

            
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
