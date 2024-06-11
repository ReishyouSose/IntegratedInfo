using IntegratedInfo.InfoUI.ExtraUI;
using RUIModule.RUIElements;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel
    {
        private UIBottom npcPanel;
        private UIContainerPanel npcView;
        private void RegisterNpcPanel(UICornerPanel bg)
        {
            npcPanel = new();
            npcPanel.SetPos(0, 62);
            npcPanel.SetSize(-30, -62, 1, 1);
            bg.Register(npcPanel);

            UICornerPanel npcBg = new();
            npcBg.SetSize(0, 0, 1, 1);
            npcPanel.Register(npcBg);

            npcView = new(10);
            npcView.SetSize(-20, 0, 1, 1);
            npcView.VerticalEdge();
            //npcView.autoPos = [5, 5];
            //AutoPosRule(npcView);
            npcBg.Register(npcView);

            VerticalScrollbar sv = new(39);
            npcView.SetVerticalScrollbar(sv);
            npcBg.Register(sv);
        }
        private void CheckVanillaNPCDrop()
        {
            npcView.ClearAllElements();
            int x = 0, y = npcView.GetEdgeBlur()[0];
            for (int i = 1; i < NPCID.Count; i++)
            {
                UINPCSlot npc = new(i);
                npc.SetPos(x, y);
                npcView.AddElement(npc);
                y += npc.Height + 5;
                int banner = Item.NPCtoBanner(i);
                if (banner == 0)
                    continue;
                banner = Item.BannerToItem(banner);
                if (banner > 0)
                {
                    int lastH = 0;
                    foreach (var drs in Main.ItemDropsDB.GetRulesForNPCID(i, false))
                    {
                        List<DropRateInfo> drInfo = [];
                        drs.ReportDroprates(drInfo, new(1f));
                        foreach (var info in drInfo)
                        {
                            if (info.itemId > ItemID.Count || info.stackMax > 1)
                                continue;
                            int count = Math.Clamp((int)(1f / info.dropRate / 50), 1, 10);
                            UIDropItem drop = new(info);
                            if (x + drop.Width > npcView.InnerWidth)
                            {
                                x = 0;
                                y += drop.Height + 5;
                            }
                            drop.SetPos(x, y);
                            npcView.AddElement(drop);
                            x += drop.Width + 5;
                            lastH = drop.Height;
                        }
                    }
                    y += lastH + 5;
                }
                x = 0;
                y += 20;
            }
        }
    }
}
