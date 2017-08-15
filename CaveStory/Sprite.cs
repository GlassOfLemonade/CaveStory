using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaveStory
{
    public class Sprite
    {
        protected Texture2D texture;
        protected Rectangle sourceRect;

        public Sprite(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.sourceRect= new Rectangle(x, y, width, height);
        }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            Rectangle destinationRectangle = new Rectangle(x, y, sourceRect.Width, sourceRect.Height);
            spriteBatch.Draw(texture, destinationRectangle, sourceRect, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
