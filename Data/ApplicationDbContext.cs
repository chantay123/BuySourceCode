using Microsoft.EntityFrameworkCore;
using WebBuySource.Data.Seed;
using WebBuySource.Models;
using YourNamespace.Models;


namespace WebBuySource.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<CodeFile> CodeFiles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CodeTag> CodeTags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payout> Payouts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<CodePromotion> CodePromotions { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserFavorites> UserFavorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Device> Devices { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== ENUM CONVERSIONS =====
            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasConversion<string>();

            modelBuilder.Entity<VerificationCode>()
                .Property(v => v.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Permission>()
                .Property(p => p.Method)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Payout>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Promotion>()
                .Property(p => p.DiscountType)
                .HasConversion<string>();

            modelBuilder.Entity<Promotion>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Conversation>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Code>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Code>()
                .Property(c => c.LicenseType)
                .HasConversion<string>();

            // ===== RELATIONSHIPS =====

            // USER - ROLE
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // ROLE - PERMISSION (n-n)
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // REFRESH TOKEN - USER
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);

            // CATEGORY - PARENT
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentId);

            // CODE - USER (Seller)
            modelBuilder.Entity<Code>()
                .HasOne(c => c.Seller)
                .WithMany(u => u.Codes)
                .HasForeignKey(c => c.SellerId);

            // CODE - CATEGORY
            modelBuilder.Entity<Code>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Codes)
                .HasForeignKey(c => c.CategoryId);

            // CODE - PROGRAMMING LANGUAGE
            modelBuilder.Entity<Code>()
                .HasOne(c => c.ProgrammingLanguage)
                .WithMany(pl => pl.Codes)
                .HasForeignKey(c => c.ProgrammingLanguageId);

            // CODE FILE - CODE
            modelBuilder.Entity<CodeFile>()
                .HasOne(cf => cf.Code)
                .WithMany(c => c.CodeFiles)
                .HasForeignKey(cf => cf.CodeId);

            // CODETAG (n-n)
            modelBuilder.Entity<CodeTag>()
                .HasKey(ct => new { ct.CodeId, ct.TagId });

            modelBuilder.Entity<CodeTag>()
                .HasOne(ct => ct.Code)
                .WithMany(c => c.CodeTags)
                .HasForeignKey(ct => ct.CodeId);

            modelBuilder.Entity<CodeTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CodeTags)
                .HasForeignKey(ct => ct.TagId);

            // TRANSACTION - CODE + USER
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Code)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CodeId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.BuyerId);

            // PAYOUT - USER
            modelBuilder.Entity<Payout>()
                .HasOne(p => p.Seller)
                .WithMany(u => u.Payouts)
                .HasForeignKey(p => p.SellerId);

            // COMMENT - CODE + USER + PARENT
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Code)
                .WithMany(co => co.Comments)
                .HasForeignKey(c => c.CodeId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Buyer)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.BuyerId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany(p => p.Replies)
                .HasForeignKey(c => c.ParentId);

            // DOWNLOAD - USER + CODE
            modelBuilder.Entity<Download>()
                .HasOne(d => d.User)
                .WithMany(u => u.Downloads)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Download>()
                .HasOne(d => d.Code)
                .WithMany(c => c.Download)
                .HasForeignKey(d => d.CodeId);

            // CODEPROMOTION (n-n)
            modelBuilder.Entity<CodePromotion>()
                .HasKey(cp => new { cp.CodeId, cp.PromotionId });

            modelBuilder.Entity<CodePromotion>()
                .HasOne(cp => cp.Code)
                .WithMany(c => c.CodePromotions)
                .HasForeignKey(cp => cp.CodeId);

            modelBuilder.Entity<CodePromotion>()
                .HasOne(cp => cp.Promotion)
                .WithMany(p => p.CodePromotions)
                .HasForeignKey(cp => cp.PromotionId);

            // CONVERSATION - BUYER + ADMIN + CODE
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Buyer)
                .WithMany(u => u.ConversationsAsBuyer)
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Admin)
                .WithMany(u => u.ConversationsAsAdmin)
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Code)
                .WithMany(c => c.Conversations)
                .HasForeignKey(c => c.CodeId);

            // MESSAGE - CONVERSATION + USER
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId);

            // AUDIT LOG - USER
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId);

            // REPORT - CODE + REPORTER
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Code)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.CodeId);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Reporter)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.ReporterId);

            // USER FAVORITE (n-n)
            modelBuilder.Entity<UserFavorites>()
                .HasKey(uf => new { uf.UserId, uf.CodeId });

            modelBuilder.Entity<UserFavorites>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFavorites)
                .HasForeignKey(uf => uf.UserId);

            ///UserFavorites
            modelBuilder.Entity<UserFavorites>()
                .HasOne(uf => uf.Code)
                .WithMany(c => c.Favorites)
                .HasForeignKey(uf => uf.CodeId);


            ////Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);


            //// DEVICE
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasIndex(d => d.DeviceId).IsUnique();
                entity.HasIndex(d => new { d.UserId, d.DeviceId }).IsUnique();
                entity.HasOne(d => d.User)
                      .WithMany(u => u.Devices)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
             
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
         

        }
    }
}
