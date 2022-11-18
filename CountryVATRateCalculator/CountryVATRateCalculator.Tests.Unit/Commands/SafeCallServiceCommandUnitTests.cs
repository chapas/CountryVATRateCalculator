using System;
using System.Threading.Tasks;
using CountryVATCalculator.Commands;
using NUnit.Framework;

namespace CountryVATRateCalculator.Tests.Unit.Commands
{
    /// <summary>
    /// Unit Tests for <see cref="SafeCallServiceCommand"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceCommandUnitTests
    {
        public SafeCallServiceCommand Command { get; set; }

        public SafeCallServiceLogMetadata Metadata { get; set; }

        [Test]
        public void Construct_Successfully()
        {
            Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceCommandUnitTests), "Unit Test");
            Command = new SafeCallServiceCommand(() => Task.CompletedTask, Metadata);
        }

    }
}
