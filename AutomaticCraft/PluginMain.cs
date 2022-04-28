using MC;
using LLNET.Event;
using LLNET.Logger;
using AutomaticCraft;

namespace AutomaticCraft
{
    public abstract class AutomaticCraftBase
    {
        public static Logger Logger { get; protected set; } = new("AutomaticCraft");
    }
}

namespace PluginMain
{
    public class Plugin : AutomaticCraftBase
    {
        public static void OnPostInit()
        {
            PlayerUseItemOnEvent.Event += ev =>
            {

                Logger.warn.WriteLine(ev.BlockInstance.Block.TypeName);

                return true;
            };
        }
    }
}