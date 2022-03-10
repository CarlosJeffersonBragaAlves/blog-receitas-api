using Microsoft.EntityFrameworkCore;

namespace blog_receitas_api.Models
{
    public class AppDbContext: DbContext
    {

        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<ModoDePreparo> ModoDePreparos { get; set; }
        public DbSet<Status> Status { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
