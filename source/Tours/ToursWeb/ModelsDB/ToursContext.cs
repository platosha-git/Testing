using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class ToursContext : DbContext
    {
        public ToursContext()
        {
        }

        public ToursContext(DbContextOptions<ToursContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Tours;Username=postgres;Password=21rfrnec");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "ru_RU.UTF-8");

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.Property(e => e.Foodid)
                    .ValueGeneratedNever()
                    .HasColumnName("foodid");

                entity.Property(e => e.Bar).HasColumnName("bar");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("category");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Menu)
                    .HasMaxLength(30)
                    .HasColumnName("menu");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotel");

                entity.HasIndex(e => e.Name, "uk_h")
                    .IsUnique();

                entity.Property(e => e.Hotelid)
                    .ValueGeneratedNever()
                    .HasColumnName("hotelid");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("city");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Swimpool).HasColumnName("swimpool");

                entity.Property(e => e.Type)
                    .HasMaxLength(40)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("tour");

                entity.Property(e => e.Tourid)
                    .ValueGeneratedNever()
                    .HasColumnName("tourid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Datebegin)
                    .HasColumnType("date")
                    .HasColumnName("datebegin");

                entity.Property(e => e.Dateend)
                    .HasColumnType("date")
                    .HasColumnName("dateend");

                entity.Property(e => e.Food).HasColumnName("food");

                entity.Property(e => e.Hotel).HasColumnName("hotel");

                entity.Property(e => e.Transfer).HasColumnName("transfer");

                entity.HasOne(d => d.FoodNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.Food)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tf");

                entity.HasOne(d => d.HotelNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.Hotel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_th");

                entity.HasOne(d => d.TransferNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.Transfer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tt");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("transfer");

                entity.Property(e => e.Transferid)
                    .ValueGeneratedNever()
                    .HasColumnName("transferid");

                entity.Property(e => e.Cityfrom)
                    .HasMaxLength(30)
                    .HasColumnName("cityfrom");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Departuretime).HasColumnName("departuretime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("userid");

                entity.Property(e => e.Accesslevel).HasColumnName("accesslevel");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.Toursid).HasColumnName("toursid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
