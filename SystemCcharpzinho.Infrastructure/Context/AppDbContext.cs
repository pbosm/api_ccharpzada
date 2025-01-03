using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Core.Models;

namespace SystemCcharpzinho.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Compra> Compras { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasMany(u => u.Compras)
                    .WithOne(c => c.Usuario) // Relacionamento com Usuario
                    .HasForeignKey(c => c.IdUsuario) // Define a chave estrangeira na tabela Compra
                    .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata
            });

            // Relacionamento Compra -> Usuario
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Usuario) // Relacionamento reverso
                    .WithMany(u => u.Compras) // Um Usuario pode ter muitas Compras
                    .HasForeignKey(c => c.IdUsuario) // Chave estrangeira na tabela Compra
                    .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata
            });
        }
    }
}