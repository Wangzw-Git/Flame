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

        TimerScheduler GetData(int id);

        void Insert(TimerScheduler entity);

        void Update(TimerScheduler entity);

        void Delete(TimerScheduler entity);
        
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

        public TimerScheduler GetData(int id)
        {
            return _dbContext.Set<TimerScheduler>().Find(id);
        }
        public void Insert(TimerScheduler entity)
        {
            _dbContext.Set<TimerScheduler>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TimerScheduler entity)
        {
            _dbContext.SaveChanges();
        }

        public void Delete(TimerScheduler entity)
        {
            _dbContext.Set<TimerScheduler>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
