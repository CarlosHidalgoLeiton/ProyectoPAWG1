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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PAWProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.IdComponent).HasName("PK__Componen__F186FE863E068CE4");

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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C780A7E13");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053433F670A7").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastName).IsUnicode(false);
            entity.Property(e => e.PasswordHash).IsUnicode(false);
            entity.Property(e => e.Role).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);

            entity.HasMany(d => d.Widgets).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Component>().WithMany()
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__Widget__534D60F1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__UserId__52593CB8"),
                    j =>
                    {
                        j.HasKey("UserId", "WidgetId").HasName("PK__Favorite__2D571F4D5CFE8791");
                        j.ToTable("Favorite");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
