using RUIModule.RUIElements;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UIRecipeDetail : UICornerPanel
    {
        public readonly Recipe recipe;
        public UIRecipeDetail(Recipe recipe) : base()
        {
            this.recipe = recipe;
            int x = 0;
            if (recipe.requiredTile.Count > 0)
            {
                foreach (int tile in recipe.requiredTile)
                {
                    int item = TileLoader.tileTypeAndTileStyleToItemType[(tile, 0)];
                    if (item > ItemID.None)
                    {
                        UIItemSlot slot = new(new(item), 0.75f);
                        Register(slot);
                        x++;
                    }
                }
            }
            else
            {
                UIIconSlot slot = new(TextureAssets.Item[ItemID.PowerGlove].Value, 2, 0.75f)
                {
                    hoverText = MiscHelper.GTV("Info.Hand")
                };
                Register(slot);
                x = 1;
            }
            foreach (Item item in recipe.requiredItem)
            {

            }
            foreach (int index in recipe.acceptedGroups)
            {
                RecipeGroup group = RecipeGroup.recipeGroups[index];
            }
        }
    }
}
