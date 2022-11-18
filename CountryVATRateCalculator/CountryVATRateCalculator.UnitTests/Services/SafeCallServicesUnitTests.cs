using System;
using System.Threading.Tasks;
using CountryVATCalculator.Commands;
using CountryVATCalculator.Services;
using CountryVATRateCalculator.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using ILogger = Serilog.ILogger;

namespace CountryVATRateCalculator.UnitTests.Services
{
    [TestFixture]
    public class SafeCallServicesTests
    {
        public SafeCallService CallService { get; private set; }
        public Mock<ILogger> MockLogger { get; private set; }

        [SetUp]
        public void Setup()
        {
            MockLogger = LoggerStub.GetLogger();
            CallService = new SafeCallService(MockLogger.Object);
        }

        /// <summary>
        /// Given IoC has valid dependencies available
        /// When constructing the <see cref="SafeCallService"/>
        /// Then do not throw any exceptions
        /// </summary>
        [Test]
        public void Construction_WithValidParameters_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => new SafeCallService(MockLogger.Object));
        }

        /// <summary>
        /// Given an operation taking 1 second
        /// When calling <see cref="SafeCallService.Call(Func{Task}, Type)"/>
        /// Then log the start and end with the time of the operation
        /// </summary>
        [Test]
        public async Task Call_WithValidParameters_LogTiming()
        {
            var command = new SafeCallServiceCommand(
                () => Task.Delay(1000),
                new SafeCallServiceLogMetadata(
                    Guid.NewGuid(),
                    typeof(SafeCallService),
                    "Unit Tests"));

            await CallService.CallAsync(command);
        }
    }
}
