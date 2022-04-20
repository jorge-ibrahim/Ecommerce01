using Ecommerce01.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce01.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//con todo esto le agrego un indice al campo nombre de country asi sea unico
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            //indises compuestos(no puede haber mas de una provincia con el mismo nombre en un pais,pero si en otro pais
            //puede haber una provincia con el mismo nombre,misma situacion para las ciudades por provincia)
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();
        }

    }
}
