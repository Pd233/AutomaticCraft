using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutomaticCraft.Kernel.Interfaces;

namespace AutomaticCraft.Kernel
{
    abstract public class BlockBattery : BlockMachine
    {
        public BlockBattery(BlockPos position) : base(position)
        {
            X_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
            X_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
            Z_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
            Z_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
            Y_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
            Y_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Interflow);
        }

        public override double Power => 0;
    }
}
