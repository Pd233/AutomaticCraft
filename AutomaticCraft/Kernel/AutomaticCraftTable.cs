using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticCraft.Kernel.Interfaces;


namespace AutomaticCraft.Kernel
{
    public class AutomaticCraftTable : BlockMachine
    {
        public AutomaticCraftTable(BlockPos position) : base(position)
        {
            X_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            X_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Z_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Z_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
        }

        public override double Power => 10;

        public override double MaxCapacity => 500;

        public override void Operate()
        {
            throw new NotImplementedException();
        }
    }
}
