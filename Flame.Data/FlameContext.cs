using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace Flame.Data
{
    public class FlameContext : DbContext, IDbContext
    {
        public FlameContext() : base("name=FlameContext")
        {
        }

        public virtual DbSet<TimerScheduler> TimerScheduler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimerScheduler>()
                .Property(e => e.Ts_NameSpace)
                .IsUnicode(false);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }
    }
}
