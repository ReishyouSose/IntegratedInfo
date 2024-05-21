using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;
using Terraria.ModLoader;
using static IntegratedInfo.MiscHelper;

namespace IntegratedInfo.System
{
    public class InfoPlayer : ModPlayer
    {
        internal static ModKeybind check;
        public override void Load()
        {
            check = KeybindLoader.RegisterKeybind(Mod, "InfoCheck", Keys.P);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (check.JustPressed)
            {
                IPUI.OnInitialization();
                IPUI.Info.IsVisible = true;
            }
        }
    }
}
