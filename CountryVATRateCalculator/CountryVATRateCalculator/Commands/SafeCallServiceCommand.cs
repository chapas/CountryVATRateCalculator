using System;
using System.Threading.Tasks;

namespace CountryVATCalculator.Commands
{
    public class SafeCallServiceCommand
    {
        public Func<Task> Func { get; private set; }
        public SafeCallServiceLogMetadata Metadata { get; private set; }

        public SafeCallServiceCommand(Func<Task> func, SafeCallServiceLogMetadata metadata)
        {
            Func = func ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(func)} to execute");
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata), $"Require {nameof(metadata)} for logging");
        }
    }
}
