using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel
{
    public abstract class Container : BlockMachine
    {
        public Container(BlockInstance instance) : base(instance.Position)
        {
            if (!instance.HasContainer)
                throw new Exception();

            Container_ = instance.Container;
        }

        public MC.Container Container_ { get; protected set; }
    }
}
