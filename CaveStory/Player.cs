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
    public class Player
    {
        private int _x, _y;
        private float _accelerationX;
        private float _velocityX;
        SpriteState.FacingDirection facingDirection; 

        Dictionary<SpriteState, Sprite> sprites;

        private SpriteState spriteState;
        public SpriteState SpriteState
        {
            get
            {
                return new SpriteState(_accelerationX == 0.0f ? SpriteState.MotionType.STANDING : SpriteState.MotionType.WALKING,
                    facingDirection);
            }
        }
        

        public Player(ContentManager Content,int x, int y)
        {
            sprites = new Dictionary<SpriteState, Sprite>();
            InitializeSprites(Content);
            _x = x;
            _y = y;
            _velocityX = 0.0f;
            _accelerationX = 0.0f;
            facingDirection = SpriteState.FacingDirection.LEFT;
        }

        public void Update(GameTime gameTime)
        {
            sprites[SpriteState].Update(gameTime);

            _x += (int)Math.Round(_velocityX * gameTime.ElapsedGameTime.Milliseconds); // rounding not truncating
            _velocityX += _accelerationX * gameTime.ElapsedGameTime.Milliseconds;
            if (_accelerationX < 0.0f) { _velocityX = Math.Max(_velocityX, -Constants.kMaxSpeedX); } // left movement
            else if (_accelerationX > 0.0f) { _velocityX = Math.Min(_velocityX, Constants.kMaxSpeedX); } // right movement
            else { _velocityX *= Constants.kSlowdownFactor; } // not moving
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprites[SpriteState].Draw(spriteBatch, _x, _y);
        }

        public void StartMovingLeft()
        {
            _accelerationX = -Constants.kWalkingAcceleration;
            facingDirection = SpriteState.FacingDirection.LEFT;
        }
        public void StartMovingRight()
        {
            _accelerationX = Constants.kWalkingAcceleration;
            facingDirection = SpriteState.FacingDirection.RIGHT;
        }
        public void StopMoving()
        {
            _accelerationX = 0f;
        }

        public void InitializeSprites(ContentManager content)
        {
            // standing, facing left
            sprites.Add(new SpriteState(SpriteState.MotionType.STANDING, SpriteState.FacingDirection.LEFT), 
                new Sprite(content, "Sprites/MyChar", 0, 0, Constants.kTileSize, Constants.kTileSize));
            // standing, facing right
            sprites.Add(new SpriteState(SpriteState.MotionType.STANDING, SpriteState.FacingDirection.RIGHT),
                new Sprite(content, "Sprites/MyChar", 0, Constants.kTileSize, Constants.kTileSize, Constants.kTileSize));
            // walking, facing left
            sprites.Add(new SpriteState(SpriteState.MotionType.WALKING, SpriteState.FacingDirection.LEFT),
                new AnimatedSprite(content, "Sprites/MyChar", 0, 0, Constants.kTileSize, Constants.kTileSize, 15, 3));
            // walking, facing right
            sprites.Add(new SpriteState(SpriteState.MotionType.WALKING, SpriteState.FacingDirection.RIGHT),
                new AnimatedSprite(content, "Sprites/MyChar", 0, Constants.kTileSize, Constants.kTileSize, Constants.kTileSize, 15, 3));
        }
    }
}
