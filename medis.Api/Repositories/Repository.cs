using medis.Api.Interfaces.Repositories;
using medis.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medis.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}