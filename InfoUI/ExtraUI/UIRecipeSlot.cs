using Microsoft.Xna.Framework;
using RUIModule.RUIElements;
using System.Linq;
using Terraria;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UIRecipeSlot(Recipe recipe) : UIItemSlot(recipe.createItem)
    {
        public Recipe recipe = recipe;
        public bool Avaliable => Slot.slotID == 2;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Slot.slotID = Main.availableRecipe.Contains(recipe.RecipeIndex) ? 2 : 0;
        }

    }
}
