using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.Comment;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.CommentResponse;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Models.Enums;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class CommentService : BaseService, ICommentService
    {
        #region Repository
        private IRepository<Comment> CommentRepository => UnitOfWork.CommentRepository;

        private IRepository<Transaction> TransactionRepository => UnitOfWork.TransactionRepository;
        #endregion

        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<BaseAPIResponse> CreateComment(int userId,CommentRequestDTO request )
        {
            if (request.CodeId <= 0)
                return BaseApiResponse.Error("CodeId is required");

            if (request.Rating < 1 || request.Rating > 5)
                return BaseApiResponse.Error("Rating must be between 1 and 5");

            
            var code = await UnitOfWork.CodeRepository
                .GetByIdAsync(request.CodeId);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found");

            var transaction = await TransactionRepository.FirstOrDefaultAsync(t => t.BuyerId == userId && t.CodeId == request.CodeId &&t.Status == TransactionStatus.COMPLETED
                                                                    );

            if (transaction == null)
                return BaseApiResponse.Error("You must purchase this code before commenting");

          
            var comment = new Comment
            {
                CodeId = request.CodeId,
                BuyerId = userId,
                ParentId = request.ParentId,
                Rating = request.Rating,
                CommentText = request.CommentText
            };

            await CommentRepository.AddAsync(comment);
            await CommentRepository.SaveChangesAsync();

           
            var avgRating = await CommentRepository
                .GetAllAsNoTracking()
                .Where(c => c.CodeId == request.CodeId)
                .AverageAsync(c => (double?)c.Rating) ?? 0;

            code.AvgRating = (float)Math.Round(avgRating, 1);
            UnitOfWork.CodeRepository.Update(code);
            await UnitOfWork.CodeRepository.SaveChangesAsync();

          
            var response = new CommentResponseDTO
            {
                Id = comment.Id,
                Rating = comment.Rating,
                CommentText = comment.CommentText,
                Username = "You", 
                CreatedAt = comment.CreatedAt
            };

            return BaseApiResponse.OK(response, "Comment created successfully");
           
        }

        public async Task<BaseAPIResponse> GetCommentsByCode(CommentRequestDTO request)
        {
            if (request.CodeId <= 0)
                return BaseApiResponse.Error("CodeId is required");

            var query = CommentRepository
                .GetAllAsNoTracking()
                .Where(c => c.CodeId == request.CodeId && c.ParentId == null);

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CommentResponseDTO
                {
                    Id = c.Id,
                    Rating = c.Rating,
                    CommentText = c.CommentText,
                    Username = c.Buyer.Username,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return BaseApiResponse.OK(items, total, request.PageSize);
        }
    }
}
