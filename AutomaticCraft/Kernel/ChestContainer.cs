using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MC;
using AutomaticCraft.Kernel.Interfaces;

namespace AutomaticCraft.Kernel
{
    public class ChestContainer : Container
    {
        public ChestContainer(BlockInstance instance) : base(instance)
        {
            
        }

        public ItemInterface? Item_X_Positive { get; protected set; }
        public ItemInterface? Item_X_Negative { get; protected set; }
        public ItemInterface? Item_Z_Positive { get; protected set; }
        public ItemInterface? Item_Z_Negative { get; protected set; }
        public ItemInterface? Item_Y_Positive { get; protected set; }
        public ItemInterface? Item_Y_Negative { get; protected set; }


        public override string Name => "ChestContainer";

        public override ulong Power => 0;

        public override ulong MaxCapacity => 0;

        public override void Operate()
        {
        }
    }
}
