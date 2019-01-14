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
        private float _velocityY;

        // collision rects
        private Rectangle kCollisionRectX = new Rectangle(6,10,20,12);
        private Rectangle kCollisionRectY = new Rectangle(10,2,12,30);

        //private int jumpTimer = 1500;
        private bool onGround;
        public bool OnGround
        {
            get { return onGround; }
        }

        Jump jump;

        SpriteState.FacingDirection facingDirection;
        SpriteState.MotionType motionType;
        SpriteState.VerticalLooking verticalLooking;

        Dictionary<SpriteState, Sprite> sprites;

        public SpriteState SpriteState
        {
            get
            {
                return getSpriteState();
            }
        }
        private SpriteState getSpriteState()
        {
            if (OnGround) // on ground
            {
                motionType = _accelerationX == 0.0f ? SpriteState.MotionType.STANDING : SpriteState.MotionType.WALKING;
            }
            else // in air
            {
                motionType = _velocityY < 0.0f ? SpriteState.MotionType.JUMPING : SpriteState.MotionType.FALLING;
            }
            return new SpriteState(motionType, facingDirection, verticalLooking);
        }

        // need to study a bit more on this section, the math is making sense but I am not grasping the delta values and how they would affect the collision handling
        #region Collision Rectangles
        Rectangle bottomCollision(int delta)
        {
            Rectangle bottomColRect = new Rectangle(_x + kCollisionRectY.Left, _y + kCollisionRectY.Top + (kCollisionRectY.Height / 2), kCollisionRectY.Width, kCollisionRectY.Height / 2);
            if (delta >= 0)
            {
                bottomColRect.Height += delta;
            }
            return bottomColRect;
        }
        Rectangle topCollision(int delta)
        {
            Rectangle topColRect = new Rectangle(_x + kCollisionRectY.Left, _y + kCollisionRectY.Top, kCollisionRectY.Width, kCollisionRectY.Height / 2);
            if (delta <= 0)
            {
                topColRect.Y += delta;
                topColRect.Height -= delta;
            }
            return topColRect;
        }
        Rectangle leftCollision(int delta)
        {
            Rectangle leftColRect = new Rectangle(_x + kCollisionRectX.Left, _y + kCollisionRectX.Top, kCollisionRectX.Width / 2, kCollisionRectX.Height);
            if (delta <= 0)
            {
                leftColRect.X += delta;
                leftColRect.Width -= delta;
            }
            return leftColRect;
        }
        Rectangle rightCollision(int delta)
        {
            Rectangle rightColRect = new Rectangle(_x + kCollisionRectX.Left + (kCollisionRectX.Width/2), _y + kCollisionRectX.Top, kCollisionRectX.Width / 2, kCollisionRectX.Height);
            if (delta >= 0)
            {
                rightColRect.Width += delta;
            }
            return rightColRect;
        }
        #endregion

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
            verticalLooking = SpriteState.VerticalLooking.HORIZONTAL;
            jump = new Jump();
            onGround = false;
        }
        #region Monogame Methods
        public void Update(GameTime gameTime, Map map)
        {
            sprites[SpriteState].Update(gameTime);
            jump.Update(gameTime);

            #region X position update
            UpdateX(gameTime, map);
            #endregion

            #region Y position update
            UpdateY(gameTime, map);
            #endregion
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprites[SpriteState].Draw(spriteBatch, _x, _y);
        }
        /// <summary>
        /// Helper Update method for X axis
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="map"></param>
        private void UpdateX(GameTime gameTime, Map map)
        {
            _x += (int)Math.Round(_velocityX * gameTime.ElapsedGameTime.Milliseconds); // rounding not truncating
            _velocityX += _accelerationX * gameTime.ElapsedGameTime.Milliseconds;
            if (_accelerationX < 0.0f) { _velocityX = Math.Max(_velocityX, -Constants.kMaxSpeedX); } // left movement
            else if (_accelerationX > 0.0f) { _velocityX = Math.Min(_velocityX, Constants.kMaxSpeedX); } // right movement
            else if (OnGround) { _velocityX *= Constants.kSlowdownFactor; } // not moving
        }
        /// <summary>
        /// Helper Update method for Y axis
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="map"></param>
        private void UpdateY(GameTime gameTime, Map map)
        {
            // Update Velocity
            if (jump.Active)
            {
                // don't do anything
            }
            else
            {
                _velocityY = Math.Min(_velocityY + (Constants.kGravity * gameTime.ElapsedGameTime.Milliseconds),
                                       Constants.kMaxSpeedY);
            }

            int delta = (int)Math.Round(_velocityY * gameTime.ElapsedGameTime.Milliseconds);
            _y += (int)Math.Round(_velocityY * gameTime.ElapsedGameTime.Milliseconds); // gravity

            /* Temporary Ground Collision: To Be Removed */
            if (_y > 320)
            {
                _y = 320;
                _velocityY = 0.0f;
            }
            onGround = _y == 320;
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

        #region Vertical Looking Handlers
        public void LookUp()
        {
            verticalLooking = SpriteState.VerticalLooking.UP;
        }
        public void LookDown()
        {
            verticalLooking = SpriteState.VerticalLooking.DOWN;
        }
        public void LookHorizontal()
        {
            verticalLooking = SpriteState.VerticalLooking.HORIZONTAL;
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

        #region Sprite Work
        private void InitializeSprites(ContentManager content)
        {
            // for every motion type
            for (SpriteState.MotionType motionType = 0; motionType < SpriteState.MotionType.LAST_MOTION; motionType++)
            {
                // for every facing direction
                for (SpriteState.FacingDirection facingDirection = 0; facingDirection < SpriteState.FacingDirection.LAST_FACING; facingDirection++)
                {
                    // for every vertical looking
                    for (SpriteState.VerticalLooking verticalLooking = 0; verticalLooking < SpriteState.VerticalLooking.LAST_VERTICAL; verticalLooking++)
                    {
                        InitializeSprite(content, new SpriteState(motionType, facingDirection, verticalLooking));
                    }
                }
            }
        }

        private void InitializeSprite(ContentManager content, SpriteState spriteState)
        {
            #region Source Rectangle Horizontal Index
            int sourceX = 0; // have to initialize to 0
            switch (spriteState.motionType)
            {
                case SpriteState.MotionType.WALKING:
                    sourceX = Constants.kWalkFrame * Constants.kTileSize;
                    break;
                case SpriteState.MotionType.STANDING:
                    sourceX = Constants.kStandFrame * Constants.kTileSize;
                    break;
                case SpriteState.MotionType.JUMPING:
                    sourceX = Constants.kJumpFrame * Constants.kTileSize;
                    break;
                case SpriteState.MotionType.FALLING:
                    sourceX = Constants.kFallFrame * Constants.kTileSize;
                    break;
                case SpriteState.MotionType.LAST_MOTION:
                    break;
            }
            sourceX = spriteState.verticalLooking == SpriteState.VerticalLooking.UP ?
                      sourceX + (Constants.kLookingUpOffset * Constants.kTileSize) : sourceX;
            #endregion
            #region Source Rectangle Vertical Index
            // characterframe*tilesize if direction is left, or (characterframe+1)*tilesize if direction is right
            int sourceY = (spriteState.facingDirection == SpriteState.FacingDirection.LEFT) ? 
                Constants.kCharacterFrame * Constants.kTileSize : (Constants.kCharacterFrame + 1) * Constants.kTileSize;
            #endregion

            if (spriteState.motionType == SpriteState.MotionType.WALKING)
            {
                // create animated sprite
                sprites.Add(spriteState,
                new AnimatedSprite(content, Constants.kSpriteFilePath, sourceX, sourceY, Constants.kTileSize, Constants.kTileSize, 15, 3));
            }
            else
            {
                if (spriteState.verticalLooking == SpriteState.VerticalLooking.DOWN)
                {
                    if (spriteState.motionType == SpriteState.MotionType.STANDING)
                    {
                        // if standing, change source x to index 7 or 8th sprite
                        sourceX = ((Constants.kLookingDownOffset + 1) * Constants.kTileSize);
                    }
                    else if (spriteState.motionType == SpriteState.MotionType.FALLING || spriteState.motionType == SpriteState.MotionType.JUMPING)
                    {
                        // if jumping or falling, change source x to index 6 or 7th sprite
                        sourceX = (Constants.kLookingDownOffset * Constants.kTileSize);
                    }
                }
                // create a static sprite
                sprites.Add(spriteState,
                new Sprite(content, Constants.kSpriteFilePath, sourceX, sourceY, Constants.kTileSize, Constants.kTileSize));
            }
        }
        #endregion
    }
}
