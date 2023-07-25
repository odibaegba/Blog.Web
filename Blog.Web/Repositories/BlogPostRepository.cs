using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly BlogDbContext _context;
		public BlogPostRepository(BlogDbContext context)
		{
			_context = context;
		}
		public async Task<BlogPost> AddAsync(BlogPost post)
		{
			await _context.AddAsync(post);
			await _context.SaveChangesAsync();
			return post;
		}

		public async Task<BlogPost?> DeleteAsync(Guid id)
		{
			var existingBlog = await _context.BlogPosts.FindAsync(id);
			if (existingBlog != null)
			{
				_context.BlogPosts.Remove(existingBlog);
				await _context.SaveChangesAsync();
				return existingBlog;
			}
			return null;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await _context.BlogPosts.Include(x => x.Tags).ToListAsync();
		}

		public async Task<BlogPost?> GetAsync(Guid id)
		{
			return await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

		}

		public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
		{
			return await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost post)
		{
			var existingBlog = await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == post.Id);
			if (existingBlog != null)
			{
				existingBlog.Tags = post.Tags;
				existingBlog.Heading = post.Heading;
				existingBlog.PageTitle = post.PageTitle;
				existingBlog.Content = post.Content;
				existingBlog.ShortDescription = post.ShortDescription;
				existingBlog.Author = post.Author;
				existingBlog.FeaturedImageUrl = post.FeaturedImageUrl;
				existingBlog.UrlHandle = post.UrlHandle;
				existingBlog.Visible = post.Visible;
				existingBlog.PublishedDate = post.PublishedDate;
				existingBlog.Tags = post.Tags;

				await _context.SaveChangesAsync();
				return existingBlog;
			}
			return null;
		}
	}
}
