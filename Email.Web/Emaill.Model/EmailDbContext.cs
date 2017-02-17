using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model
{
    public class EmailDbContext:DbContext
    {
        public EmailDbContext(): base("Name=emailContext")
        {
        }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
    }
}
