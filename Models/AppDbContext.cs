using Microsoft.EntityFrameworkCore;

namespace blog_receitas_api.Models
{
    public class AppDbContext: DbContext
    {

        public DbSet<Tipo> Tipos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
