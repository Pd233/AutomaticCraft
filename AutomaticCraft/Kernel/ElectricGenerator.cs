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
            X_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            X_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Z_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Z_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Y_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
            Y_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Output);
        }
    }
}
