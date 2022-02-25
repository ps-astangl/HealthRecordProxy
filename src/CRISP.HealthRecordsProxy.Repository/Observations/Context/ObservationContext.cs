using CRISP.HealthRecordsProxy.Repository.Observations.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.HealthRecordsProxy.Repository.Observations.Context
{
    public partial class ObservationContext : DbContext
    {
        public ObservationContext(DbContextOptions<ObservationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Observations> Observations { get; set; }
        public virtual DbSet<ObservationsCode> ObservationsCode { get; set; }
        public virtual DbSet<ObservationsJson> ObservationsJson { get; set; }
        public virtual DbSet<ObservationsPerformers> ObservationsPerformers { get; set; }
        public virtual DbSet<ObservationsSpecimen> ObservationsSpecimen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Observations>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => new { e.ParentId, e.ObservationNumber })
                    .HasName("Index for SMRN-Accession-Obs")
                    .ForSqlServerIsClustered();

                entity.HasIndex(e => new { e.Source, e.Smrn, e.DateIssued })
                    .HasName("Index for SMRN-Date");

                entity.HasIndex(e => new { e.Source, e.Smrn, e.Category, e.DateIssued })
                    .HasName("Index for SMRN-Cat-Date");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AbnormalFlag).HasMaxLength(32);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Component).IsRequired();

                entity.Property(e => e.DataAbsentReason).HasMaxLength(16);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateEffective).HasColumnType("datetime");

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Device).IsRequired();

                entity.Property(e => e.Method).HasMaxLength(16);

                entity.Property(e => e.ObservationNumber)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.ParentId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Related).IsRequired();

                entity.Property(e => e.Smrn)
                    .IsRequired()
                    .HasColumnName("SMRN")
                    .HasMaxLength(65);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.ValueConcept).HasMaxLength(200);

                entity.Property(e => e.ValueDateTime).HasColumnType("datetime");

                entity.Property(e => e.ValueQuantity).HasMaxLength(50);

                entity.Property(e => e.ValueQuantityUnits).HasMaxLength(32);
            });

            modelBuilder.Entity<ObservationsCode>(entity =>
            {
                entity.HasKey(e => new { e.LoincCode, e.ObservationId });

                entity.HasIndex(e => e.ObservationId)
                    .HasName("nci_wi_ObservationsCode_E361F6590146A9E7959C1FFA1D7E9C68");

                entity.Property(e => e.LoincCode).HasMaxLength(32);

                entity.Property(e => e.ObservationId).HasColumnName("ObservationID");

                entity.HasOne(d => d.Observation)
                    .WithMany(p => p.ObservationsCode)
                    .HasForeignKey(d => d.ObservationId)
                    .HasConstraintName("FK__Observati__Obser__45BE5BA9");
            });

            modelBuilder.Entity<ObservationsJson>(entity =>
            {
                entity.ToTable("ObservationsJSON");

                entity.HasIndex(e => e.ObservationId)
                    .HasName("Index for ObservationID");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ObservationId).HasColumnName("ObservationID");

                entity.Property(e => e.Response).IsRequired();

                entity.HasOne(d => d.Observation)
                    .WithMany(p => p.ObservationsJson)
                    .HasForeignKey(d => d.ObservationId)
                    .HasConstraintName("FK__Observati__Obser__46B27FE2");
            });

            modelBuilder.Entity<ObservationsPerformers>(entity =>
            {
                entity.HasKey(e => new { e.PerformerId, e.ObservationId });

                entity.ToTable("Observations_Performers");

                entity.Property(e => e.PerformerId)
                    .HasColumnName("PerformerID")
                    .HasMaxLength(128);

                entity.Property(e => e.ObservationId).HasColumnName("ObservationID");

                entity.HasOne(d => d.Observation)
                    .WithMany(p => p.ObservationsPerformers)
                    .HasForeignKey(d => d.ObservationId)
                    .HasConstraintName("FK__Observati__Obser__43D61337");
            });

            modelBuilder.Entity<ObservationsSpecimen>(entity =>
            {
                entity.HasKey(e => new { e.SpecimenId, e.ObservationId });

                entity.ToTable("Observations_Specimen");

                entity.HasIndex(e => e.ObservationId)
                    .HasName("nci_wi_Observations_Specimen_E361F6590146A9E7959C1FFA1D7E9C68");

                entity.Property(e => e.SpecimenId)
                    .HasColumnName("SpecimenID")
                    .HasMaxLength(128);

                entity.Property(e => e.ObservationId).HasColumnName("ObservationID");

                entity.HasOne(d => d.Observation)
                    .WithMany(p => p.ObservationsSpecimen)
                    .HasForeignKey(d => d.ObservationId)
                    .HasConstraintName("FK__Observati__Obser__44CA3770");
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}