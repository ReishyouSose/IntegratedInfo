using IntegratedInfo.InfoUI;
using RUIModule.RUISys;
using Terraria.Localization;

namespace IntegratedInfo
{
    public static class MiscHelper
    {
        public const string LocalKey = "Mods.IntegratedInfo.";
        public static string GTV(string key, params object[] args)
        {
            if (args == null || args.Length == 0)
                return Language.GetTextValue(LocalKey + key);
            return Language.GetText(LocalKey + key).WithFormatArgs(args).Value;
        }
        public static InfoPanel IPUI => RUISystem.Ins.Elements[InfoPanel.LocalKey] as InfoPanel;
    }
}
