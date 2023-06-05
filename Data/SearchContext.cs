using Microsoft.EntityFrameworkCore;
using uwierzytelnianieClaimsIdentity.Models;
using System;

namespace uwierzytelnianieClaimsIdentity.Data
{
	public class SearchContext : DbContext
	{
		public SearchContext(DbContextOptions options) : base(options) { }
		public DbSet<Search> Search { get; set; }
	}
}
