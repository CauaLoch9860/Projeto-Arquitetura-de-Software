using Microsoft.EntityFrameworkCore;
using Pagamentos.Models;

namespace Pagamentos.Data
{
    public class PagamentoDbContext : DbContext
    {
        public PagamentoDbContext(DbContextOptions<PagamentoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
