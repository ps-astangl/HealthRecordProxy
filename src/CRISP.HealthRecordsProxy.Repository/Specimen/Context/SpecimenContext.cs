using CRISP.HealthRecordsProxy.Repository.Specimen.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.HealthRecordsProxy.Repository.Specimen.Context
{
    public partial class SpecimenContext : DbContext
    {

        public SpecimenContext(DbContextOptions<SpecimenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ParentSpecimensSpecimen> ParentSpecimensSpecimen { get; set; }
        public virtual DbSet<Specimens> Specimens { get; set; }
        public virtual DbSet<SpecimensJson> SpecimensJson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentSpecimensSpecimen>(entity =>
            {
                entity.HasKey(e => new { e.ParentSpecimenId, e.SpecimenId });

                entity.ToTable("ParentSpecimens_Specimen");

                entity.Property(e => e.ParentSpecimenId)
                    .HasColumnName("ParentSpecimenID")
                    .HasMaxLength(256);

                entity.Property(e => e.SpecimenId).HasColumnName("SpecimenID");

                entity.HasOne(d => d.Specimen)
                    .WithMany(p => p.ParentSpecimensSpecimen)
                    .HasForeignKey(d => d.SpecimenId)
                    .HasConstraintName("FK__ParentSpe__Speci__19DFD96B");
            });

            modelBuilder.Entity<Specimens>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for Parent")
                    .ForSqlServerIsClustered();

                entity.HasIndex(e => new { e.Smrn, e.AccessionIdentifier })
                    .HasName("Index for SMRN-Accession");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessionIdentifier).HasMaxLength(256);

                entity.Property(e => e.CollectedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Collector).HasMaxLength(65);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasMaxLength(256);

                entity.Property(e => e.Smrn)
                    .IsRequired()
                    .HasColumnName("SMRN")
                    .HasMaxLength(65);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.SpecimenNumber).HasMaxLength(32);

                entity.Property(e => e.Status).HasMaxLength(16);
            });

            modelBuilder.Entity<SpecimensJson>(entity =>
            {
                entity.ToTable("SpecimensJSON");

                entity.HasIndex(e => e.SpecimenId)
                    .HasName("Index for SpecimenId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Response).IsRequired();

                entity.HasOne(d => d.Specimen)
                    .WithMany(p => p.SpecimensJson)
                    .HasForeignKey(d => d.SpecimenId)
                    .HasConstraintName("FK__Specimens__Speci__1AD3FDA4");
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}