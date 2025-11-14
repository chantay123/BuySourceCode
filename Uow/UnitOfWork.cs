using Microsoft.EntityFrameworkCore;
using WebBuySource.Data;
using WebBuySource.Interfaces;
using WebBuySource.Models;


namespace WebBuySource.Uow
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _dbContext;
      

        /// <summary>
        /// Repository of table Categrory
        /// </summary>
        private IRepository<Category> _CategroryRepository;


        /// <summary>
        /// Repository of table Category
        /// </summary
        public IRepository<Category> CategoryRepository
        {
            get { return _CategroryRepository ??= new Repository<Category>(_dbContext); }
        }

        /// <summary>
        /// Repository of table User
        /// </summary>

        private IRepository<User > _UserRepository;

        /// <summary>
        /// Repository of table User 
        /// </summary
        public IRepository<User> UserRepository
        {
            get { return _UserRepository ??= new Repository<User>(_dbContext);}
        }

        /// <summary>
        /// Repository of table RefreshToken
        /// </summary>
        private IRepository<RefreshToken> _RefreshTokenRepository;

        /// <summary>
        /// Repository of table RefreshToken
        /// </summary>
        public IRepository<RefreshToken> RefreshTokenRepository
        {
            get { return _RefreshTokenRepository ??= new Repository<RefreshToken>(_dbContext); }
        }


        /// <summary>
        /// Repository of table VerificationCode (for OTP)
        /// </summary>
        private IRepository<VerificationCode> _VerificationCodeRepository;

        public IRepository<VerificationCode> VerificationCodeRepository
        {
            get { return _VerificationCodeRepository ??= new Repository<VerificationCode>(_dbContext); }
        }

        /// <summary>
        /// Repository of table Role
        /// </summary>
        private IRepository<Role> _RoleRepository;

        public IRepository<Role> RoleRepository
        {
            get { return _RoleRepository ??= new Repository<Role>(_dbContext); }
        }


        #region Constructor
        /// <summary>
        /// Constructor unit of work
        /// </summary>
        /// <param name="appDbContext">Database context</param>
        /// 
        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            // Database context
            _dbContext = appDbContext;
        }
        #endregion

        #region Commit
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }
        #endregion
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
