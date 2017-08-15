using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public class Player
    {
        private Sprite sprite;
        private int _x, _y;
        private float _accelerationX;
        private float _velocityX;
        

        public Player(int x, int y, AnimatedSprite sprite)
        {
            _x = x;
            _y = y;
            this.sprite = sprite;
            _velocityX = 0.0f;
            _accelerationX = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            _x += (int)Math.Round(_velocityX * gameTime.ElapsedGameTime.Milliseconds); // rounding not truncating
            _velocityX += _accelerationX * gameTime.ElapsedGameTime.Milliseconds;
            if (_accelerationX < 0.0f) { _velocityX = Math.Max(_velocityX, -Constants.kMaxSpeedX); } // left movement
            else if (_accelerationX > 0.0f) { _velocityX = Math.Min(_velocityX, Constants.kMaxSpeedX); } // right movement
            else { _velocityX *= Constants.kSlowdownFactor; } // not moving
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, _x, _y);
        }

        public void StartMovingLeft()
        {
            _accelerationX = -Constants.kWalkingAcceleration;
        }
        public void StartMovingRight()
        {
            _accelerationX = Constants.kWalkingAcceleration;
        }
        public void StopMoving()
        {
            _accelerationX = 0f;
        }
    }
}
