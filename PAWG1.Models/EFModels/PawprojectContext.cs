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
            entity.HasKey(e => e.IdComponent).HasName("PK__Componen__F186FE86F398602B");

            entity.ToTable("Component");

            entity.Property(e => e.IdComponent).HasColumnName("ID_Component");
            entity.Property(e => e.AllowedRole)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApiKey).IsUnicode(false);
            entity.Property(e => e.ApiKeyId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApiUrl).IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descrip).IsUnicode(false);
            entity.Property(e => e.IdOwner).HasColumnName("ID_Owner");
            entity.Property(e => e.Title)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.TypeComponent)
                .HasMaxLength(70)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.IdOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Component__ID_Ow__68487DD7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__43DCD32D73990B0A");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User__ED4DE442B85BC211");

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Username).IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__ID_Role__656C112C");

            entity.HasMany(d => d.Components1).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Hide",
                    r => r.HasOne<Component>().WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Hide__ComponentI__75A278F5"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Hide__UserId__74AE54BC"),
                    j =>
                    {
                        j.HasKey("UserId", "ComponentId").HasName("PK__Hide__EAF103489A948196");
                        j.ToTable("Hide");
                    });

            entity.HasMany(d => d.ComponentsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Component>().WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__Compon__6C190EBB"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__UserId__6B24EA82"),
                    j =>
                    {
                        j.HasKey("UserId", "ComponentId").HasName("PK__Favorite__EAF10348C94645E9");
                        j.ToTable("Favorite");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
