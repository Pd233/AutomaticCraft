using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LLNET.Event;
using AutomaticCraft.Core;

namespace AutomaticCraft.Kernel
{
    public class SeaLanternBatery : BlockBattery
    {
        public SeaLanternBatery(BlockPos position) : base(position)
        {
        }

        public override ulong MaxCapacity => 50000;

        public override string Name => "SeaLanternBatery";

        public override void Operate()
        {
        }

        ////////////////static////////////////

        public static void Setup()
        {
            PlayerUseItemOnEvent.Event += PlayerUseItemOn_Event;
        }

        static readonly List<SeaLanternBatery> battery = new();

        public static bool PlayerUseItemOn_Event(PlayerUseItemOnEvent ev)
        {
            using var blockinstance = ev.BlockInstance;
            using var block = blockinstance.Block;

            if (block.Id != MinecraftSeaLanternBlock.Id)
                return true;

            using var pos = blockinstance.Position;

            using var item = ev.ItemStack;
            using var player = ev.Player;

            TryCreateBatery(item, player, blockinstance);

            return true;
        }

        static void TryCreateBatery(ItemStack item, Player player, BlockInstance block)
        {
            if (item.Id != MinecraftNetherStarItem.Id)
                return;
            else
            {
                var pos = block.Position;
                bool signal = true;

                foreach (var generator in battery)
                {
                    if (generator.Position == pos)
                        signal = false;
                }

                if (signal)
                {
                    battery.Add(new(pos));
                    Logger.info.WriteLine("Create SeaLanternBatery");
                }
            }
        }
    }
}
