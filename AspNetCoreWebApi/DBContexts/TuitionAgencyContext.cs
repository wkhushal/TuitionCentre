using AspNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.DBContexts
{
    public class TuitionAgencyContext : DbContext
    {
        public readonly ILoggerFactory _loggerFactory;

        public TuitionAgencyContext() : base() { }

        public TuitionAgencyContext(DbContextOptions<TuitionAgencyContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TuitionAgency> TuitionAgencies { get; set; }
        public virtual DbSet<TuitionAgencyBranch> TuitionAgencyBranches { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            SetupSubjectEntity();
            SetupCourseEntity();
            SetupTuitionAgencyEntity();
            SetupTuitionAgencyBranchEntity();

            void SetupSubjectEntity()
            {
                var subjectEntity = modelBuilder.Entity<Subject>();
                subjectEntity.Property(subject => subject.SubjectId).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
                subjectEntity.HasOne(subject => subject.Course).WithMany(course => course.Subjects).HasForeignKey(subject => subject.CourseId);
            }

            void SetupCourseEntity()
            {
                var courseEntity = modelBuilder.Entity<Course>();
                courseEntity.Property(course => course.CourseId).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
                courseEntity.HasOne(course => course.TuitionAgency).WithMany(agency => agency.Courses).HasForeignKey(course => course.TuitionAgencyId);
            }

            void SetupTuitionAgencyEntity()
            {
                var tuitionAgencyEntity = modelBuilder.Entity<TuitionAgency>();
                tuitionAgencyEntity.Property(agency => agency.TuitionAgencyId).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            }

            void SetupTuitionAgencyBranchEntity()
            {
                var tuitionAgencyBranchEntity = modelBuilder.Entity<TuitionAgencyBranch>();
                tuitionAgencyBranchEntity.Property(branch => branch.TuitionAgencyBranchId).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
                tuitionAgencyBranchEntity.HasOne(branch => branch.TuitionAgency).WithMany(agency => agency.Branches).HasForeignKey(branch => branch.TuitionAgencyBranchId);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
