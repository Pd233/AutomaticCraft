using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel
{
    public abstract class MachineInterface<T> : InterfaceBase<T> where T : InterfaceBase<T>
    {
        public Machine Machine { get; protected set; }
        protected MachineInterface(Machine machine, InterfaceConnectionMode mode)
            : base(mode)
        {
            Machine = machine;
        }
    }
}
