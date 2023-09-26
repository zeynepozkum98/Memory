using Memory.DataAccess.Concrete.EntityFramework.Mappings;
using Memory.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.DataAccess.Concrete.EntityFramework.Context
{
    public class MemoryContext:IdentityDbContext<AppIdentityUser,AppIdentityRole,int>
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KDK-301-PC07-YZ;Initial Catalog=MemoryDb;uid=sa;pwd=123;"); // veri tabanı bağlantısı
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // config ayarları 
        {
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new NotebookMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
    }
}
