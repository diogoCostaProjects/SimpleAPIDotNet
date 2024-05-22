using Microsoft.EntityFrameworkCore;
using SimpleApi.Models; // Certifique-se de que este namespace corresponda ao que você usou para Produto

namespace SimpleApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<ProdutoCustom> ProdutosCustom { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo, se necessário
        }
    }
}
