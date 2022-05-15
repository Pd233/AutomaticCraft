using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticCraft.Kernel.Interfaces;

using LLNET.Event;
using AutomaticCraft.Core;


namespace AutomaticCraft.Kernel
{
    public class AutomaticCraftTable : BlockMachine
    {
        public AutomaticCraftTable(BlockPos position) : base(position)
        {
            Ele_X_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Input);
            Ele_X_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Input);
            Ele_Z_Positive = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Input);
            Ele_Z_Negative = new(this, InterfaceBase<ElectricInterface>.InterfaceConnectionMode.Input);
        }

        public override ulong Power => 100;

        public override ulong MaxCapacity => 5000;

        public override string Name => "AutomaticCraftTable";

        

        public override void Operate()
        {
            throw new NotImplementedException();
        }

        ////////////////static////////////////

        public static void Setup()
        {
            PlayerUseItemOnEvent.Event += PlayerUseItemOn_Event;
        }

        public static bool PlayerUseItemOn_Event(PlayerUseItemOnEvent ev)
        {
            using var blockinstance = ev.BlockInstance;
            using var block = blockinstance.Block;

            if (block.Id != MinecraftCraftingTableBlock.Id)
                return true;

            using var pos = blockinstance.Position;

            using var item = ev.ItemStack;
            using var player = ev.Player;

            TryCreateCraftTable(item, player, blockinstance);

            return true;

            
        }

        static readonly List<AutomaticCraftTable> AutomaticCraftTables = new();

        static void TryCreateCraftTable(ItemStack item, Player player, BlockInstance block)
        {
            if (item.Id != MinecraftNetherStarItem.Id)
                return;
            else
            {
                var pos = block.Position;
                bool signal = true;

                foreach (var generator in AutomaticCraftTables)
                {
                    if (generator.Position == pos)
                        signal = false;
                }

                if (signal)
                {
                    AutomaticCraftTables.Add(new(pos));
                    Logger.info.WriteLine("Create AutomaticCraftTable");
                }
            }
        }
    }
}
