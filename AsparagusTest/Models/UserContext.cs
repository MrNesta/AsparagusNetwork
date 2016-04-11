using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AsparagusTest.Models
{
    public class UserContext: DbContext
    {
        public UserContext(string connection): base(connection)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
    }
}