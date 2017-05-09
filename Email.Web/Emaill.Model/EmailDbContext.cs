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
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<LifeRecord> LifeRecords { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RecordLike> RecordLikes { get; set; }

        public DbSet<UserCommnet> UserCommnets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordLike>().HasKey(t => new { t.CommentUserId, t.RecordId });
            modelBuilder.Entity<UserCommnet>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }
    }
}
