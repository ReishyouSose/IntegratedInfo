using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RUIModule.RUIElements;
using System;
using Terraria;
using Terraria.GameContent;
using static IntegratedInfo.MiscHelper;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel : ContainerElement
    {
        internal static string LocalKey => typeof(InfoPanel).FullName;
        private Action<SpriteBatch, Vector2> extraDraw;
        public override void OnInitialization()
        {
            base.OnInitialization();
            RemoveAll();

            UICornerPanel bg = new();
            bg.SetSize(500, 400);
            bg.SetCenter(0, 0, 0.5f, 0.5f);
            bg.Info.SetMargin(10);
            bg.canDrag = true;
            Register(bg);

            focus = new();
            focus.Events.OnLeftDown += evt =>
            {
                int type = Main.mouseItem.type;
                focus.item.SetDefaults(type);
                //CheckRicpeFromItem(type);
                CheckSourceFromItem(type);
            };
            bg.Register(focus);

            UIAdjust adjust = new();
            bg.Register(adjust);

            //RegisterRecipePanel(bg);
            RegisterSourcePanel(bg);
        }
        public override void Update(GameTime gt)
        {
            hoverTile = null;
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            ExtraDraw(sb);
        }
        private static void AutoPosRule(UIContainerPanel view)
        {
            view.autoPosRule = innerUIEs =>
            {
                int count = innerUIEs.Count;
                if (count == 0)
                    return;
                int[] edge = view.GetEdgeBlur();
                int x = edge[1], y = edge[0],
                    h = view.autoPos[0].Value, w = view.autoPos[1].Value;
                for (int i = 0; i < count; i++)
                {
                    BaseUIElement uie = innerUIEs[i];
                    uie.SetPos(x, y);
                    if (i == count - 1)
                        return;
                    BaseUIElement next = innerUIEs[i + 1];
                    if (uie is UIText text)
                    {
                        x = edge[1];
                        y += 28;
                        if (next is UIText)
                        {
                            y += 20;
                        }
                    }
                    else if (uie is UIImage)
                    {
                        x = edge[1];
                        y += 12;
                    }
                    else
                    {
                        x += uie.Width + w;
                        if (next is UIText)
                        {
                            x = edge[1];
                            y += uie.Height + 20;
                        }
                        else if (x + next.Width > view.InnerWidth)
                        {
                            x = edge[1];
                            y += uie.Height + h;
                        }
                    }
                }
            };
        }
        private static void TipAndLine(UIContainerPanel view, string key)
        {
            UIText tip = new(GTV("Info." + key));
            tip.SetSize(tip.TextSize);
            view.AddElement(tip);

            UIImage line = new(TextureAssets.MagicPixel.Value);
            line.SetSize(0, 2, 1);
            view.AddElement(line);
        }
        private void ExtraDraw(SpriteBatch sb) => extraDraw?.Invoke(sb, ChildrenElements[0].HitBox().TopLeft() + Vector2.UnitX * 10);
    }
}
