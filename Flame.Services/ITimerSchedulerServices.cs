using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Data;

namespace Flame.Services
{
    public interface ITimerSchedulerServices
    {
        IQueryable<TimerScheduler> GetList();
    }

    public class TimerSchedulerServices : ITimerSchedulerServices
    {
        private readonly IDbContext _dbContext;

        public TimerSchedulerServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TimerScheduler> GetList()
        {
            return _dbContext.Set<TimerScheduler>();
        }
    }
}
