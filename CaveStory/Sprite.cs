using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public class Sprite
    {
        Texture2D texture;
        Rectangle sourceRect;

        public Sprite(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.sourceRect= new Rectangle(x, y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            Rectangle destinationRectangle = new Rectangle(x, y, sourceRect.Width, sourceRect.Height);
            spriteBatch.Draw(texture, destinationRectangle, sourceRect, Color.White);
        }
    }
}
