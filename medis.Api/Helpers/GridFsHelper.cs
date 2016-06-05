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

        public async Task<ObjectId> UploadFromStreamAsync(string gfsname, Stream source,string filename, string contentType, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions {
                BucketName = bucketName.ToString()
            });

            var options = new GridFSUploadOptions {
                Metadata = new BsonDocument {
                    { "filename", filename },
                    { "contentType", contentType }
                }
            };

            return await bucket.UploadFromStreamAsync(gfsname, source, options);
        }

        public async Task DownloadToStreamByNameAsync(string gfsname, Stream source, MediaTypeEnum bucketName) {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });

            await bucket.DownloadToStreamByNameAsync(gfsname, source);
        }

        public async Task DownloadToStreamAsync(ObjectId id, Stream source, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });
            
            await bucket.DownloadToStreamAsync(id, source);
        }

        public async Task<GridFSDownloadStream> OpenDownloadStreamAsync(ObjectId id, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });

            return await bucket.OpenDownloadStreamAsync(id);
        }

        public async Task<GridFSDownloadStream> OpenDownloadStreamByNameAsync(string name, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });

            return await bucket.OpenDownloadStreamByNameAsync(name);
        }

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });

            return await bucket.DownloadAsBytesAsync(id);
        }

        public async Task<byte[]> DownloadAsBytesByNameAsync(string gfsname, MediaTypeEnum bucketName) {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });

            return await bucket.DownloadAsBytesByNameAsync(gfsname);
        }

        public async Task<bool> FileExistsAsync(string gfsname, MediaTypeEnum bucketName)
        {
            var bucket = new GridFSBucket(_db, new GridFSBucketOptions
            {
                BucketName = bucketName.ToString()
            });
            
            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, gfsname);
            var fileInfo = await bucket.FindAsync(filter);

            return fileInfo.Any();
        }
    }
}