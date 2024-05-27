using Microsoft.Xna.Framework;
using RUIModule.RUIElements;
using Terraria;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel : ContainerElement
    {
        internal static string LocalKey => typeof(InfoPanel).FullName;
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
                CheckRicpeFromItem(type);
            };
            bg.Register(focus);

            UIAdjust adjust = new();
            bg.Register(adjust);

            RegisterRecipePanel(bg);
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
    }
}
