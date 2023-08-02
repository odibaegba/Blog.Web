﻿using Blog.Web.Models.Domain;

namespace Blog.Web.Models.ViewModels
{
	public class BlogDetailsViewModel
	{
		public Guid Id { get; set; }
		public string Heading { get; set; } = string.Empty;
		public string PageTitle { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = string.Empty;
		public string FeaturedImageUrl { get; set; } = string.Empty;
		public string UrlHandle { get; set; } = string.Empty;
		public DateTime PublishedDate { get; set; }
		public string Author { get; set; } = string.Empty;
		public bool Visible { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public int TotalLikes { get; set; }
		public bool Liked { get; set; }
		public string CommentDescription { get; set; } = string.Empty;
		public IEnumerable<BlogComment> Comments { get; set; }
	}
}
