using System;

namespace CountryVATCalculator.Commands
{
    public class SafeCallServiceLogMetadata
    {
        public Guid CorrelationId { get; private set; }
        public Type Context { get; private set; }
        public string Name { get; private set; }

        public SafeCallServiceLogMetadata(Guid correlationId, Type context, string name)
        {
            CorrelationId = correlationId;
            Context = context ?? throw new ArgumentNullException(nameof(context), $"Require {nameof(context)} to reference source in logs");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"Require {nameof(name)} to reference source in logs");
            }

            Name = name;
        }
    }
}
