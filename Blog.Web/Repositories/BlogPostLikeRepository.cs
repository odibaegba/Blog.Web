using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostLikeRepository : IBlogPostLikeRepository
	{
		private readonly BlogDbContext _context;
		public BlogPostLikeRepository(BlogDbContext context)
		{
			_context = context;
		}

		public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
		{
			await _context.BlogPostLikes.AddAsync(blogPostLike);
			await _context.SaveChangesAsync();
			return blogPostLike;
		}

		public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return await _context.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			return await _context.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
		}

	}
}
