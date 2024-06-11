using RUIModule.RUIElements;
using Terraria.GameContent.ItemDropRules;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UIDropItem : BaseUIElement
    {
        public readonly UIItemSlot itemSlot;
        public readonly UIText dropRate;
        public UIDropItem(DropRateInfo drInfo, float scale = 0.75f)
        {

            itemSlot = new(new(drInfo.itemId), scale);
            Register(itemSlot);
            float size = 52 * scale;
            SetSize(size, size + 28);

            dropRate = new(drInfo.dropRate.ToString());
            dropRate.SetPos(0, -28, 0, 1);
            Register(dropRate);
        }
    }
}
