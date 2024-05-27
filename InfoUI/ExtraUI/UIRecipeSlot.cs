using Microsoft.Xna.Framework;
using RUIModule.RUIElements;
using System.Linq;
using Terraria;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UIRecipeSlot(Recipe recipe, bool shimmer = false) : UIItemSlot(recipe.createItem, 0.75f)
    {
        public Recipe recipe = recipe;
        public bool shimmer = shimmer;
        public bool Avaliable => Slot.slotID == 2;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Slot.slotID = Main.availableRecipe.Contains(recipe.RecipeIndex) ? 2 : 0;
        }

    }
}
