﻿using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticCraft.Kernel.Interfaces;

namespace AutomaticCraft.Kernel
{
    public abstract class ElectricGenerator : BlockMachine
    {
        protected ElectricGenerator(BlockPos position)
            : base(position)
        {
            Ele_X_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Ele_X_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Ele_Z_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Ele_Z_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Ele_Y_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Ele_Y_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
        }
    }
}
