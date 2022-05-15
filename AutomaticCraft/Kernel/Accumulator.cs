using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel
{
    public abstract class Accumulator : AutomaticCraftBase
    {
        public abstract ulong MaxCapacity { get; }
        public ulong Storage { get; protected set; }
        public ulong SpareCapacity { get => MaxCapacity - Storage; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void Charge(ref ulong e)
        {
            if (SpareCapacity >= e)
            {
                Storage += e;
                e = 0;
            }
            else
            {
                var val = e - SpareCapacity;
                Storage += val;
                e-= val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ulong DisCharge(ulong e)
        {
            ulong ret;
            if (Storage >= e)
            {
                ret = e;
                Storage -= e;
            }
            else
            {
                ret = Storage;
                Storage = 0;
            }
            return ret;
        }
    }
}
