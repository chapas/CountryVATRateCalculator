using System;
using System.Threading.Tasks;
using CountryVATCalculator.Commands;
using NUnit.Framework;

namespace CountryVATRateCalculator.Tests.Unit.Commands
{
    /// <summary>
    /// Unit tests for <see cref="SafeCallServiceQuery{T}"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceQueryUnitTests
    {
        public SafeCallServiceQuery<string> Query { get; set; }

        public SafeCallServiceLogMetadata Metadata { get; set; }

        public void Construct_Successfully()
        {
            Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceQueryUnitTests), "Unit Tests");
            Query = new SafeCallServiceQuery<string>(() => Task.FromResult("Success"), Metadata);
        }
    }
}
