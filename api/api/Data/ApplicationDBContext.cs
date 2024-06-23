using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
	public class ApplicationDBContext : IdentityDbContext<AppUser>
	{
		public ApplicationDBContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Stock> Stocks { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// seed rodes, we will have admin and user
			List<IdentityRole> roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "USER"
				},
			};

			// add the roles to the db
			builder.Entity<IdentityRole>().HasData(roles);

		}
	}
}
