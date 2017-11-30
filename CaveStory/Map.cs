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
        private List<List<Sprite>> foregroundSprites; // 2D list of sprites, as each tile is essentially just a sprite

        public Map()
        {
            // initial empty constructor
            foregroundSprites = new List<List<Sprite>>();
        }

        public static Map MakeTestMap(ContentManager content) // need content manager to define the bitmap for the tile sprite
        {
            Map map = new Map();

            const int numRows = 15; // 15 * 32 = 480
            const int numCols = 20; // 20 * 32 = 640
            map.foregroundSprites = new List<List<Sprite>>();
            for (int i = 0; i < numRows; i++)
            {
                map.foregroundSprites.Add(new List<Sprite>()); // ensures 15 rows are added
                for (int j = 0; j < numCols; j++)
                {
                    map.foregroundSprites[i].Add(null); // for each column in the row add in a null for now
                }
            }

            Sprite sprite = new Sprite(content, Constants.kCaveFilePath, Constants.kTileSize, 0, Constants.kTileSize, Constants.kTileSize);
            const int row = 11; // the row where quote currently stands
            for (int col = 0; col < numCols; col++)
            {
                map.foregroundSprites[row][col] = sprite; // sets the sprite for each column in this particular row to the assigned sprite
            }

            return map;
        }

        #region Update and Draw
        /// <summary>
        /// Updates rows then columns
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int row = 0; row < foregroundSprites.Count; row++) // rows
            {
                for (int col = 0; col < foregroundSprites[row].Count; col++)
                {
                    if (foregroundSprites[row][col] != null) // have to check for if a tile is null for now since we'll get a null exception if we try to update
                    {
                        foregroundSprites[row][col].Update(gameTime);
                    }
                }
            }
        }
        
        /// <summary>
        /// draws rows then columns
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < foregroundSprites.Count; row++) // rows
            {
                for (int col = 0; col < foregroundSprites[row].Count; col++)
                {
                    if (foregroundSprites[row][col] != null) // do not draw null sprites as we'll get a null exception
                    {
                        foregroundSprites[row][col].Draw(spriteBatch, col * Constants.kTileSize, row * Constants.kTileSize);
                    }
                }
            }
        }
        #endregion 
    }
}
