using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLNET.Event;
using MC;

using AutomaticCraft.Core;

namespace AutomaticCraft.Kernel
{
    public class FurnaceElectricGenerator : ElectricGenerator
    {
        //////////////////////////////////////////////////////

        static FurnaceElectricGenerator()
        {
            FurnaceBlockClass.OnSetLit += GeneratorUpdate;
        }

        //////////////////////////////////////////////////////


        public FurnaceElectricGenerator(BlockPos position) : base(position)
        {
        }

        public override double Power => 0.2;

        public override ulong MaxCapacity => 10000;

        public void Setup()
        {

        }
        //////////////////////////////////////////////////////

        static readonly List<FurnaceElectricGenerator> FurnaceGenerators = new();

        static BlockPos p = BlockPos.ZERO;

        public static bool PlayerUseItemOn_Event(PlayerUseItemOnEvent ev)
        {
            using var blockinstance = ev.mBlockInstance;
            using var pos = blockinstance.Position;
            if (p == pos)
                return true;
            else
            {
                p = pos;
                using var item = ev.mItemStack;
                using var player = ev.mPlayer;

                TryCreateGenrator(item, player, blockinstance);
            }
            return true;
        }

        static void TryCreateGenrator(ItemStack item, Player player, BlockInstance block)
        {
            if (item.Id != MinecraftNetherStarItem.Id)
                return;
            else
            {
                using var pos = block.Position;
                bool signal = true;

                foreach (var generator in FurnaceGenerators)
                {
                    if (generator.Position == pos)
                        signal = false;
                }

                if (signal)
                {
                    FurnaceGenerators.Add(new(pos));
                }
            }
        }

        static void GeneratorUpdate(char a1, BlockSource source, BlockPos pos, uint a4, long a5, long a6)
        {

            foreach (var item in FurnaceGenerators)
            {
                if (item.Position == pos)
                {
                    item.IsActive = !item.IsActive;
                    return;
                }
            }
        }

        //////////////////////////////////////////////////////

    }
}
