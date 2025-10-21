
using WebBuySource.Models;

namespace WebBuySource.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<Category> CategoryRepository { get; }
        IRepository<User> UserRepository { get; }

        IRepository<RefreshToken>RefreshTokenRepository { get; }

        bool Commit();
    }
}
