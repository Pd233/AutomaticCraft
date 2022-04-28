using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel.Interfaces
{
    public class ElectricInterface : MachineInterface<ElectricInterface>
    {
        public ElectricInterface(Machine machine, InterfaceConnectionMode mode)
            : base(machine, mode)
        {
        }
    }
}
