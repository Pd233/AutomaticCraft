using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MC;

namespace AutomaticCraft.Kernel
{
    public abstract class Machine : Accumulator
    {
        public abstract double Power { get; }

        public bool IsActive { get; protected set; } = false;
    }
}
