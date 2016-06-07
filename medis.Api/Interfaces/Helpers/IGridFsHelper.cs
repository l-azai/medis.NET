using medis.Api.Enums;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Helpers
{
    public interface IGridFsHelper
    {
        Task<ObjectId> UploadFromStreamAsync(string gfsname, Stream source, string filename, MediaTypeEnum bucketName);

        Task DownloadToStreamByNameAsync(string gfsname, Stream source, MediaTypeEnum bucketName);

        Task DownloadToStreamAsync(ObjectId id, Stream source, MediaTypeEnum bucketName);

        Task<GridFSDownloadStream> OpenDownloadStreamAsync(ObjectId id, MediaTypeEnum bucketName);

        Task<GridFSDownloadStream> OpenDownloadStreamByNameAsync(string name, MediaTypeEnum bucketName);

        Task<byte[]> DownloadAsBytesAsync(ObjectId id, MediaTypeEnum bucketName);

        Task<byte[]> DownloadAsBytesByNameAsync(string gfsname, MediaTypeEnum bucketName);

        Task<bool> FileExistsAsync(string gfsname, MediaTypeEnum bucketName);
    }
}
