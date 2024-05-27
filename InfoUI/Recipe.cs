using IntegratedInfo.InfoUI.ExtraUI;
using Microsoft.Xna.Framework;
using RUIModule.RUIElements;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static IntegratedInfo.MiscHelper;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel
    {
        private UIBottom recipePanel;
        private UIItemSlot focus;
        private UIContainerPanel recipeView, tileView, ingrediantView;
        private void RegisterRecipePanel(UICornerPanel bg)
        {
            recipePanel = new();
            recipePanel.SetPos(0, 62);
            recipePanel.SetSize(-30, -62, 1, 1);
            bg.Register(recipePanel);

            int bottom = 59;

            UICornerPanel recipeBg = new();
            recipeBg.SetSize(0, -bottom - 10, 1, 1);
            recipePanel.Register(recipeBg);

            recipeView = new(10);
            recipeView.SetSize(-20, 0, 1, 1);
            recipeView.autoPos = [5, 5];
            recipeView.VerticalEdge();
            recipeView.autoPosRule = innerUIEs =>
            {
                int count = innerUIEs.Count;
                if (count == 0)
                    return;
                int x = 0, y = recipeView.GetEdgeBlur()[0],
                    h = recipeView.autoPos[0].Value, w = recipeView.autoPos[1].Value;
                for (int i = 0; i < count; i++)
                {
                    BaseUIElement uie = innerUIEs[i];
                    if (uie is UIText)
                    {
                        uie.SetPos(x, y);
                        y += 48;
                        if (i + 1 < count && innerUIEs[i + 1] is UIImage)
                        {
                            y -= 20;
                        }
                    }
                    else if (uie is UIImage)
                    {
                        uie.SetPos(x, y);
                        y += 10;
                    }
                    else
                    {
                        if (x + uie.Width > recipeView.InnerWidth)
                        {
                            x = 0;
                            y += uie.Height + h;
                        }
                        uie.SetPos(x, y);
                        x += uie.Width + w;
                        if (i + 1 < count && innerUIEs[i + 1] is UIText)
                        {
                            x = 0;
                            y += uie.Height + 20;
                        }
                    }
                }
            };
            recipeBg.Register(recipeView);

            VerticalScrollbar rv = new(39);
            recipeView.SetVerticalScrollbar(rv);
            recipeBg.Register(rv);

            UICornerPanel requirePanel = new();
            requirePanel.SetPos(0, -bottom, 0, 1);
            requirePanel.SetSize(0, bottom, 1);
            requirePanel.Info.HiddenOverflow = true;
            recipePanel.Register(requirePanel);

            int left = 39 * 2 + 40;
            tileView = new(10);
            tileView.SetSize(left, 0, 0, 1);
            tileView.HorizonEdge();
            tileView.autoPos = [null, 5];
            tileView.DrawRec[0] = Color.White;
            requirePanel.Register(tileView);

            HorizontalScrollbar th = new(39);
            th.Info.Top.Pixel += 40;
            tileView.SetHorizontalScrollbar(th);
            requirePanel.Register(th);

            UIImage line = new(TextureAssets.MagicPixel.Value);
            line.SetSize(2, -20, 0, 1);
            line.SetPos(left, 10);
            requirePanel.Register(line);
            left += 2;

            ingrediantView = new(10);
            ingrediantView.SetPos(left, 0);
            ingrediantView.SetSize(-left, 0, 1, 1);
            ingrediantView.HorizonEdge();
            ingrediantView.autoPos = [null, 5];
            ingrediantView.DrawRec[0] = Color.White;
            requirePanel.Register(ingrediantView);

            HorizontalScrollbar ih = new(39);
            ih.Info.Top.Pixel += 40;
            ingrediantView.SetHorizontalScrollbar(ih);
            requirePanel.Register(ih);
        }
        private void CheckRicpeFromItem(int type = 0)
        {
            if (type == 0)
                type = focus.item.type;
            else
                focus.item.SetDefaults(type);
            recipeView.ClearAllElements();
            if (type == 0)
                return;

            HashSet<Recipe>[] targets = [[], [], []];

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.Disabled)
                    continue;
                if (recipe.createItem.type == type)
                {
                    targets[0].Add(recipe);
                }
                else if (recipe.ContainsIngredient(type))
                {
                    targets[1].Add(recipe);
                    if (!recipe.notDecraftable)
                    {
                        targets[2].Add(recipe);
                    }
                }
            }

            RegisterSlot("Obtain", targets[0], false);
            RegisterSlot("UsedFor", targets[1], false);
            RegisterSlot("Shimmer.To", ItemID.Sets.CraftingRecipeIndices[type], true);
            RegisterSlot("Shimmer.By", targets[2], true);
            TipAndLine("Shimmer.Transmutation");
            int trans = ItemID.Sets.ShimmerTransformToItem[type];
            bool any = false;
            if (trans > ItemID.None)
            {
                UISTLSlot slot = new(type, trans);
                recipeView.AddElement(slot);
                any = true;
            }
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                int target = ItemID.Sets.ShimmerTransformToItem[i];
                if (target == type)
                {
                    UISTLSlot slot = new(i, type);
                    recipeView.AddElement(slot);
                    any = true;
                }
            }
            if (!any)
            {
                AddNoneResult();
            }
        }
        private void TipAndLine(string key)
        {
            UIText tip = new(GTV("Info." + key));
            tip.SetSize(tip.TextSize);
            recipeView.AddElement(tip);

            UIImage line = new(TextureAssets.MagicPixel.Value);
            line.SetSize(0, 2, 1);
            recipeView.AddElement(line);
        }
        private void RegisterRecipeSlot(Recipe recipe, bool shimmer)
        {
            UIRecipeSlot slot = new(recipe, shimmer);
            if (slot.shimmer)
            {

            }
            else
            {
                slot.Events.OnLeftDown += evt =>
                {
                    Recipe target = slot.recipe;
                    if (slot.Avaliable)
                    {
                        Main.focusRecipe = target.RecipeIndex;
                    }
                    tileView.ClearAllElements();
                    if (target.requiredTile.Count > 0)
                    {
                        foreach (int tile in target.requiredTile)
                        {
                            int item = TileLoader.tileTypeAndTileStyleToItemType[(tile, 0)];
                            if (item > ItemID.None)
                            {
                                UIItemSlot slot = new(new(item), 0.75f);
                                tileView.AddElement(slot);
                            }
                        }
                    }
                    else
                    {
                        UIIconSlot slot = new(TextureAssets.Item[ItemID.PowerGlove].Value, 2, 0.75f)
                        {
                            hoverText = GTV("Info.Hand")
                        };
                        tileView.AddElement(slot);
                    }
                    ingrediantView.ClearAllElements();
                    foreach (Item item in target.requiredItem)
                    {
                        UIItemSlot slot = new(item, 0.75f);
                        ingrediantView.AddElement(slot);
                    }
                };
            }
            slot.Events.OnLeftDoubleClick += evt =>
            {
                CheckRicpeFromItem(slot.recipe.createItem.type);
            };
            recipeView.AddElement(slot);
        }
        private void RegisterSlot(string key, IEnumerable<Recipe> target, bool shimmer)
        {
            TipAndLine(key);
            if (target.Any())
            {
                foreach (Recipe recipe in target)
                    RegisterRecipeSlot(recipe, shimmer);
            }
            else
            {
                AddNoneResult();
            }
        }
        private void RegisterSlot(string key, IEnumerable<int> target, bool shimmer)
        {
            List<Recipe> result = [];
            foreach (int i in target)
            {
                result.Add(Main.recipe[i]);
            }
            RegisterSlot(key, result, shimmer);
        }

        private void AddNoneResult()
        {
            UIText none = new(GTV("Info.None"));
            none.SetSize(none.TextSize);
            recipeView.AddElement(none);
        }
    }
}
