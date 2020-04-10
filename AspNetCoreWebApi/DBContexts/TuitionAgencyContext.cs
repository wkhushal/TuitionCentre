using AspNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.DBContexts
{
    public class TuitionAgencyContext : DbContext
    {
        public TuitionAgencyContext() { }

        public TuitionAgencyContext(DbContextOptions<TuitionAgencyContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            var courseEntity = modelBuilder.Entity<Course>();
            courseEntity.Property(course => course.CourseId).HasColumnName("Id").HasDefaultValue(0).IsRequired();
            courseEntity.HasOne(course => course.TuitionAgency).WithMany(agency => agency.Courses).HasForeignKey(course => course.TuitionAgencyId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
