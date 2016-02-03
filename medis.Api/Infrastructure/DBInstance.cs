using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace medis.Api.Infrastructure
{
    public sealed class DBInstance
    {
        // mongodb://localhost:27017/medis

        private static string ConnectionString => ConfigurationManager.AppSettings["InspirConnectionString"];
        private static string DatabaseName => ConfigurationManager.AppSettings["InspirDBName"];

        private static readonly Lazy<IMongoDatabase> inst = new Lazy<IMongoDatabase>(() => {
            var client = new MongoClient(ConnectionString);
            return client.GetDatabase(DatabaseName);
        });

        public static IMongoDatabase Database
        {
            get
            {
                return inst.Value;
            }
        }
    }
}