using RUIModule.RUISys;
using Terraria.ModLoader;

namespace IntegratedInfo
{
    public class IntegratedInfo : Mod
    {
        public override void Load()
        {
            AddContent<RUISystem>();
        }
    }
}
