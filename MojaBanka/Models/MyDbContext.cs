using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml;

namespace MojaBanka.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MyDbContext")
        {
        }

        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Transakcija> Transakcije { get; set; }
    }
}