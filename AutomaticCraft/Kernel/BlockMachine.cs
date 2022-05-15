using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using MC;
using LLNET.Event;
using LLNET.Hook;
using AutomaticCraft.Core;
using AutomaticCraft.Kernel.Interfaces;

namespace AutomaticCraft.Kernel
{
    public unsafe abstract class BlockMachine : Machine
    {
        protected BlockMachine(BlockPos position)
        {
            Position = position;

            machines.Add(this);
        }


        public BlockPos Position { get; protected set; }

        public ElectricInterface? Ele_X_Positive { get; protected set; }
        public ElectricInterface? Ele_X_Negative { get; protected set; }
        public ElectricInterface? Ele_Z_Positive { get; protected set; }
        public ElectricInterface? Ele_Z_Negative { get; protected set; }
        public ElectricInterface? Ele_Y_Positive { get; protected set; }
        public ElectricInterface? Ele_Y_Negative { get; protected set; }

        public ElectricInterface?[] Interfaces
        {
            get
            {
                return new ElectricInterface?[6]
                {
                    Ele_X_Positive,
                    Ele_X_Negative,
                    Ele_Y_Positive,
                    Ele_Y_Negative,
                    Ele_Z_Positive,
                    Ele_Z_Negative,
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
                    return Ele_X_Positive;
                if (pos.Y-Position.Y == 1)
                    return Ele_Y_Positive;
                if (pos.Z-Position.Z == 1)
                    return Ele_Z_Positive;
                if (pos.X-Position.X == -1)
                    return Ele_X_Negative;
                if (pos.Y-Position.Y == -1)
                    return Ele_Y_Negative;
                if (pos.Z-Position.Z == -1)
                    return Ele_Z_Negative;
                return null;
            }
        }

        public override string ToString()
        {
            return $"CurrentElectricity:{Storage:0.00},MaxCapacity:{MaxCapacity}";
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

        //static delegate* unmanaged<void*, uint, void*> SetTitlePacket_ctor__TitleType
        //    = (delegate* unmanaged<void*, uint, void*>)HookAPI.SYM("??0SetTitlePacket@@QEAA@W4TitleType@0@@Z");

        //static delegate* unmanaged<void*, uint, void*, void*> SetTitlePacket_ctor__TitleType_StdString
        //    = (delegate* unmanaged<void*, uint, void*, void*>)HookAPI.SYM("??0SetTitlePacket@@QEAA@W4TitleType@0@AEBV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z");

        //static delegate* unmanaged<void*, void**> SetTitlePacket_dctor
        //    = (delegate* unmanaged<void*, void**>)HookAPI.SYM("??1SetTitlePacket@@UEAA@XZ");

        //static delegate* unmanaged<void*, void*, void*> Player_SendNetWorkPacket__Packet
        //    = (delegate* unmanaged<void*, void*, void*>)HookAPI.SYM("?sendNetworkPacket@ServerPlayer@@UEBAXAEAVPacket@@@Z");



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
                        Level.RuncmdEx($"title \"{player.RealName}\" actionbar {machine}");
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
            if (item.Id == MinecraftGoldIngotItem.Id)
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
                            if (currentMachine.Ele_X_Positive != null && SelectedMachine.Ele_X_Negative != null)
                                ElectricInterface.Connect(currentMachine.Ele_X_Positive, SelectedMachine.Ele_X_Negative);
                            else
                                return false;
                        }
                        else
                        {
                            if (currentMachine.Ele_X_Negative != null && SelectedMachine.Ele_X_Positive != null)
                                ElectricInterface.Connect(currentMachine.Ele_X_Negative, SelectedMachine.Ele_X_Positive);
                            else
                                return false;
                        }
                        Logger.info.WriteLine($"{currentMachine.Name}[Pos:{currentMachine.Position}] & {SelectedMachine.Name}[Pos:{SelectedMachine.Position}] Connected");
                        SelectedMachine = null;
                        return true;
                    }

                    if (dy > dx && dy > dz)
                    {

                        if (currentMachine.Position.Y<SelectedMachine.Position.Y)
                        {
                            if (currentMachine.Ele_Y_Positive != null && SelectedMachine.Ele_Y_Negative != null)
                                ElectricInterface.Connect(currentMachine.Ele_Y_Positive, SelectedMachine.Ele_Y_Negative);
                            else
                                return false;
                        }
                        else
                        {
                            if (currentMachine.Ele_Y_Negative!= null && SelectedMachine.Ele_Y_Positive != null)
                                ElectricInterface.Connect(currentMachine.Ele_Y_Negative, SelectedMachine.Ele_Y_Positive);
                            else
                                return false;
                        }
                        Logger.info.WriteLine($"{currentMachine.Name}[Pos:{currentMachine.Position}] & {SelectedMachine.Name}[Pos:{SelectedMachine.Position}] Connected");
                        SelectedMachine = null;
                        return true;
                    }


                    if (dz > dx && dz > dy)
                    {

                        if (currentMachine.Position.Z<SelectedMachine.Position.Z)
                        {
                            if (currentMachine.Ele_Z_Positive != null && SelectedMachine.Ele_Z_Negative != null)
                                ElectricInterface.Connect(currentMachine.Ele_Z_Positive, SelectedMachine.Ele_Z_Negative);
                            else 
                                return false;
                        }
                        else
                        {
                            if (currentMachine.Ele_Z_Negative!= null && SelectedMachine.Ele_Z_Positive != null)
                                ElectricInterface.Connect(currentMachine.Ele_Z_Negative, SelectedMachine.Ele_Z_Positive);
                            else 
                                return false;
                        }
                        Logger.info.WriteLine($"{currentMachine.Name}[Pos:{currentMachine.Position}] & {SelectedMachine.Name}[Pos:{SelectedMachine.Position}] Connected");
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
