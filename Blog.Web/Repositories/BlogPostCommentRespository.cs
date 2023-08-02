using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostCommentRespository : IBlogPostCommentRepository
	{
		private readonly BlogDbContext _dbContext;
		public BlogPostCommentRespository(BlogDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
		{
			await _dbContext.AddAsync(comment);
			await _dbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
		{
			return await _dbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).
				ToListAsync();
		}
	}
}
