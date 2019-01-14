using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace CaveStory
{
    public class CollisionTile
    {
        int row;
        int col;

        Tile.TileType tileType;

        public CollisionTile(int row, int col, Tile.TileType tileType)
        {
            this.row = row;
            this.col = col;
            this.tileType = tileType;
        }
    }
}