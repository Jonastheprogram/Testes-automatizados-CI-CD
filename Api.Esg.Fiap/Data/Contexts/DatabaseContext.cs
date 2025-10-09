using Api.Esg.Fiap.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Esg.Fiap.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<ColetaModel> Coletas { get; set; }
        public virtual DbSet<ResiduoModel> Residuos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColetaModel>(entity =>
            {
                entity.ToTable("Coletas");
                entity.HasKey(e => e.ColetaId);

                entity.Property(e => e.PontoColeta)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CapacidadeMax)
                    .IsRequired();

                entity.Property(e => e.QtdAtual)
                    .IsRequired();

              
            });

            modelBuilder.Entity<ResiduoModel>(entity =>
            {
                entity.ToTable("Residuos");
                entity.HasKey(e => e.ResiduoId);

                entity.Property(e => e.NomeResiduo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TipoResiduo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Peso)
                    .IsRequired();
            });
        }

        public DatabaseContext(DbContextOptions opt) : base(opt)
        {
        }
    }
}
