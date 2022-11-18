using System;
using CountryVATCalculator.Commands;
using NUnit.Framework;

namespace CountryVATRateCalculator.Tests.Unit.Commands
{
    /// <summary>
    /// Unit Tests for <see cref="SafeCallServiceLogMetadata"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceLogMetadataUnitTests
    {
        public SafeCallServiceLogMetadata Metadata { get; set; }

        [Test]
        public void Constuct_Successfully()
        {
            Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceLogMetadataUnitTests), "Unit Tests");
        }
    }
}
