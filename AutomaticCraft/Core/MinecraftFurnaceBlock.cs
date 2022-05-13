using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PluginMain;
using LLNET.Hook;
using LLNET.Logger;
using MC;

namespace AutomaticCraft.Core
{
    public class MinecraftFurnaceBlock : AutomaticCraftBase
    {
        static MinecraftFurnaceBlock()
        {
            Logger.warn.WriteLine("Setup FurnaceBlockClass.");
            Thook.RegisterHook<FurnaceBlockClass_SetLit_Hook, SetLit>();
        }

        public const int Id = 61;

        public delegate void SetLitEventHandler(char a1, BlockSource source, BlockPos pos, uint a4, long a5, long a6);

        public static event SetLitEventHandler? OnSetLit;

        public delegate IntPtr SetLit(char a1, IntPtr blockSource, IntPtr blockPos, uint a4, long a5, long a6);
        [HookSymbol("?setLit@FurnaceBlock@@SAX_NAEAVBlockSource@@AEBVBlockPos@@W4BlockActorType@@AEBVBlock@@4@Z")]
        class FurnaceBlockClass_SetLit_Hook : THookBase<SetLit>
        {
            IntPtr SetLitHookFn(char a1, IntPtr blockSource, IntPtr blockPos, uint a4, long a5, long a6)
            {

                Console.WriteLine("QAQ");

                var bp = new BlockPos(blockPos);
                var bs = new BlockSource(blockSource);

                try
                {
                    OnSetLit?.Invoke(a1, bs, bp, a4, a5, a6);
                }
                catch (Exception ex)
                {
                    Logger.error.WriteLine("Uncaught Exception detected!");
                    Logger.error.WriteLine("Info:{0}", ex);
                }
                return Original(a1, blockSource, blockPos, a4, a5, a6);
            }
            public override void Setup()
            {
                Hook = SetLitHookFn;
            }
        }
    }
}
