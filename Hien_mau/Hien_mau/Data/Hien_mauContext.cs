using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Data
{
    public class Hien_mauContext(DbContextOptions<Hien_mauContext> options) : DbContext(options)
    {        
        public DbSet<User> Users { get; set; }
    }
}
