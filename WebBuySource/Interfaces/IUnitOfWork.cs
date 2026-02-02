
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
        IRepository <Code> CodeRepository { get; }
        IRepository<CodeFile> CodeFileRepository { get; }
        IRepository<CodeTag> CodeTagRepository { get; }
        IRepository<RolePermission> RolePermissionRepository { get; }
        IRepository<Permission> PermissionRepository { get; }
        IRepository<Tag> TagRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<ProgrammingLanguage> ProgrammingLanguageRepository { get; }

        IRepository<Transaction> TransactionRepository { get; }

        IRepository<CodeLike> CodeLikeRepository { get; }



        bool Commit();
    }
}
