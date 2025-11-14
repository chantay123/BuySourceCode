
using WebBuySource.Models;

namespace WebBuySource.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<Category> CategoryRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<RefreshToken>RefreshTokenRepository { get; }
        IRepository<VerificationCode> VerificationCodeRepository { get; }

        bool Commit();
    }
}
