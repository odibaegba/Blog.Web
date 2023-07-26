using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Seed Roles (User, Admin, SuperAdmin)

			var adminRoleId = "e3d811d9-e8c7-4d0d-9392-10d4a2edec1a";
			var superAdminRoleId = "f8f2eeec-2074-46b6-8b08-04546e9e3a7d";
			var userRoleId = "a976d7d5-0e58-4ece-acd6-b2d0e017ef5e";

			var roles = new List<IdentityRole>
			{
			   new IdentityRole
			   {
				   Name = "Admin",
				   NormalizedName = "Admin",
				   Id = adminRoleId,
				   ConcurrencyStamp = adminRoleId
			   },

			   new IdentityRole
			   {
				   Name = "SuperAdmin",
				   NormalizedName = "SuperAdmin",
				   Id = superAdminRoleId,
				   ConcurrencyStamp = superAdminRoleId
			   },

			   new IdentityRole
			   {
				   Name = "User",
				   NormalizedName = "User",
				   Id = userRoleId,
				   ConcurrencyStamp = userRoleId
			   }
			};

			builder.Entity<IdentityRole>().HasData(roles);

			//Seed SuperAdmin
			var superAdminId = "dbab4a31-97f7-49dd-83a4-035bab33194f";
			var superAdmin = new IdentityUser
			{
				UserName = "superadmin@blog.com",
				Email = "superadmin@blog.com",
				NormalizedEmail = "superadmin@blog.com".ToUpper(),
				NormalizedUserName = "superadmin@blog.com".ToUpper(),
				Id = superAdminId
			};

			superAdmin.PasswordHash = new PasswordHasher<IdentityUser>()
							 .HashPassword(superAdmin, "Superadmin@123");

			builder.Entity<IdentityUser>().HasData(superAdmin);

			//Add All Roles to SuperAdmin

			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = userRoleId,
					UserId = superAdminId,
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
		}
	}
}
