using System.Collections;
using System.Linq.Expressions;
namespace WebBuySource.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Add entity to database.
        /// <summary>
        /// Add entity to database.
        /// </summary>
        /// <param name="obj">Entity.</param>
        void Add(TEntity obj);
        /// <summary>
        /// Add entity to database async.
        /// </summary>
        /// <param name="obj">Entity.</param>
        /// <returns>Task complate.</returns>
        Task AddAsync(TEntity obj);
        /// <summary>
        /// Add range entity to database.
        /// </summary>
        /// <param name="obj">List entity.</param>
        void AddRange(IEnumerable<TEntity> obj);
        /// <summary>
        /// Add range entity to database async.
        /// </summary>
        /// <param name="obj">List entity.</param>
        /// <returns>Task complate.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> obj);
        #endregion

        #region Update entity to database.
        /// <summary>
        /// Update entity to database.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        void Update(TEntity obj);
        /// <summary>
        /// Update entities to database.
        /// </summary>
        /// <param name="obj">The entities to update.</param>
        void UpdateRange(IEnumerable<TEntity> obj);

        /// <summary>
        /// Update entity to database async.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        /// <returns>Task complete.</returns>
        Task UpdateAsync(TEntity obj);
        #endregion

        #region Remove entity.
        /// <summary>
        /// Remove single entity with Id.
        /// </summary>
        /// <param name="id">The entity to remove.</param>
        void Remove(string id);
        /// <summary>
        /// Remove multiple entity with multiple Id.
        /// </summary>
        /// <param name="id">The entites to remove.</param>
        void RemoveRange(IEnumerable<String> id);
        #endregion

        #region Get entity by Id.
        /// <summary>
        /// Get entity by Id.
        /// </summary>
        /// <param name="id">The entity Id.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity GetById(int id);
        /// <summary>
        /// Get entity by Id async.
        /// </summary>
        /// <param name="id">The entity Id.</param>
        /// <returns>The entity found, or null.</returns>
        Task<TEntity> GetByIdAsync(int id);
        #endregion

        #region Get all entity.
        /// <summary>
        /// Get all data in table.
        /// </summary>
        /// <returns>The entitys in table.</returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <returns>The entites in table.</returns>
        IQueryable<TEntity> GetAllAsNoTracking();
        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites in table.</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites in table.</returns>
        IQueryable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Get first or default entity.
        /// <summary>
        /// Get first or default entity.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get first or default entity.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get first or default entity async.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get first or default entity async.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        Task<TEntity> FirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Get entity with filter.
        /// <summary>
        /// Get entity with filter.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites.</returns>
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get entity with filter no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites.</returns>
        IQueryable<TEntity> FindByAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Creates a LINQ query based on a raw SQL query.
        /// <summary>
        /// Creates a LINQ query based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TModel">The type of query for which a DbQuery should be returned.</typeparam>
        /// <param name="sql">
        /// The raw SQL query. NB. A string literal may be passed here 
        /// because Microsoft.EntityFrameworkCore.RawSqlString
        /// is implicitly convertible to string.</param>
        /// <returns>A DbQuery for the given keyless entity type.</returns>
        IQueryable<TModel> FromSqlRaw<TModel>(string sql) where TModel : class;

        /// <summary>
        /// Creates a LINQ query based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TModel">The type of query for which a DbQuery should be returned.</typeparam>
        /// <param name="sql">
        /// The raw SQL query. NB. A string literal may be passed here 
        /// because Microsoft.EntityFrameworkCore.RawSqlString
        /// is implicitly convertible to string.</param>
        /// <returns>A DbQuery for the given keyless entity type.</returns>
        IQueryable<TModel> FromSqlRaw<TModel>(string sql, ArrayList parameters) where TModel : class;
        #endregion

        #region Executes the given SQL against the database and returns the number of rows affected.
        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <param name="parameters">Parameters to use with the SQL.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteSqlRaw(string sql, ArrayList parameters);

        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteSqlRaw(string sql);

        /// <summary>
        /// Executes the SQL raw asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        Task<int> ExecuteSqlRawAsync(string sql);

        /// <summary>
        /// Executes the SQL raw asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<int> ExecuteSqlRawAsync(string sql, ArrayList parameters);
        #endregion

        #region Saves all changes made in this context to the database.
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();
        /// <summary>
        /// Saves all changes made in this context to the database async.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();
        #endregion

        #region Delete entity directly
        /// <summary>
        /// Delete an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete multiple entities from the database.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void DeleteRange(IEnumerable<TEntity> entities);
        #endregion
    }
}
