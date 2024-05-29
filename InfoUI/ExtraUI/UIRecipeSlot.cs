using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public void DrawRecipe(SpriteBatch sb, Vector2 pos)
        {

        }
    }
}
