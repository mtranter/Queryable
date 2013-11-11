using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Queryable.Models;

namespace Queryable.Infrastructure
{
    public class AppContext : DbContext
    {
        public AppContext() : base("Default")
        {
        }

        public DbSet<Person> People { get { return base.Set<Person>(); } }
    }
}