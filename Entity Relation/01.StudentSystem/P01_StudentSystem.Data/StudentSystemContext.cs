namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        { }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);


                entity.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsRequired(false)
                .IsUnicode(false);

                entity.Property(e => e.RegisteredOn)
                .HasColumnType("DATETIME2");

                entity.Property(e => e.Birthday)
                .HasColumnType("DATETIME2")
                .IsRequired(false);


                entity.HasMany(c => c.CourseEnrollments)
                .WithOne(c => c.Student);

                entity.HasMany(h => h.HomeworkSubmissions)
                .WithOne(c => c.Student);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);


                entity.Property(e => e.Name)
                .IsUnicode()
                .HasMaxLength(80);

                entity.Property(e => e.Description)
                .IsUnicode()
                .IsRequired(false);

                entity.Property(e => e.StartDate)
                .HasColumnType("DATETIME2");

                entity.Property(e => e.EndDate)
                .HasColumnType("DATETIME2");

                entity.Property(e => e.Price)
                .HasColumnType("DECIMAL(20,5)");


                entity.HasMany(c => c.StudentsEnrolled)
                .WithOne(e => e.Course);

                entity.HasMany(c => c.Resources)
                .WithOne(e => e.Course);

                entity.HasMany(c => c.HomeworkSubmissions)
                .WithOne(e => e.Course);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(c => c.ResourceId);


                entity.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.Url)
                .IsUnicode(false);

                entity.HasOne(c => c.Course)
                .WithMany(r => r.Resources);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);


                entity.Property(e => e.Content)
                .IsUnicode(false);

                entity.Property(e => e.SubmissionTime)
                .HasColumnType("DATETIME2");

                entity.HasOne(e => e.Student)
                .WithMany(c => c.HomeworkSubmissions);

                entity.HasOne(e => e.Course)
                .WithMany(c => c.HomeworkSubmissions);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.CourseId,
                    e.StudentId
                });

                entity.HasOne(c => c.Course)
                .WithMany(s => s.StudentsEnrolled);

                entity.HasOne(c => c.Student)
                .WithMany(e => e.CourseEnrollments);
            });
        }
    }
}
