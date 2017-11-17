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
            STANDING, // 0
            WALKING, // 1
        }
        public enum FacingDirection
        {
            LEFT, // 0
            RIGHT, // 1
        }

        MotionType motionType;
        FacingDirection facingDirection;

        public SpriteState(MotionType motionType = MotionType.STANDING, FacingDirection facingDirection = FacingDirection.LEFT)
        {
            this.motionType = motionType;
            this.facingDirection = facingDirection;
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
            return false;
        }
        public static bool operator > (SpriteState a, SpriteState b)
        {
            return false;
        }
    }
}
