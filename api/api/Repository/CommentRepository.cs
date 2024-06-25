using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
	public class CommentRepository : ICommentRepository
	{

		private readonly ApplicationDBContext _dbContext;

        public CommentRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
		{
			var comments = _dbContext.Comments.Include(a => a.AppUser).AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
			{
				comments = comments.Where(s => s.Stock.Symbol.ToLower() == queryObject.Symbol.ToLower());
			}

			if (queryObject.IsDecsending)
			{
				comments = comments.OrderByDescending(c => c.CreateedOn);
			}

			return await comments.ToListAsync();

		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			return await _dbContext.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Comment?> CreateAsync(Comment commentModel)
		{
			await _dbContext.Comments.AddAsync(commentModel);
			await _dbContext.SaveChangesAsync();
			return commentModel;
		}

		public async Task<Comment?> UpdateAsync(int id, Comment comment)
		{
			var existingComment = await _dbContext.Comments.FindAsync(id);
			if (existingComment == null)
			{
				return null;
			}

			existingComment.Title = comment.Title;
			existingComment.Content = comment.Content;
			
			await _dbContext.SaveChangesAsync();
			return existingComment;
		}

		public async Task<Comment?> DeleteAsync(int id)
		{
			var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

			if (comment == null)
			{
				return null;
			}

			_dbContext.Comments.Remove(comment);
			await _dbContext.SaveChangesAsync();
			return comment;

		}

	}
}
