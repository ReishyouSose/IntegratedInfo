using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RUIModule.RUIElements;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UINPCSlot : UICornerPanel
    {
        public readonly UnlockableNPCEntryIcon npcIcon;
        public UINPCSlot(int npc) : base()
        {
            npcIcon = new(npc);
            Main.instance.LoadNPC(npc);
            Vector2 size = TextureAssets.Npc[npc].Size();
            size.Y /= Main.npcFrameCount[npc];
            size.X += 10;
            size.Y += 10;
            SetSize(size);
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
            Rectangle hitbox = HitBox();
            npcIcon.Update(new(), hitbox, new() { iconbox = hitbox, IsHovered = Info.IsMouseHover, IsPortrait = true });
        }
        public override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            npcIcon.Draw(new(), sb, new() { iconbox = HitBox(), IsHovered = Info.IsMouseHover, IsPortrait = true });
        }
    }
}
