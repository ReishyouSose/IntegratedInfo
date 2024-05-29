using IntegratedInfo.InfoUI.ExtraUI;
using RUIModule.RUIElements;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel
    {
        private UIBottom sourcePanel;
        private UIContainerPanel sourceView;
        private UITileText hoverTile;
        private void RegisterSourcePanel(UICornerPanel bg)
        {
            sourcePanel = new();
            sourcePanel.SetPos(0, 62);
            sourcePanel.SetSize(-30, -62, 1, 1);
            bg.Register(sourcePanel);

            UICornerPanel sourceBg = new();
            sourceBg.SetSize(0, 0, 1, 1);
            sourcePanel.Register(sourceBg);

            sourceView = new(10);
            sourceView.SetSize(-20, 0, 1, 1);
            sourceView.VerticalEdge();
            sourceView.autoPos = [5, 5];
            AutoPosRule(sourceView);
            sourceBg.Register(sourceView);

            VerticalScrollbar sv = new(39);
            sourceView.SetVerticalScrollbar(sv);
            sourceBg.Register(sv);
        }
        private void CheckSourceFromItem(int type)
        {
            sourceView.ClearAllElements();
            if (type == 0)
                return;
            TipAndLine(sourceView, "Obtain");
            bool any = false;
            void CheckNone()
            {
                if (!any)
                    AddNoneResult(sourceView);
                any = false;
            }
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == type)
                {
                    any = true;
                    UIRecipeSlot slot = new(recipe);
                    sourceView.AddElement(slot);
                }
            }
            CheckNone();

            TipAndLine(sourceView, "Shimmer.By");
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                foreach (int index in ItemID.Sets.CraftingRecipeIndices[i])
                {
                    Recipe recipe = Main.recipe[index];
                    if (recipe.ContainsIngredient(type))
                    {
                        any = true;
                        UIItemSlot slot = new(new(i), 0.75f);
                        sourceView.AddElement(slot);
                    }
                }
            }
            CheckNone();

            TipAndLine(sourceView, "Shimmer.Transmutation");
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                if (ItemID.Sets.ShimmerTransformToItem[i] == type)
                {
                    any = true;
                    sourceView.AddElement(new UISTLSlot(i, type));
                }
            }
            CheckNone();

            TipAndLine(sourceView, "Dig");
            void RegisterTile(int type, int tile)
            {
                any = true;
                Main.instance.LoadTiles(tile);
                UITileText tileName = new(ContentSamples.ItemsByType[type].Name, tile);
                int t = tile;
                tileName.Events.OnMouseOver += evt => extraDraw += tileName.DrawTile;
                tileName.Events.OnMouseOut += evt => extraDraw -= tileName.DrawTile;
                sourceView.AddElement(tileName);
            }
            int createTile = ContentSamples.ItemsByType[type].createTile;
            if (createTile > 0)
            {
                RegisterTile(type, createTile);
            }
            else
            {
                foreach (var ((tile, style), item) in TileLoader.tileTypeAndTileStyleToItemType)
                {
                    if (item == type)
                    {
                        RegisterTile(item, tile);
                    }
                }
            }
            CheckNone();

            TipAndLine(sourceView, "Bag");
            HashSet<int> contains = [];
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                foreach (var drs in Main.ItemDropsDB.GetRulesForItemID(i))
                {
                    List<DropRateInfo> drInfo = [];
                    drs.ReportDroprates(drInfo, new(1f));
                    if (drInfo.Any(x => x.itemId == type))
                    {
                        contains.Add(i);
                        break;
                    }
                }
            }
            foreach (int item in contains)
            {
                any = true;
                UIItemSlot slot = new(new(item), 0.75f);
                sourceView.AddElement(slot);
            }
            CheckNone();

            TipAndLine(sourceView, "NPCDrop");
            contains = [];
            for (int i = 1; i < NPCLoader.NPCCount; i++)
            {
                foreach (var drs in Main.ItemDropsDB.GetRulesForNPCID(i))
                {
                    List<DropRateInfo> drInfo = [];
                    drs.ReportDroprates(drInfo, new(1f));
                    if (drInfo.Any(x => x.itemId == type))
                    {
                        contains.Add(i);
                        break;
                    }
                }
            }
            foreach (int npc in contains)
            {
                any = true;
                UINPCSlot slot = new(npc);
                sourceView.AddElement(slot);
            }
            CheckNone();
        }
    }
}
