using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final.net.Models
{
    public partial class DriveContext : DbContext
    {
        public DriveContext()
        {
        }

        public DriveContext(DbContextOptions<DriveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Drive;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.DocumentName)
                    .HasColumnName("Document_Name")
                    .HasMaxLength(60);

                entity.Property(e => e.DocumentType)
                    .HasColumnName("Document_Type")
                    .HasMaxLength(30);

                entity.Property(e => e.FolderId).HasColumnName("Folder_Id");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Documents__Creat__628FA481");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.FolderId)
                    .HasConstraintName("FK__Documents__Folde__6383C8BA");
            });

            modelBuilder.Entity<Folders>(entity =>
            {
                entity.HasKey(e => e.FolderId);

                entity.Property(e => e.FolderId).HasColumnName("Folder_Id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.FolderName)
                    .HasColumnName("Folder_Name")
                    .HasMaxLength(60);

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Folders__Created__5FB337D6");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("User_Password")
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
