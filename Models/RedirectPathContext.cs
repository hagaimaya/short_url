using Microsoft.EntityFrameworkCore;

namespace short_url.Models
{
    public class RedirectPathContext : DbContext
    {
        public RedirectPathContext(DbContextOptions<RedirectPathContext> options)
            : base(options)
        {
        }
        public DbSet<RedirectPath> RedirectPaths { get; set; }
    }
}