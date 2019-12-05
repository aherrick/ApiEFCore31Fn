using Microsoft.EntityFrameworkCore;

namespace ApiEFCore31Fn
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options)
            : base(options)
        {
        }

        public static Db GetInstance()
        {
            var options = new DbContextOptionsBuilder<Db>()
             .UseInMemoryDatabase(databaseName: "Db")
             .Options;

            return new Db(options);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}