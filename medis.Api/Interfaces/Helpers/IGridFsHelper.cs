using medis.Api.Enums;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Helpers
{
    public interface IGridFsHelper
    {
        Task<ObjectId> UploadFromStreamAsync(string filename, Stream source, MediaTypeEnum bucketName);
    }
}
