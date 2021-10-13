using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext _db;

        public RepositoryBase(DbContext Db)
        {
            _db = Db;
        }

        /// <summary> Insert a record to the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be inserted to the table. </params>
        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        /// <summary> Deletes a record from the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be deleted from the table. </params>
        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        /// <summary> Search for all records from a table as defined
        /// by the model of the passed object. </summary>
        /// <returns>IQueryable - A list of all records found in the table.</returns>
        public IQueryable<T> FindAll()
        {
            return _db.Set<T>().AsQueryable<T>();
        }

        /// <summary> Filter records from a table defined by the model
        /// of the passed object using the expressions defined in the
        /// parameter.</summary>
        /// <params>expression - A list of conditions to filter the table with.</params>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Where(expression).AsQueryable<T>();
        }

        /// <summary> Updates a record from the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be updated. </params>
        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
    }
}