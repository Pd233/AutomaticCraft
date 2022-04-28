using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel
{
    public abstract class Accumulator : AutomaticCraftBase
    {
        public abstract ulong MaxCapacity { get; }
        public ulong Storage { get; protected set; }
        public ulong SpareCapacity { get => MaxCapacity - Storage; }
    }
}
