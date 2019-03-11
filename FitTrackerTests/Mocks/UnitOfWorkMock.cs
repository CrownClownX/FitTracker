using FitTracker.Core;
using FitTracker.Core.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerTests.Mocks
{
    public static class UnitOfWorkMock
    {
        public static Mock<IUnitOfWork> GetMockRepository(List<Activity> activities)
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

            mock.Setup(m => m.ActivityRepository.GetAll())
                .Returns(() => activities);

            mock.Setup(m => m.ActivityRepository.Get(It.IsAny<int>()))
                .Returns((int id) => activities
                    .SingleOrDefault(p => p.ID == id));

           /* mock.Setup(m => m.ActivityRepository.Find(It.IsAny<Expression<Func<Activity, bool>>>()))
                .Returns((Expression<Func<Activity, bool>> predicate) =>
                    activities.AsQueryable().Where(predicate));*/

            mock.Setup(m => m.ActivityRepository.Add(It.IsAny<Activity>()))
                .Callback((Activity entity) => activities.Add(entity))
                .Verifiable();

            mock.Setup(m => m.Complete())
                .Verifiable();

            return mock;
        }
    }
}
