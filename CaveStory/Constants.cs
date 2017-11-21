using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public static class Constants
    {
        public const int kTileSize = 32;
        public const float kWalkingAcceleration = 0.0012f;
        public const float kMaxSpeedX = 0.325f;
        public const float kMaxSpeedY = 0.325f;
        public const float kGravity = 0.0012f;
        public const float kSlowdownFactor = 0.8f;
        public const float kJumpSpeed = 0.325f; // pixels per millisecond
        public const int kJumpTime = 275; // in milliseconds
    }
}
