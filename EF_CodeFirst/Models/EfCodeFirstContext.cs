using Microsoft.EntityFrameworkCore;

namespace EF_CodeFirst.Models
{
    public class EfCodeFirstContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public EfCodeFirstContext(DbContextOptions<EfCodeFirstContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);

                entity.Property(s => s.StudentId)
                .HasColumnType("int");

                entity.Property(s => s.StudentName)
                .HasColumnType("nchar")
                .HasMaxLength(100);

                entity.Property(s => s.DateOfBirth)
                .HasColumnType("datetime");

                entity.Property(s => s.Height)
                .HasColumnType("decimal(5,2)");

                entity.Property(s => s.Weight)
                .HasColumnType("float");

            });
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(g => g.GradeId);

                entity.Property(g => g.GradeId)
                .HasColumnType("int");

                entity.Property(g => g.GradeName)
                .HasColumnType("nvarchar")
                .HasMaxLength(2);

                entity.Property(g => g.Section)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

                entity.Property(g => g.StudentId)
                .HasColumnType("int");

            });

            modelBuilder.Entity<Grade>()
                .HasOne(s => s.Student)
                .WithMany(g => g.Grades)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Student>().HasData(
                    new Student
                    {
                        StudentId = 1,
                        StudentName = "Ana Marić",
                        DateOfBirth = new DateTime(2012, 4, 10),
                        Height = 135.2m,
                        Weight = 32.5f
                    },
                    new Student
                    {
                        StudentId = 2,
                        StudentName = "Ivan Novak",
                        DateOfBirth = new DateTime(2011, 9, 18),
                        Height = 140.7m,
                        Weight = 36.0f
                    }
                );

            modelBuilder.Entity<Grade>().HasData(
                    new Grade { GradeId = 1, GradeName = "5", Section = "Matematika", StudentId = 1 },
                    new Grade { GradeId = 2, GradeName = "4", Section = "Hrvatski jezik", StudentId = 1 },
                    new Grade { GradeId = 3, GradeName = "5", Section = "Priroda i društvo", StudentId = 1 },
                    new Grade { GradeId = 4, GradeName = "3", Section = "Engleski jezik", StudentId = 2 },
                    new Grade { GradeId = 5, GradeName = "4", Section = "Matematika", StudentId = 2 },
                    new Grade { GradeId = 6, GradeName = "5", Section = "Likovna kultura", StudentId = 2 },
                    new Grade { GradeId = 7, GradeName = "4", Section = "Tjelesna i zdravstvena kultura", StudentId = 2 }
                );
        }

    }
}
