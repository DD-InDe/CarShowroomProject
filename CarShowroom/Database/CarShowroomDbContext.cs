using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Database;

public partial class CarShowroomDbContext : DbContext
{
    public CarShowroomDbContext()
    {
    }

    public CarShowroomDbContext(DbContextOptions<CarShowroomDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BodyType> BodyTypes { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarPhoto> CarPhotos { get; set; }

    public virtual DbSet<CarStatus> CarStatuses { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<EngineType> EngineTypes { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<ModelGeneration> ModelGenerations { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LEGION;Database=CarShowroomDb;Trusted_connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodyType>(entity =>
        {
            entity.HasKey(e => e.BodyTypeId).HasName("PK__BodyType__3F42D9A1CD9B8DA8");

            entity.ToTable("BodyType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F05E17627467");

            entity.ToTable("Brand");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarVin).HasName("PK__Car__5807E445E776C0FE");

            entity.ToTable("Car");

            entity.Property(e => e.CarVin)
                .HasMaxLength(17)
                .HasColumnName("CarVIN");
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Model).WithMany(p => p.Cars)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Car__ModelId__6B24EA82");

            entity.HasOne(d => d.Status).WithMany(p => p.Cars)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Car__StatusId__6C190EBB");
        });

        modelBuilder.Entity<CarPhoto>(entity =>
        {
            entity.HasKey(e => e.CarPhotoId).HasName("PK__CarPhoto__6C84FE2811F7D871");

            entity.ToTable("CarPhoto");

            entity.Property(e => e.CarVin)
                .HasMaxLength(17)
                .HasColumnName("CarVIN");

            entity.HasOne(d => d.CarVinNavigation).WithMany(p => p.CarPhotos)
                .HasForeignKey(d => d.CarVin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarPhoto__CarVIN__6EF57B66");
        });

        modelBuilder.Entity<CarStatus>(entity =>
        {
            entity.HasKey(e => e.CarStatusId).HasName("PK__CarStatu__4A328CC68D1C8A66");

            entity.ToTable("CarStatus");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927C033B2DE8E");

            entity.ToTable("Class");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EngineType>(entity =>
        {
            entity.HasKey(e => e.EngineTypeId).HasName("PK__EngineTy__B01776FC4ACAB2DE");

            entity.ToTable("EngineType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Model__E8D7A12C3958BD4E");

            entity.ToTable("Model");

            entity.Property(e => e.EngineVolume).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Height).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Length).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Width).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.BodyType).WithMany(p => p.Models)
                .HasForeignKey(d => d.BodyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Model__BodyTypeI__66603565");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Model__BrandId__6477ECF3");

            entity.HasOne(d => d.Class).WithMany(p => p.Models)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Model__ClassId__68487DD7");

            entity.HasOne(d => d.EngineType).WithMany(p => p.Models)
                .HasForeignKey(d => d.EngineTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Model__EngineTyp__6754599E");

            entity.HasOne(d => d.Generation).WithMany(p => p.Models)
                .HasForeignKey(d => d.GenerationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Model__Generatio__656C112C");
        });

        modelBuilder.Entity<ModelGeneration>(entity =>
        {
            entity.HasKey(e => e.ModelGenerationId).HasName("PK__ModelGen__4E0A55BEA0A75BB4");

            entity.ToTable("ModelGeneration");

            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.PassportId).HasName("PK__Passport__185653D0D92BEABC");

            entity.ToTable("Passport");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedBy).HasMaxLength(200);
            entity.Property(e => e.IssuedPlace).HasMaxLength(200);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A354B7AC7");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CFC997C7E");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ__User__5E55825B77378D6A").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__5535A963");

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User___FK___Passport");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
