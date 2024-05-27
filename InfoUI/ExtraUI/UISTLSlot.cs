using RUIModule.RUIElements;
using RUIModule.RUISys;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UISTLSlot : UIPanel
    {
        public readonly UIItemSlot origin, target;
        public UISTLSlot(int origin, int target) : base()
        {
            Register(new UIItemSlot(new(origin), 0.75f));
            UIItemSlot tar = new(new(target), 0.75f);
            tar.SetPos(-52, 0, 1);
            Register(tar);
            UIImage line = new(AssetLoader.Increase);
            line.SetCenter(0, 0, 0.5f, 0.5f);
            Register(line);
            SetSize(tar.Width * 2 + 20, tar.Height);
        }
    }
}
