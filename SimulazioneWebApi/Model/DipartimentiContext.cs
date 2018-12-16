using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimulazioneWebApi.Model
{
    public partial class DipartimentiContext : DbContext
    {
        public DipartimentiContext()
        {
        }

        public DipartimentiContext(DbContextOptions<DipartimentiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dipartimenti> Dipartimenti { get; set; }
        public virtual DbSet<Impiegati> Impiegati { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimulazioneAngular;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dipartimenti>(entity =>
            {
                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.Dipartimenti)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dipartimenti_Impiegati");
            });

            modelBuilder.Entity<Impiegati>(entity =>
            {
                entity.HasOne(d => d.IdDipartimentoNavigation)
                    .WithMany(p => p.Impiegati)
                    .HasForeignKey(d => d.IdDipartimento)
                    .HasConstraintName("FK_Impiegati_Dipartimenti");
            });
        }
    }
}
