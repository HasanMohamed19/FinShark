using api.Data;
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

        public async Task<List<Comment>> GetAllAsync()
		{
			return await _dbContext.Comments.ToListAsync();
		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			return await _dbContext.Comments.FindAsync(id);
		}

		public async Task<Comment?> CreateAsync(Comment commentModel)
		{
			await _dbContext.Comments.AddAsync(commentModel);
			await _dbContext.SaveChangesAsync();
			return commentModel;
		}

	}
}
