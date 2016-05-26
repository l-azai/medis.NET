using medis.Api.Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medis.Api.Repositories.GridFs
{
    public class GridFsRepository
    {
        private readonly IMongoDatabase _db;

        public GridFsRepository()
        {
            _db = DBInstance.Database;
        }

        public void Test() {
            var bucket = new GridFSBucket(_db);
            //bucket.upload
        }
    }
}