using System.Data.Entity;
using Queryable.Web.Models;

namespace Queryable.Web.Infrastructure
{
    public class AppContext : DbContext
    {
        public AppContext() : base("Default")
        {
        }

        public DbSet<Person> People { get { return base.Set<Person>(); } }
    }
}