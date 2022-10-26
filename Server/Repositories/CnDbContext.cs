using StoreBlazor.Client.Aggregates;
using Microsoft.EntityFrameworkCore;


namespace StoreBlazor.Server.Repositories
{
    public class CnDbContext : DbContext
    {
        public CnDbContext(DbContextOptions<CnDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductsAggregate> Countries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            /*
            modelBuilder.Entity<ProductsAggregate>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("VSIVisitors");

                entity.Property(e => e.ProductType).HasColumnName("ProductType");

                entity.Property(e => e.SpecialityType).HasColumnName("SpecialityType");

                entity.Property(e => e.Speciality).HasColumnName("Speciality");

            });
            */
            base.OnModelCreating(modelBuilder);
                

        }
    }
}