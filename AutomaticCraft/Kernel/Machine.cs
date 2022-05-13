using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MC;
using LLNET.Schedule;

namespace AutomaticCraft.Kernel
{
    public abstract class Machine : Accumulator
    {
        public Machine()
        {
            machines.Add(this);
        }
        public abstract double Power { get; }

        public bool IsActive { get; protected set; } = false;

        public abstract void Operate();

        ////////////////static////////////////

        static Machine()
        {
            ScheduleAPI.Repeat(Tick, 1);
        }

        static readonly List<Machine> machines = new();

        static void Tick()
        {
            foreach(var machine in machines)
            {
                if (machine.IsActive)
                {
                    machine.Operate();
                }
            }
        }
    }
}
