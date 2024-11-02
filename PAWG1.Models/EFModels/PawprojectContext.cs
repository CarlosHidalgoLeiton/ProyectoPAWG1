using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PAWG1.Models.EFModels;

public partial class PawprojectContext : DbContext
{
    public PawprojectContext()
    {
    }

    public PawprojectContext(DbContextOptions<PawprojectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PAWProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.IdComponent).HasName("PK__Componen__F186FE86F407AD3B");

            entity.ToTable("Component");

            entity.Property(e => e.IdComponent).HasColumnName("ID_Component");
            entity.Property(e => e.ApiKey).IsUnicode(false);
            entity.Property(e => e.ApiUrl).IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descrip).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.TypeComponent)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__43DCD32D56B386F0");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User__ED4DE442A1292B8C");

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Username).IsUnicode(false);

            entity.HasMany(d => d.IdRoles).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__ID_Rol__6EF57B66"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__ID_Use__6E01572D"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdRole").HasName("PK__UserRole__29702970AE9A536B");
                        j.ToTable("UserRole");
                        j.IndexerProperty<int>("IdUser").HasColumnName("ID_User");
                        j.IndexerProperty<int>("IdRole").HasColumnName("ID_Role");
                    });

            entity.HasMany(d => d.Widgets).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Component>().WithMany()
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__Widget__72C60C4A"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__UserId__71D1E811"),
                    j =>
                    {
                        j.HasKey("UserId", "WidgetId").HasName("PK__Favorite__2D571F4DED1D8A40");
                        j.ToTable("Favorite");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
