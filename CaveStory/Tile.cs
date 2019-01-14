using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public class Tile
    {
        public enum TileType
        {
            AIR_TILE,
            WALL_TILE
        }

        public TileType tileType;
        public Sprite sprite;

        public Tile()
        {

        }

        public Tile(Sprite sprite, TileType tileType = TileType.AIR_TILE)
        {
            this.tileType = tileType;
            this.sprite = sprite;
        }
    }
}
