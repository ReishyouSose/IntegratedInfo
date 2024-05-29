using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RUIModule;
using RUIModule.RUIElements;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ObjectData;

namespace IntegratedInfo.InfoUI.ExtraUI
{
    public class UITileSlot : UIIconSlot
    {
        public readonly Tile tile;
        public readonly Rectangle[] frame;
        public readonly int width, height;
        public readonly UIItemSlot item;
        public readonly float drawScale;
        public UITileSlot(int tileID, int slotID = 0, float scale = 1) : base(null, slotID, scale)
        {
            Item item = ContentSamples.ItemsByType.Values.FirstOrDefault(x => x.createTile == tileID, null);
            string name = Lang.GetMapObjectName(MapHelper.TileToLookup(tileID, 0));
            hoverText = item?.Name ?? name;
            if (item == null)
            {
                Main.instance.LoadTiles(tileID);
                TileObjectData data = TileObjectData.GetTileData(tileID, 0);
                width = data.Width;
                height = data.Height;
                int readWidth = data.CoordinateWidth;
                int[] readHeight = data.CoordinateHeights;
                int padding = data.CoordinatePadding;
                frame = new Rectangle[width * height];
                int i = 0, h = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        frame[i++] = new(x * (readWidth + padding), h, readWidth, readHeight[y]);
                    }
                    h += readHeight[y] + padding;
                }
                icon = TextureAssets.Tile[tileID].Value;
                SetSize(52 * scale, 52 * scale);
                drawScale = (new Vector2(width, height) * 16).AutoScale(new Vector2(52 * scale * 0.75f));
            }
            else
            {
                this.item = new(item, scale);
                Register(this.item);
            }
        }
        public override void DrawSelf(SpriteBatch sb)
        {
            DrawSlot(sb);
            if (item == null)
            {
                Vector2 pos = HitBox().Center() - new Vector2(width, height) * 8 * drawScale;
                int count = frame.Length;
                for (int i = 0; i < count; i++)
                {
                    int x = i % width;
                    int y = i / width;
                    sb.Draw(icon, pos + new Vector2(x, y) * 16 * drawScale,
                        frame[i], Color.White, 0, Vector2.Zero, drawScale, 0, 0);
                }
            }
        }
    }
}
