using Microsoft.EntityFrameworkCore;
using SGEV.Core.Entities;
using SGEV.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SGEV.Data.Context
{
    internal class SGEVDbContext : DbContext
    {
        //Acomplar os objetos entities com os dados/propriedades ja tratados
        public DbSet<ProductStock> EstoqueProdutos { get; set; }
        public DbSet<ProductSales> VendasProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductStock>(x => x.HasKey(x => x.Id));
            builder.Entity<ProductSales>(x => x.HasKey(x => x.Id));

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=SERVER NAME;Database=DATABASE NAME;Trusted_Connection=True;");//Connection string
            base.OnConfiguring(optionsBuilder);
        }


        public static ResponseModel<bool> Validation()
        {
            ResponseModel <bool> response = new ResponseModel<bool>();
            try 
            {
                this.Database.OpenConnection();
                this.Database.CloseConnection();

                response.Status = true;

                return response;
            }
            catch(Exception ex){

                response.Message = "Falha na tentativa de conexão ao banco de dados. \n\nErro: "+ex.Message;
                return response;
            }
            
            
        }
    }
}
