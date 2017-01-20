using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class EmailDbContext:DbContext
    {
        public EmailDbContext(): base("Name=emailContext")
        {
        }
        public DbSet<AccountUser> AccountUsers { get; set; }
    }
}
