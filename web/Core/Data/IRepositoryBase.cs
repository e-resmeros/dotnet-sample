using System;
using System.Linq;
using System.Linq.Expressions;

namespace api.Core.Data
{
    public interface IRepositoryBase<T>
    {
        /// <summary> Search for all records from a table as defined
        /// by the model of the passed object. </summary>
        /// <returns>IQueryable - A list of all records found in the table.</returns>
        IQueryable<T> FindAll();

        /// <summary> Filter records from a table defined by the model
        /// of the passed object using the expressions defined in the
        /// parameter.</summary>
        /// <params>expression - A list of conditions to filter the table with.</params>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary> Insert a record to the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be inserted to the table. </params>
        void Create(T entity);

        /// <summary> Updates a record from the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be updated. </params>
        void Update(T entity);

        /// <summary> Deletes a record from the table as defined
        /// by the model of the passed object. </summary>
        /// <params> entity - The record to be deleted from the table. </params>
        void Delete(T entity);
    }
}