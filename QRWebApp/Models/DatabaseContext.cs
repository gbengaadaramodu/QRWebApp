using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace QRWebApp.Models
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext()
        {
        }

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<QRProduct> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnections");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    public class QRProduct
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
    }
}
