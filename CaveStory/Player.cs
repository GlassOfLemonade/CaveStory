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
        private float _accelerationY;
        private float _velocityY;

        private int jumpTimer = 1500;
        private bool onGround;
        public bool OnGround
        {
            get { return onGround; }
        }

        Jump jump;

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
            _velocityY = 0.0f;
            _accelerationX = 0.0f;
            facingDirection = SpriteState.FacingDirection.LEFT;

            jump = new Jump();
            onGround = false;
        }
        #region Monogame Methods
        public void Update(GameTime gameTime)
        {
            sprites[SpriteState].Update(gameTime);
            jump.Update(gameTime);

            #region X position update
            _x += (int)Math.Round(_velocityX * gameTime.ElapsedGameTime.Milliseconds); // rounding not truncating
            _velocityX += _accelerationX * gameTime.ElapsedGameTime.Milliseconds;
            if (_accelerationX < 0.0f) { _velocityX = Math.Max(_velocityX, -Constants.kMaxSpeedX); } // left movement
            else if (_accelerationX > 0.0f) { _velocityX = Math.Min(_velocityX, Constants.kMaxSpeedX); } // right movement
            else if (OnGround) { _velocityX *= Constants.kSlowdownFactor; } // not moving
            #endregion

            #region Y position update
            _y += (int)Math.Round(_velocityY * gameTime.ElapsedGameTime.Milliseconds);
            if (jump.Active)
            {
                // don't do anything
            }
            else
            {
                _velocityY = Math.Min(_velocityY + (Constants.kGravity * gameTime.ElapsedGameTime.Milliseconds), 
                                       Constants.kMaxSpeedY);
            }
            /* Temporary Ground Collision: To Be Removed */
            if (_y > 320)
            {
                _y = 320;
                _velocityY = 0.0f;
            }
            onGround = _y == 320;
            #endregion
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprites[SpriteState].Draw(spriteBatch, _x, _y);
        }
        #endregion

        #region Movement Methods
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
        #endregion

        #region Jump Methods
        public void StartJump()
        {
            if (OnGround == true)
            {
                // reset jump
                jump.Reset();
                // initial velocity upwards
                _velocityY = -Constants.kJumpSpeed;
            }
            else if (_velocityY < 0.0)
            {
                // cancel gravity
                jump.Reactivate();
            }
            else if (_velocityY > 0.0)
            {
                // do nothing
            }
        }
        public void StopJump()
        {
            jump.Deactivate();
        }
        #endregion

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
