using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MC;
using LLNET.Event;
using AutomaticCraft.Core;
using AutomaticCraft.Kernel.Interfaces;

namespace AutomaticCraft.Kernel
{
    public class Wire
    {
        public Wire(BlockPos pos)
        {
            this.pos = pos;
        }

        readonly BlockPos pos;

        List<BlockMachine> NearMachines
        {
            get
            {
                List<BlockMachine> ret = new();
                BlockPos[] arr = new BlockPos[6]
                    {
                        BlockPos.Create(pos.X-1, pos.Y, pos.Z),
                        BlockPos.Create(pos.X+1, pos.Y, pos.Z),
                        BlockPos.Create(pos.X, pos.Y-1, pos.Z),
                        BlockPos.Create(pos.X, pos.Y+1, pos.Z),
                        BlockPos.Create(pos.X, pos.Y, pos.Z-1),
                        BlockPos.Create(pos.X, pos.Y, pos.Z+1),
                    };
                foreach (var pos in arr)
                {
                    if (BlockMachine.TryGetBlockMachine(pos, out var machine))
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                        ret.Add(machine);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
                    pos.Dispose();
                }
                return ret;
            }
        }

        ////////////////static////////////////

        static readonly List<BlockPos> wires = new List<BlockPos>();

        static bool TryConnectWire(PlayerUseItemOnEvent ev)
        {
            using var item = ev.ItemStack;
            if (item.Id != MinecraftNetherStarItem.Id)
                return true;
            else
            {
                using var instance = ev.BlockInstance;
                using var block = instance.Block;
                if (block.Id != MinecraftIronBarBlock.Id)
                    return true;

                else
                {
                    using var pos = instance.Position;

                    var wire = new Wire(pos);
                    var machines = wire.NearMachines;

                    var interfaces = new List<ElectricInterface>();

                    foreach (var machine in machines)
                    {
                        foreach (var _interface in machine.Interfaces)
                        {
                            if (_interface != null)
                                interfaces.Add(_interface);
                        }
                    }
                }
            }


            return true;
        }


    }
}
