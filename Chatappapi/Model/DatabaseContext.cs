using Microsoft.EntityFrameworkCore;

namespace Chatappapi.Model
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DatabaseContext(DbContextOptions option ):base(option) {}
        
    }
}
