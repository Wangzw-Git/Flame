using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Flame.Data
{
    public partial class FlameContext : DbContext
    {
        public FlameContext()
            : base("name=FlameContext")
        {
        }

        public virtual DbSet<TimerScheduler> TimerScheduler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimerScheduler>()
                .Property(e => e.Ts_NameSpace)
                .IsUnicode(false);
        }
    }
}
