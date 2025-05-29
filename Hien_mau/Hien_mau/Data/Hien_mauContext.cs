using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Data
{
    public class Hien_mauContext : DbContext
    {
        public Hien_mauContext(DbContextOptions<Hien_mauContext> options) : base(options)  { }


    }
}
