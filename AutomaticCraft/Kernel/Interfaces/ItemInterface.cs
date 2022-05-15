using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel.Interfaces
{
    public class ItemInterface : MachineInterface<ItemInterface>
    {
        public ItemInterface(Machine machine, InterfaceConnectionMode mode) : base(machine, mode)
        {
        }
    }
}
