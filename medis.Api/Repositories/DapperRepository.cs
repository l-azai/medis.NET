using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using DapperExtensions;
using medis.Api.Models;
using medis.Api.Enums;
using medis.Api.Interfaces.Repositories;

namespace medis.Api.Repositories
{
    public class DapperRepository<T> : IRepository<T> where T : DapperEntity
    {
        /// <summary>
        /// The connection string
        /// </summary>
        protected virtual string ConnectionString => ConfigurationManager.ConnectionStrings["Medis"].ConnectionString;

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        protected DbConnection GetConnection() => new SqlConnection(ConnectionString);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                return conn.Get<T>(id);
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                return conn.GetList<T>();
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual bool Update(T entity)
        {
            if (entity == null) {
                throw new ArgumentNullException(typeof(T).Name);
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                entity.DateUpdated = DateTime.Now;
                // TODO: change to actual UserId once implemented
                entity.UpdatedByUserId = (int)UserEnum.System;

                return conn.Update<T>(entity);
            }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                entity.DateCreated = DateTime.Now;
                // TODO: change to actual UserId once implemented
                entity.CreatedByUserId = (int)UserEnum.System;

                var id = conn.Insert<T>(entity);

                entity.Id = id;

                return entity;
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual bool Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                var entity = conn.Get<T>(id);

                if (entity == null) {
                    throw new ArgumentNullException(entity.ToString());
                }

                entity.DateDeleted = DateTime.Now;
                // TODO: change to actual UserId once implemented
                entity.DeletedByUserId = (int)UserEnum.System;

                return conn.Update<T>(entity);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                return conn.Delete<T>(entity);
            }
        }
    }
}
