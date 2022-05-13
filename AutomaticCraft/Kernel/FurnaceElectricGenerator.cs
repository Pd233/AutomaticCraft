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
        public FurnaceElectricGenerator(BlockPos position) : base(position)
        {
        }

        public override double Power => 0.2;

        public override double MaxCapacity => 1000;

        public override void Operate()
        {
            if (SpareCapacity > Power)
                Storage += Power;
        }

        ////////////////static////////////////

        public static void Setup()
        {
            MinecraftFurnaceBlock.OnSetLit += GeneratorUpdate;
            PlayerUseItemOnEvent.Event += PlayerUseItemOn_Event;
        }

        static readonly List<FurnaceElectricGenerator> FurnaceGenerators = new();

        //static BlockPos p = BlockPos.ZERO;

        public static bool PlayerUseItemOn_Event(PlayerUseItemOnEvent ev)
        {
            using var blockinstance = ev.BlockInstance;
            using var block = blockinstance.Block;

            if (block.Id != MinecraftFurnaceBlock.Id)
                return true;

            using var pos = blockinstance.Position;

            using var item = ev.ItemStack;
            using var player = ev.Player;

            TryCreateGenrator(item, player, blockinstance);

            return true;
        }

        static void TryCreateGenrator(ItemStack item, Player player, BlockInstance block)
        {
            if (item.Id != MinecraftNetherStarItem.Id)
                return;
            else
            {
                var pos = block.Position;
                bool signal = true;

                foreach (var generator in FurnaceGenerators)
                {
                    if (generator.Position == pos)
                        signal = false;
                }

                if (signal)
                {
                    FurnaceGenerators.Add(new(pos));
                    Logger.info.WriteLine("Create FurnaceElectricGenerator");
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


    }
}
