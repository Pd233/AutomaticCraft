using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MC;

namespace AutomaticCraft.Kernel
{
    public abstract class BlockMachine : Machine
    {
        protected BlockMachine(BlockPos position)
        {
            Position = position;
        }


        public BlockPos Position { get; protected set; }
    }
}
