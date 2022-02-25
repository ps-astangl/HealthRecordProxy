using CRISP.HealthRecordsProxy.Repository.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.HealthRecordsProxy.Repository.ImagingStudy.Context
{
    public partial class ImagingStudyContext : DbContext
    {

        public ImagingStudyContext(DbContextOptions<ImagingStudyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Repository.Context.Models.ImagingStudy> ImagingStudy { get; set; }
        public virtual DbSet<ImagingStudyBasedOn> ImagingStudyBasedOn { get; set; }
        public virtual DbSet<ImagingStudyBodySite> ImagingStudyBodySite { get; set; }
        public virtual DbSet<ImagingStudyCache> ImagingStudyCache { get; set; }
        public virtual DbSet<ImagingStudyDicomClass> ImagingStudyDicomClass { get; set; }
        public virtual DbSet<ImagingStudyEndpoint> ImagingStudyEndpoint { get; set; }
        public virtual DbSet<ImagingStudyIdentifier> ImagingStudyIdentifier { get; set; }
        public virtual DbSet<ImagingStudyJson> ImagingStudyJson { get; set; }
        public virtual DbSet<ImagingStudyModality> ImagingStudyModality { get; set; }
        public virtual DbSet<ImagingStudyPerformer> ImagingStudyPerformer { get; set; }
        public virtual DbSet<ImagingStudyReason> ImagingStudyReason { get; set; }
        public virtual DbSet<ImagingStudySeries> ImagingStudySeries { get; set; }
        public virtual DbSet<ImagingStudyUid> ImagingStudyUid { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository.Context.Models.ImagingStudy>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.FullId)
                    .HasName("Index for FullId")
                    .ForSqlServerIsClustered();

                entity.HasIndex(e => new { e.Smrn, e.DateStarted })
                    .HasName("Index for SMRN-Date");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Accession).HasMaxLength(64);

                entity.Property(e => e.Context).HasMaxLength(128);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateStarted).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FullId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Smrn)
                    .IsRequired()
                    .HasColumnName("SMRN")
                    .HasMaxLength(65);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Study).HasMaxLength(128);

                entity.Property(e => e.StudyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ImagingStudyBasedOn>(entity =>
            {
                entity.HasKey(e => new { e.ReferenceString, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.ReferenceString).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyBasedOn)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__662B2B3B");
            });

            modelBuilder.Entity<ImagingStudyBodySite>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.System, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.System).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyBodySite)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__671F4F74");
            });

            modelBuilder.Entity<ImagingStudyCache>(entity =>
            {
                entity.HasIndex(e => e.ExpiresAtTime)
                    .HasName("Index_ExpiresAtTime");

                entity.Property(e => e.Id)
                    .HasMaxLength(449)
                    .ValueGeneratedNever();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<ImagingStudyDicomClass>(entity =>
            {
                entity.HasKey(e => new { e.Oid, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Oid).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyDicomClass)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__681373AD");
            });

            modelBuilder.Entity<ImagingStudyEndpoint>(entity =>
            {
                entity.HasKey(e => new { e.ReferenceString, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.ReferenceString).HasMaxLength(128);

                entity.Property(e => e.IdentifierValue).HasMaxLength(64);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyEndpoint)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__690797E6");
            });

            modelBuilder.Entity<ImagingStudyIdentifier>(entity =>
            {
                entity.HasKey(e => new { e.Value, e.System, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.System).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyIdentifier)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__69FBBC1F");
            });

            modelBuilder.Entity<ImagingStudyJson>(entity =>
            {
                entity.ToTable("ImagingStudyJSON");

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Response).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyJson)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6AEFE058");
            });

            modelBuilder.Entity<ImagingStudyModality>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.System, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.System).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyModality)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6BE40491");
            });

            modelBuilder.Entity<ImagingStudyPerformer>(entity =>
            {
                entity.HasKey(e => new { e.ReferenceString, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.ReferenceString).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyPerformer)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6CD828CA");
            });

            modelBuilder.Entity<ImagingStudyReason>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.System, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.System).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyReason)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6DCC4D03");
            });

            modelBuilder.Entity<ImagingStudySeries>(entity =>
            {
                entity.HasKey(e => new { e.Oid, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Oid).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudySeries)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6EC0713C");
            });

            modelBuilder.Entity<ImagingStudyUid>(entity =>
            {
                entity.HasKey(e => new { e.Oid, e.ParentId });

                entity.HasIndex(e => e.ParentId)
                    .HasName("Index for ParentId");

                entity.Property(e => e.Oid).HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ImagingStudyUid)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ImagingSt__Paren__6FB49575");
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}