using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class SatelliteData
    {
        public IDictionary<int, string> AtomicRules { get; private set; } = new Dictionary<int, string>();
        public IDictionary<int, IList<IList<int>>> SubRules { get; private set; } = new Dictionary<int, IList<IList<int>>>();
        public IList<string> Messages { get; private set; } = new List<string>();
        public SatelliteData(
            IDictionary<int, string> atomicRules,
            IDictionary<int, IList<IList<int>>> subRules,
            IList<string> messages)
        {
            AtomicRules = atomicRules;
            SubRules = subRules;
            Messages = messages;
        }
    }
}
