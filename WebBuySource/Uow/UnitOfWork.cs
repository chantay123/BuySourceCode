using Microsoft.EntityFrameworkCore;
using WebBuySource.Data;
using WebBuySource.Interfaces;
using WebBuySource.Models;

namespace WebBuySource.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Repository of table Category
        /// </summary>
        private IRepository<Category>? _CategoryRepository;

        public IRepository<Category> CategoryRepository =>
            _CategoryRepository ??= new Repository<Category>(_dbContext);

        /// <summary>
        /// Repository of table User
        /// </summary>
        private IRepository<User>? _UserRepository;

        public IRepository<User> UserRepository =>
            _UserRepository ??= new Repository<User>(_dbContext);

        /// <summary>
        /// Repository of table RefreshToken
        /// </summary>
        private IRepository<RefreshToken>? _RefreshTokenRepository;

        public IRepository<RefreshToken> RefreshTokenRepository =>
            _RefreshTokenRepository ??= new Repository<RefreshToken>(_dbContext);

        /// <summary>
        /// Repository of table VerificationCode (for OTP)
        /// </summary>
        private IRepository<VerificationCode>? _VerificationCodeRepository;

        public IRepository<VerificationCode> VerificationCodeRepository =>
            _VerificationCodeRepository ??= new Repository<VerificationCode>(_dbContext);

        /// <summary>
        /// Repository of table Role
        /// </summary>
        private IRepository<Role>? _RoleRepository;

        public IRepository<Role> RoleRepository =>
            _RoleRepository ??= new Repository<Role>(_dbContext);

        /// <summary>
        /// Repository of table Code 
        /// </summary>
        private IRepository<Code>? _CodeRepository;

        public IRepository<Code> CodeRepository => _CodeRepository ??= new Repository<Code>(_dbContext);


        /// <summary>
        /// Repository of table CodeFile  
        /// </summary>
        private IRepository<CodeFile>? _CodeFileRepository;
        public IRepository<CodeFile> CodeFileRepository => _CodeFileRepository ??= new Repository<CodeFile>(_dbContext);


        /// <summary>
        /// Repository of table CodeTag
        /// </summary>
        private IRepository<CodeTag>? _CodeTagRepository;
        public IRepository<CodeTag> CodeTagRepository => _CodeTagRepository ??= new Repository<CodeTag>(_dbContext);

        /// <summary>
        /// Repository of table  RolePermission
        /// </summary>
        private IRepository<RolePermission>? _RolePermissionRepository;
        public IRepository<RolePermission> RolePermissionRepository => _RolePermissionRepository ??= new Repository<RolePermission>(_dbContext);


        /// <summary>
        /// Repository of table  RolePermission
        /// </summary>
        private IRepository<Permission>? _PermissionRepository;
        public IRepository<Permission> PermissionRepository => _PermissionRepository ??= new Repository<Permission>(_dbContext);

        /// <summary>
        /// Repository of table Tag
        /// </summary>
        private IRepository<Tag>? _TagRepository;
        public IRepository<Tag> TagRepository => _TagRepository ??= new Repository<Tag>(_dbContext);

        #region Constructor

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _dbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }
        #endregion

        #region Commit  
        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }
        #endregion

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
