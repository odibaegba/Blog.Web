﻿namespace Blog.Web.Models.Domain
{
	public class BlogPost
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

		//Navigation Properties
		public ICollection<Tag> Tags { get; set; }

		public ICollection<BlogPostLike> Likes { get; set; }
		public ICollection<BlogPostComment> Comments { get; set; }

	}
}
