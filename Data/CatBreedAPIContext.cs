using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CatBreedAPI.Models;

namespace CatBreedAPI.Data
{
    public partial class CatBreedAPIContext : DbContext
    {
        public CatBreedAPIContext()
        {
        }

        public CatBreedAPIContext(DbContextOptions<CatBreedAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Breed> Breeds { get; set; } = null!;
        public virtual DbSet<Cat> Cats { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.ToTable("BREED");

                entity.Property(e => e.BreedId)
                    .ValueGeneratedNever()
                    .HasColumnName("breed_id");

                entity.Property(e => e.BreedName)
                    .HasMaxLength(19)
                    .HasColumnName("breed_name");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.PriceRange)
                    .HasMaxLength(5)
                    .HasColumnName("price_range");
            });

            modelBuilder.Entity<Cat>(entity =>
            {
                entity.ToTable("CAT");

                entity.HasIndex(e => e.BreedId, "breed_id");

                entity.Property(e => e.CatId)
                    .ValueGeneratedNever()
                    .HasColumnName("cat_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BreedId).HasColumnName("breed_id");

                entity.Property(e => e.CatName)
                    .HasMaxLength(50)
                    .HasColumnName("cat_name");

                //entity.HasOne(d => d.Breed)
                //    .WithMany(p => p.Cats)
                //    .HasForeignKey(d => d.BreedId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("cat_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
