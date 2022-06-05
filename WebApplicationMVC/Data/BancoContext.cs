using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Data
{
    public class BancoContext: DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<ContaModel> Conta { get; set; }
        public DbSet<TransacaoModel> Transacao { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransacaoPoco>().Metadata.SetIsTableExcludedFromMigrations(true);
        }
    }
}
