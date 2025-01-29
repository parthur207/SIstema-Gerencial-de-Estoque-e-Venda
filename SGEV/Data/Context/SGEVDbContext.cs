using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SGEV.Data.Context
{
    internal class SGEVDbContext : DbContext
    {
        //Acomplar os objetos entities com os dados/propriedades ja tratados
        public DbSet<> EstoqueProdutos { get; set; }
        public DbSet<> VendasProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProdutosEstoque>(x => x.HasKey(x => x.Id));
            builder.Entity<ProdutosVendas>(x => x.HasKey(x => x.Id));

            base.OnModelCreating(modelBuilder);
        }
        protected override OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SERVER NAME;Database=DATABASE NAME;Trusted_Connection=True;");
        }
    }
}
