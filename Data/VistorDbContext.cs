using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data
{
    // You need Install Nuget Package for migration and sovling error on Dbcontext
    public class VistorDbContext:DbContext
    {
        public VistorDbContext(DbContextOptions<VistorDbContext> options) : base(options)
        {

        }
        public DbSet<Visitor_Info> VisitorInfos { get; set; }

    }
}
