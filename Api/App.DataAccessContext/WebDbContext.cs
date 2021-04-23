using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace App.DataAccessContext
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}
