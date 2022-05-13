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
    public abstract class BlockMachine : Machine
    {
        protected BlockMachine(BlockPos position)
        {
            Position = position;

            machines.Add(this);
        }


        public BlockPos Position { get; protected set; }

        public ElectricInterface? X_Positive { get; protected set; }
        public ElectricInterface? X_Negative { get; protected set; }
        public ElectricInterface? Z_Positive { get; protected set; }
        public ElectricInterface? Z_Negative { get; protected set; }
        public ElectricInterface? Y_Positive { get; protected set; }
        public ElectricInterface? Y_Negative { get; protected set; }

        public ElectricInterface?[] Interfaces
        {
            get
            {
                return new ElectricInterface?[6]
                {
                    X_Positive,
                    X_Negative,
                    Y_Positive,
                    Y_Negative,
                    Z_Positive,
                    Z_Negative,
                };
            }
        }

        static double Distance(BlockPos a, BlockPos b)
        {
            return Math.Sqrt((a.X-b.X)*(a.X-b.X)+(a.Y-b.Y)*(a.Y-b.Y)+(a.Z-b.Z)*(a.Z-b.Z));
        }

        public ElectricInterface? SelectInterface(BlockPos pos)
        {
            if (Distance(pos, Position) != 1)
                return null;
            else
            {
                if (pos.X-Position.X == 1)
                    return X_Positive;
                if (pos.Y-Position.Y == 1)
                    return Y_Positive;
                if (pos.Z-Position.Z == 1)
                    return Z_Positive;
                if (pos.X-Position.X == -1)
                    return X_Negative;
                if (pos.Y-Position.Y == -1)
                    return Y_Negative;
                if (pos.Z-Position.Z == -1)
                    return Z_Negative;
                return null;
            }
        }

        public override string ToString()
        {
            return string.Format("Pos:{0},CurrentElectricity:{1:.00},MaxCapacity:{2}", Position, Storage, MaxCapacity);
        }

        protected virtual string DebugInfo()
        {
            return ToString();
        }

        ////////////////static////////////////

        static BlockMachine()
        {
            PlayerUseItemOnEvent.Event += OnDebugStickUse;
            PlayerUseItemOnEvent.Event += __ConnectMachine;
        }

        static readonly List<BlockMachine> machines = new();

        static bool OnDebugStickUse(PlayerUseItemOnEvent ev)
        {
            using var item = ev.ItemStack;
            if (item.Id == MinecraftStickItem.Id)
            {
                using var instance = ev.BlockInstance;
                using var pos = instance.Position;
                using var player = ev.Player;

                foreach (var machine in machines)
                {
                    if (pos == machine.Position)
                    {
                        player.SendText(machine.DebugInfo(), TextType.RAW);
                        break;
                    }
                }
            }

            return true;
        }


        static BlockMachine? SelectedMachine;
        static bool __ConnectMachine(PlayerUseItemOnEvent ev)
        {
            using var item = ev.ItemStack;
            if (item.Id == MinecraftNetherStarItem.Id)
            {
                using var instance = ev.BlockInstance;
                using var pos = instance.Position;

                var currentMachine = SelectedMachine;

                foreach (var machine in machines)
                {
                    if (pos == machine.Position)
                    {
                        SelectedMachine = machine;
                        break;
                    }
                }

                if (currentMachine != null && SelectedMachine!= null && currentMachine != SelectedMachine)
                {
                    var dx = Math.Abs(currentMachine.Position.X - SelectedMachine.Position.X);
                    var dy = Math.Abs(currentMachine.Position.Y - SelectedMachine.Position.Y);
                    var dz = Math.Abs(currentMachine.Position.Z - SelectedMachine.Position.Z);

                    if (dx > dy && dx > dz)
                    {


                        if (currentMachine.Position.X<SelectedMachine.Position.X)
                        {
                            if (currentMachine.X_Positive != null && SelectedMachine.X_Negative != null)
                                ElectricInterface.Connect(currentMachine.X_Positive, SelectedMachine.X_Negative);
                        }
                        else
                        {
                            if (currentMachine.X_Negative != null && SelectedMachine.X_Positive != null)
                                ElectricInterface.Connect(currentMachine.X_Negative, SelectedMachine.X_Positive);
                        }
                        SelectedMachine = null;
                        return true;
                    }

                    if (dy > dx && dy > dz)
                    {

                        if (currentMachine.Position.Y<SelectedMachine.Position.Y)
                        {
                            if (currentMachine.Y_Positive != null && SelectedMachine.Y_Negative != null)
                                ElectricInterface.Connect(currentMachine.Y_Positive, SelectedMachine.Y_Negative);
                        }
                        else
                        {
                            if (currentMachine.Y_Negative!= null && SelectedMachine.Y_Positive != null)
                                ElectricInterface.Connect(currentMachine.Y_Negative, SelectedMachine.Y_Positive);
                        }
                        SelectedMachine = null;
                        return true;
                    }


                    if (dz > dx && dz > dy)
                    {

                        if (currentMachine.Position.Z<SelectedMachine.Position.Z)
                        {
                            if (currentMachine.Z_Positive != null && SelectedMachine.Z_Negative != null)
                                ElectricInterface.Connect(currentMachine.Z_Positive, SelectedMachine.Z_Negative);
                        }
                        else
                        {
                            if (currentMachine.Z_Negative!= null && SelectedMachine.Z_Positive != null)
                                ElectricInterface.Connect(currentMachine.Z_Negative, SelectedMachine.Z_Positive);
                        }
                        SelectedMachine = null;
                        return true;
                    }
                }

            }
            return true;
        }

        public static bool TryGetBlockMachine(in BlockPos pos, out BlockMachine? machine)
        {
            foreach (var m in machines)
            {
                if (m.Position == pos)
                {
                    machine = m;
                    return true;
                }
            }
            machine = null;
            return false;
        }
    }
}
