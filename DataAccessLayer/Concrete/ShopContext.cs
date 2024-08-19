using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
	public class ShopContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=RABIA\\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=True;TrustServerCertificate=True");
		}
		public DbSet<Campaign> Campaigns { get; set; } 
		public DbSet<DealerCategory> DealerCategories { get; set; } 
		public DbSet<Contact> Contacts { get; set; } 
		public DbSet<Dealer> Dealers { get; set; } 
		public DbSet<News> New { get; set; } 
		public DbSet<Video> Videos { get; set; } 

	}
}
