using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RUIModule;
using RUIModule.RUIElements;
using RUIModule.RUISys;
using Terraria;
using Terraria.GameContent;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UITileText : BaseUIElement
    {
        public readonly UIText tileName;
        public readonly int tile;
        public UITileText(string name, int tile)
        {
            tileName = new(name);
            tileName.SetSize(tileName.TextSize);
            tileName.HoverToGold();
            Register(tileName);
            Info.IsSensitive = true;
            SetSize(tileName.TextSize);
            this.tile = tile;
        }
        public void DrawTile(SpriteBatch sb, Vector2 pos)
        {
            Texture2D tex = TextureAssets.Tile[tile].Value;
            Rectangle rec = RUIHelper.NewRec(pos, tex.Size());
            rec = rec.Modified(0, 0, 20, 20);
            UICornerPanel.VanillaDraw(sb, rec, AssetLoader.VnlBg, RUIHelper.VnlColor * 0.7f, 12, 4);
            UICornerPanel.VanillaDraw(sb, rec, AssetLoader.VnlBd, Color.Black, 12, 4);
            sb.Draw(tex, rec.TopLeft() + new Vector2(10), Color.White);
        }
    }
}
