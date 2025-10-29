using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoLAM.Models;

namespace ProjetoLAM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genero> Genero { get; set; }
        public DbSet<Livros> Livros { get; set; }
        public DbSet<LivrosDisponiveis> LivrosDisponiveis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>().ToTable("Generos");
            modelBuilder.Entity<Livros>().ToTable("Livros");
            modelBuilder.Entity<LivrosDisponiveis>().ToTable("LivrosDisponiveis");

            // Livros -> Genero
            modelBuilder.Entity<Livros>()
                .HasOne(l => l.Genero)
                .WithMany()
                .HasForeignKey(l => l.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            // LivrosDisponiveis -> Livros
            modelBuilder.Entity<LivrosDisponiveis>()
                .HasOne(ld => ld.Livros)
                .WithMany()
                .HasForeignKey(ld => ld.LivroId)
                .OnDelete(DeleteBehavior.Restrict);

            // LivrosDisponiveis -> Genero
            modelBuilder.Entity<LivrosDisponiveis>()
                .HasOne(ld => ld.Genero)
                .WithMany()
                .HasForeignKey(ld => ld.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
