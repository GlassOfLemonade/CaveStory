using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public struct SpriteState
    {
        public enum MotionType
        {
            STANDING = 0, // 0
            WALKING, // 1
            JUMPING, // 2
            FALLING, // 3
            LAST_MOTION // helper
        }
        public enum FacingDirection
        {
            LEFT = 0, // 0
            RIGHT, // 1
            LAST_FACING // helper
        }
        public enum VerticalLooking
        {
            UP = 0, // 0
            DOWN, // 1
            HORIZONTAL, // 2
            LAST_VERTICAL // helper
        }

        public MotionType motionType;
        public FacingDirection facingDirection;
        public VerticalLooking verticalLooking;

        public SpriteState(MotionType motionType = MotionType.STANDING, FacingDirection facingDirection = FacingDirection.LEFT,
                           VerticalLooking verticalLooking = VerticalLooking.HORIZONTAL)
        {
            this.motionType = motionType;
            this.facingDirection = facingDirection;
            this.verticalLooking = verticalLooking;
        }

        public static bool operator < (SpriteState a, SpriteState b)
        {
            if (a.motionType != b.motionType)
            {
                return a.motionType < b.motionType;
            }
            if (a.facingDirection != b.facingDirection)
            {
                return a.facingDirection < b.facingDirection;
            }
            if (a.verticalLooking != b.verticalLooking)
            {
                return a.verticalLooking < b.verticalLooking;
            }
            return false;
        }
        public static bool operator > (SpriteState a, SpriteState b)
        {
            return false;
        }
    }
}
