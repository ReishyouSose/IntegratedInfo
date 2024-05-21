using IntegratedInfo.InfoUI.ExtraUI;
using Microsoft.Xna.Framework.Graphics;
using RUIModule.RUIElements;
using Terraria;
using Terraria.GameContent;
using static IntegratedInfo.MiscHelper;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel
    {
        private UICornerPanel recipePanel;
        private UIItemSlot focus;
        private UIContainerPanel recipeView;
        private void RegisterRecipePanel(UICornerPanel bg)
        {
            recipePanel = new();
            recipePanel.SetSize(-30, 0, 1, 1);
            recipePanel.Info.SetMargin(10);
            bg.Register(recipePanel);

            int top = 0;
            focus = new();
            focus.Events.OnLeftDown += evt =>
            {
                int type = Main.mouseItem.type;
                focus.item.SetDefaults(type);
                CheckRicpeFromItem(type);
            };
            recipePanel.Register(focus);
            top += 62;

            UICornerPanel recipeBg = new();
            recipeBg.SetPos(0, top);
            recipeBg.SetSize(0, -62, 1, 1);
            recipePanel.Register(recipeBg);

            recipeView = new(10);
            recipeView.SetSize(-30, 0, 1, 1);
            recipeView.autoPos = [5, 5];
            recipeView.VerticalEdge();
            recipeView.autoPosRule = innerUIEs =>
            {
                int x = 0, y = recipeView.GetEdgeBlur()[0],
                    h = recipeView.autoPos[0].Value, w = recipeView.autoPos[1].Value;
                bool first = true;
                foreach (BaseUIElement uie in innerUIEs)
                {
                    uie.SetPos(x, y);
                    if (uie is UIText)
                    {
                        if (!first)
                        {
                            x = 0;
                            y += 72 + h;
                            uie.SetPos(x, y);
                        }
                        x = 0;
                        y += 28;
                        if (first)
                            first = false;
                    }
                    else if (uie is UIImage)
                    {
                        y += 10;
                    }
                    else
                    {
                        if (x + uie.Width + w > recipeView.InnerWidth)
                        {
                            y += uie.Height + h;
                            uie.SetPos(0, y);
                            x = uie.Width + w;
                        }
                        else
                            x += uie.Width + w;
                    }
                }
            };
            recipeBg.Register(recipeView);

            VerticalScrollbar rv = new(62, true);
            recipeView.SetVerticalScrollbar(rv);
            recipeBg.Register(rv);
        }
        private void CheckRicpeFromItem(int type = 0)
        {
            if (type == 0)
                type = focus.item.type;
            recipeView.ClearAllElements();
            if (type == 0)
                return;

            UIText obtain = new(GTV("Info.Obtain"));
            obtain.SetSize(obtain.TextSize);
            recipeView.AddElement(obtain);

            Texture2D line = TextureAssets.MagicPixel.Value;
            UIImage ol = new(line);
            ol.SetSize(0, 2, 1);
            recipeView.AddElement(ol);

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == type)
                {
                    UIRecipeSlot slot = new(recipe);
                    AddRecipeSlotEvent(slot);
                    recipeView.AddElement(slot);
                }
            }

            UIText usedFor = new(GTV("Info.UsedFor"));
            usedFor.SetSize(usedFor.TextSize);
            recipeView.AddElement(usedFor);

            UIImage ul = new(line);
            ul.SetSize(0, 2, 1);
            recipeView.AddElement(ul);

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.ContainsIngredient(type))
                {
                    UIRecipeSlot slot = new(recipe);
                    AddRecipeSlotEvent(slot);
                    recipeView.AddElement(slot);
                }
            }
        }
        private void AddRecipeSlotEvent(UIRecipeSlot slot)
        {
            slot.Events.OnLeftDown += evt =>
            {
                if (slot.Avaliable)
                {
                    Main.focusRecipe = slot.recipe.RecipeIndex;
                }
            };
            slot.Events.OnLeftDoubleClick += evt =>
            {
                CheckRicpeFromItem(slot.recipe.createItem.type);
            };
        }
    }
}
