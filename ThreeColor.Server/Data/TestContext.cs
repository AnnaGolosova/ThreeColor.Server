using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ThreeColor.Data.Models;

namespace ThreeColor.Server.Data
{
    public class TestContext : DbContext
    {
        public TestContext()
#if DEBUG
            : base("TestConnection") { }
#else
            : base("ProdConnection") { }
#endif

        public virtual DbSet<Tests> Tests { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}