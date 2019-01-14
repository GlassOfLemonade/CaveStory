using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public class Map
    {

        private List<List<Tile>> tiles; // 2D list of sprites, as each tile is essentially just a sprite

        public Map()
        {
            tiles = new List<List<Tile>>();
        }

        public static Map MakeTestMap(ContentManager content) // need content manager to define the bitmap for the tile sprite
        {
            Map map = new Map();

            const int numRows = 15; // 15 * 32 = 480
            const int numCols = 20; // 20 * 32 = 640
            map.tiles = new List<List<Tile>>();
            for (int i = 0; i < numRows; i++)
            {
                map.tiles.Add(new List<Tile>()); // ensures 15 rows are added
                for (int j = 0; j < numCols; j++)
                {
                    map.tiles[i].Add(new Tile()); // empty tile, its parameters will be null so sprite will be null
                }
            }

            Sprite sprite = new Sprite(content, Constants.kCaveFilePath, Constants.kTileSize, 0, Constants.kTileSize, Constants.kTileSize);
            Tile tile = new Tile(sprite, Tile.TileType.WALL_TILE);
            const int row = 11; // the row where quote currently stands
            for (int col = 0; col < numCols; col++)
            {
                map.tiles[row][col] = tile; // sets the sprite for each column in this particular row to the assigned sprite
            }

            return map;
        }

        public List<CollisionTile> getCollidingTiles(Rectangle rectangle) // monogame has its own rectangle method
        {
            int firstRow = rectangle.Top / Constants.kTileSize;
            int lastRow = rectangle.Bottom / Constants.kTileSize;
            int firstCol = rectangle.Left / Constants.kTileSize;
            int lastCol = rectangle.Right / Constants.kTileSize;

            List<CollisionTile> collisionTiles = new List<CollisionTile>();

            for (int row = firstRow; row <= lastRow; row++)
            {
                for (int col = firstCol; row <= lastCol; col++)
                {
                    collisionTiles.Add(new CollisionTile(row, col, tiles[row][col].tileType));
                }
            }

            return collisionTiles;
        }

        #region Update and Draw
        /// <summary>
        /// Updates rows then columns
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int row = 0; row < tiles.Count; row++) // rows
            {
                for (int col = 0; col < tiles[row].Count; col++)
                {
                    if (tiles[row][col].sprite != null) // have to check if a tile is null for now since we'll get a null exception if we try to update
                    {
                        tiles[row][col].sprite.Update(gameTime);
                    }
                }
            }
        }
        
        /// <summary>
        /// draws rows then columns
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < tiles.Count; row++) // rows
            {
                for (int col = 0; col < tiles[row].Count; col++)
                {
                    if (tiles[row][col].sprite != null) // do not draw null sprites as we'll get a null exception
                    {
                        tiles[row][col].sprite.Draw(spriteBatch, col * Constants.kTileSize, row * Constants.kTileSize);
                    }
                }
            }
        }
        #endregion 

    }
}
