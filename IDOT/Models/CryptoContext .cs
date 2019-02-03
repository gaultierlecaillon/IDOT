using Microsoft.EntityFrameworkCore;

namespace IDOT.Models
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options)
            : base(options)
        {
        }

        public DbSet<CryptoItem> CryptoItems { get; set; }
    }
}
