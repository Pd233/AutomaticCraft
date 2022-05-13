using MC;
using LLNET.Event;
using LLNET.Logger;
using LLNET.DynamicCommand;
using AutomaticCraft;
using AutomaticCraft.Kernel;

namespace AutomaticCraft
{
    public abstract class AutomaticCraftBase
    {
        public static Logger Logger { get; protected set; } = new("AutomaticCraft");

        public static double TestFunction(double x)
        {
            return 100 - 10000/(x+100);
        }
    }
}

namespace PluginMain
{
    public class Plugin : AutomaticCraftBase
    {
        public unsafe static void OnPostInit()
        {

            PlayerUseItemOnEvent.Event += ev =>
            {
                using var block = ev.BlockInstance.Block;
                using var item = ev.ItemStack;
                Logger.info.WriteLine("BlockId:{0},BlockName:{1},ItemId:{2},ItemName:{3}", block.Id, block.TypeName, item.Id, item.TypeName);
                return true;
            };

            PlayerChatEvent.Event += ev =>
            {
                if (ev.Message == "Recipe")
                {
                    AutomaticCraft.RecipeHelper.Recipes.Test(new IntPtr(ev.Player.Level.NativePtr));
                }
                return true;
            };

            FurnaceElectricGenerator.Setup();
            SeaLanternBatery.Setup();
        }
    }
}