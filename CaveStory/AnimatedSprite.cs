using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaveStory
{
    public class AnimatedSprite : Sprite
    {
        private int _FrameTime;
        private int _NumFrames;
        private int _CurrentFrame;
        private int _ElapsedTime; // elapsed time since last frame change

        public AnimatedSprite(Texture2D texture, int x, int y, int width, int height, int fps, int num_frames)
            : base(texture, x, y, width, height)
        {
            this.texture = texture;
            this.sourceRect = new Rectangle(x, y, width, height);
            _FrameTime = 1000 / fps;
            _NumFrames = num_frames;
            _CurrentFrame = 0;
            _ElapsedTime = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (_ElapsedTime > _FrameTime)
            {
                _CurrentFrame++;
                _ElapsedTime = 0;
                if (_CurrentFrame < _NumFrames)
                {
                    sourceRect.X += Game1.kTileSize;
                }
                else // source rect is at number of frames
                {
                    sourceRect.X -= Game1.kTileSize * (_NumFrames - 1);
                    _CurrentFrame = 0;
                }
            }
        }
    }
}
