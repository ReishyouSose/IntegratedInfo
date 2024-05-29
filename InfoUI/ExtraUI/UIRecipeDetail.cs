using RUIModule.RUIElements;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UIRecipeDetail : UICornerPanel
    {
        public readonly Recipe recipe;
        public UIRecipeDetail(Recipe recipe) : base()
        {
            Info.SetMargin(5);
            this.recipe = recipe;
            int x = 0, y = 0;
            UIItemSlot result = new(recipe.createItem, 0.75f);
            Register(result);
            x += 44;

            UIImage vline = new(TextureAssets.MagicPixel.Value);
            vline.SetSize(2, 35);
            vline.SetPos(x, 2);
            Register(vline);
            x += 7;

            int count = recipe.requiredTile.Count;
            if (count > 0)
            {
                foreach (int tile in recipe.requiredTile)
                {
                    UITileSlot slot = new(tile, 0, 0.75f);
                    slot.SetPos(x, y);
                    Register(slot);
                    x += 44;
                }
            }
            else
            {
                UIIconSlot slot = new(TextureAssets.Item[ItemID.PowerGlove].Value, 2, 0.75f)
                {
                    hoverText = MiscHelper.GTV("Info.Hand")
                };
                slot.SetPos(x, y);
                x += 44;
                Register(slot);
            }

            //Info.Width.Pixel = x + 5;
            /* x = 0;
             y = 44;
             UIImage hline = new(TextureAssets.MagicPixel.Value);
             hline.SetSize(-4, 2, 1);
             hline.SetPos(2, y);
             Register(hline);
             y += 7;*/

            int w = count;
            count = recipe.requiredItem.Count;
            w = count >= w * 2 ? ((int)Math.Round(count / 2f)) : -1;
            foreach (Item item in recipe.requiredItem)
            {
                UIItemSlot slot = new(item, 0.75f) { hoverText = item.type.ToString() };
                slot.SetPos(x, y);
                Register(slot);
                x += 44;
                /*Info.Width.Pixel = Math.Max(Info.Width.Pixel, x * 44 + 5);
                Info.Height.Pixel = y + 49;*/
                /*if (w > 0 && x + 1 > w)
                {
                    x = 0;
                    y += 44;
                }*/
            }
            /*foreach (int index in recipe.acceptedGroups)
            {
                RecipeGroup group = RecipeGroup.recipeGroups[index];
            }*/
            SetSize(x + 5, 49);
        }
    }
}
