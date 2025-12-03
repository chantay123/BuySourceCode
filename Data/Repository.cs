
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using WebBuySource.Interfaces;

namespace WebBuySource.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Define
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly DbContext Db;
        /// <summary>
        /// DbSet of the entity.
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the repository
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        #endregion

        #region Add entity to database.
        /// <summary>
        /// Add entity to database.
        /// </summary>
        /// <param name="obj">Entity.</param>
        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        /// <summary>
        /// Add entity to database async.
        /// </summary>
        /// <param name="obj">Entity.</param>
        /// <returns>Task complate.</returns>
        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj).ConfigureAwait(false);
        }

        /// <summary>
        /// Add range entity to database.
        /// </summary>
        /// <param name="obj">List entity.</param>
        public void AddRange(IEnumerable<TEntity> obj)
        {
            DbSet.AddRange(obj);
        }

        /// <summary>
        /// Add range entity to database async.
        /// </summary>
        /// <param name="obj">List entity.</param>
        /// <returns>Task complate.</returns>
        public async Task AddRangeAsync(IEnumerable<TEntity> obj)
        {
            await DbSet.AddRangeAsync(obj).ConfigureAwait(false);
        }
        #endregion

        #region Update entity to database.
        /// <summary>
        /// Update entity to database.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        /// <summary>
        /// Update entities to database.
        /// </summary>
        /// <param name="obj">The entities to update.</param>
        public void UpdateRange(IEnumerable<TEntity> obj)
        {
            DbSet.UpdateRange(obj);
        }

        /// <summary>
        /// Update entity to database async.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        /// <returns>Task complete.</returns>
        public async Task UpdateAsync(TEntity obj)
        {
            DbSet.Update(obj);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion

        #region Remove entity.
        /// <summary>
        /// Remove single entity with Id.
        /// </summary>
        /// <param name="id">The entity to remove.</param>
        public void Remove(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        /// <summary>
        /// Remove multiple entity with multiple Id.
        /// </summary>
        /// <param name="id">The entites to remove.</param>
        public void RemoveRange(IEnumerable<string> id)
        {
            DbSet.RemoveRange(DbSet.Find(id));
        }
        #endregion

        #region Get entity by Id.
        /// <summary>
        /// Get entity by Id.
        /// </summary>
        /// <param name="id">The entity Id.</param>
        /// <returns>The entity found, or null.</returns>
        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get entity by Id async.
        /// </summary>
        /// <param name="id">The entity Id.</param>
        /// <returns>The entity found, or null.</returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id).ConfigureAwait(false);
        }
        #endregion

        #region Get entity with filter.
        /// <summary>
        /// Get entity with filter.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites.</returns>
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Get entity with filter no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites.</returns>
        public IQueryable<TEntity> FindByAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();
        }
        #endregion

        #region Get first or default entity.
        /// <summary>
        /// Get first or default entity.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Get first or default entity.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        public TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// Get first or default entity async.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Get first or default entity async.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entitiy mapping or null.</returns>
        public async Task<TEntity> FirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);
        }
        #endregion

        #region Get all entity.
        /// <summary>
        /// Get all data in table.
        /// </summary>
        /// <returns>The entitys in table.</returns>
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites in table.</returns>
        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <returns>The entites in table.</returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Get all data in table with no tracking.
        /// </summary>
        /// <param name="predicate">Where clause to filter.</param>
        /// <returns>The entites in table.</returns>
        public IQueryable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();
        }
        #endregion

        #region Delete entity directly
        /// <summary>
        /// Delete an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Delete multiple entities from the database.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }
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
        public virtual IQueryable<TModel> FromSqlRaw<TModel>(string sql) where TModel : class
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return null;
            }
            return Db.Set<TModel>().FromSqlRaw(sql);
        }

        /// <summary>
        /// Creates a LINQ query based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TModel">The type of query for which a DbQuery should be returned.</typeparam>
        /// <param name="sql">
        /// The raw SQL query. NB. A string literal may be passed here 
        /// because Microsoft.EntityFrameworkCore.RawSqlString
        /// is implicitly convertible to string.</param>
        /// <returns>A DbQuery for the given keyless entity type.</returns>
        /// <param name="parameters">Parameters to use with the SQL.</param>
        /// <returns>A DbQuery for the given keyless entity type.</returns>
        public virtual IQueryable<TModel> FromSqlRaw<TModel>(string sql, ArrayList parameters) where TModel : class
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return null;
            }
            return Db.Set<TModel>().FromSqlRaw(sql, parameters.ToArray());
        }
        #endregion

        #region Executes the given SQL against the database and returns the number of rows affected.
        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <param name="parameters">Parameters to use with the SQL.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteSqlRaw(string sql, ArrayList parameters)
        {
            return Db.Database.ExecuteSqlRaw(sql, parameters?.ToArray());
        }

        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteSqlRaw(string sql)
        {
            return Db.Database.ExecuteSqlRaw(sql);
        }

        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> ExecuteSqlRawAsync(string sql)
        {
            return await Db.Database.ExecuteSqlRawAsync(sql).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the given SQL against the database and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
        /// <param name="parameters">Parameters to use with the SQL.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> ExecuteSqlRawAsync(string sql, ArrayList parameters)
        {
            return await Db.Database.ExecuteSqlRawAsync(sql, parameters?.ToArray()).ConfigureAwait(false);
        }
        #endregion

        #region Saves all changes made in this context to the database.
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in this context to the database async.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //  Requests that the common language runtime not call the finalizer for the specified
            // object.
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        
        #endregion

    }
}
