using System;
using Moq;
using ILogger = Serilog.ILogger;

namespace CountryVATRateCalculator.UnitTests.Helpers
{
    public static class LoggerStub
    {
        public static Mock<ILogger> GetLogger()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Debug(It.IsAny<string>()));
            logger.Setup(x => x.Information(It.IsAny<string>()));
            logger.Setup(x => x.Warning(It.IsAny<string>()));
            logger.Setup(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()));
            return logger;
        }
    }
}
