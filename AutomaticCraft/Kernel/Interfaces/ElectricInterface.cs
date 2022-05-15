using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LLNET.Schedule;

namespace AutomaticCraft.Kernel.Interfaces
{
    public class ElectricInterface : MachineInterface<ElectricInterface>
    {
        public ElectricInterface(Machine machine, InterfaceConnectionMode mode)
            : base(machine, mode)
        {
            ElectricInterfaces.Add(this);
        }

        static ElectricInterface()
        {
            ScheduleAPI.Repeat(Tick, 1);
        }

        static readonly List<ElectricInterface> ElectricInterfaces = new();

        static void Tick()
        {
            foreach (var electricInterface in ElectricInterfaces)
            {
                switch (electricInterface.ConnectionMode)
                {
                    case InterfaceConnectionMode.Input:
                        InputTick(electricInterface);
                        break;
                    case InterfaceConnectionMode.Output:
                        OutputTick(electricInterface);
                        break;
                    case InterfaceConnectionMode.Interflow:
                        InterflowTick(electricInterface);
                        break;
                }
            }
        }

        static void InputTick(ElectricInterface @interface)
        {
            if (@interface.Connection == null)
                return;

            var val = (ulong)TestFunction(@interface.Connection.Machine.Storage - @interface.Machine.Storage);
            if (val <= 0)
                return;

            var _val = val;

            @interface.Machine.Charge(ref val);
            @interface.Connection.Machine.DisCharge(_val - val);
        }

        static void OutputTick(ElectricInterface @interface)
        {
            if (@interface.Connection == null)
                return;

            var val = (ulong)TestFunction(@interface.Machine.Storage - @interface.Connection.Machine.Storage);
            if (val <= 0)
                return;

            var _val = val;

            @interface.Connection.Machine.Charge(ref val);
            @interface.Machine.DisCharge(_val - val);
        }

        static void InterflowTick(ElectricInterface @interface)
        {
            if (@interface.Connection == null)
                return;

            switch (@interface.Connection.ConnectionMode)
            {
                case InterfaceConnectionMode.Input:
                    InputTick(@interface.Connection);
                    break;
                case InterfaceConnectionMode.Output:
                    OutputTick(@interface.Connection);
                    break;
                case InterfaceConnectionMode.Interflow:
                    {
                        if(@interface.Machine.Storage > @interface.Connection.Machine.Storage)
                        {
                            OutputTick(@interface);
                        }
                        else
                        {
                            InputTick(@interface);
                        }
                    }
                    break;
            }
        }
    }
}
