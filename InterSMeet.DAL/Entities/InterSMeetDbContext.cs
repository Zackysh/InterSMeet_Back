using InterSMeet.DAL.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InterSMeet.DAL.Entities
{
    public partial class InterSMeetDbContext : DbContext
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class SqlDefaultValueAttribute : Attribute
        {
            public string DefaultValue { get; set; } = null!;
        }

        public InterSMeetDbContext()
        {
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        public InterSMeetDbContext(DbContextOptions<InterSMeetDbContext> options)
            : base(options)
        {
        }

        // User
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        // Student
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Degree> Degrees { get; set; } = null!;
        public virtual DbSet<Family> Families { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        // Company
        public virtual DbSet<Company> Companies { get; set; } = null!;

        // Shared
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Offer> Offers { get; set; } = null!;
        public virtual DbSet<OfferDegree> OfferDegrees { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
#pragma warning restore CS1030 // #warning directive
                    .UseLazyLoadingProxies()
                    .UseMySql("server=localhost;port=3306;user=root;database=InterSMeetDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "IDX_97672ac88f789774dd47f7c8be")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .HasColumnName("first_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
