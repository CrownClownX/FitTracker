using FitTracker.Core.Model;
using FitTracker.Helper;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerTests.Mocks
{
    public static class EventAggregatorMock
    {
        public static Mock<IEventAggregator> GetMockRepository()
        {
            var mock = new Mock<IEventAggregator>();
            var eventMock = new Mock<ActivityEvent>();

            mock.Setup(x => x.GetEvent<ActivityEvent>())
                .Returns(eventMock.Object);

            eventMock.Setup(x => x.Subscribe(It.IsAny<Action<int>>()))
                .Callback()


            return mock;
        }
    }
}
