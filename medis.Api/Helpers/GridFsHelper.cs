using medis.Api.Interfaces.Helpers;
using medis.Api.Enums;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Driver;
using medis.Api.Infrastructure;
using MongoDB.Driver.GridFS;

namespace medis.Api.Helpers
{
    public class GridFsHelper : IGridFsHelper
    {
        private readonly IMongoDatabase _db;

        public GridFsHelper()
        {
            _db = DBInstance.Database;
        }

        public async Task<ObjectId> UploadFromStreamAsync(string filename, Stream source, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions {
                BucketName = bucketName.ToString()
            });

            return await bucket.UploadFromStreamAsync(filename, source);
        }
    }
}