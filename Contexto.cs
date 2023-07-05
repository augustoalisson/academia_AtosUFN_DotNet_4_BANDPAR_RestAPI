using BANDPAR_RestAPI.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BANDPAR_RestAPI
{
    public class Contexto : DbContext
    {
        //cada DbSet na classe Contexto vai ser uma entidade(tabela) no banco de dados 
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; } //será adicionado futuramente
        public Contexto()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BANDPAR;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Vendas>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
