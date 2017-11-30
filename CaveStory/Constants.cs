using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public static class Constants
    {
        // general
        public const int kTileSize = 32;
        // motion
        public const float kWalkingAcceleration = 0.0012f;
        public const float kMaxSpeedX = 0.325f;
        public const float kMaxSpeedY = 0.325f;
        public const float kGravity = 0.0012f;
        public const float kSlowdownFactor = 0.8f;
        public const float kJumpSpeed = 0.325f; // pixels per millisecond
        public const int kJumpTime = 275; // in milliseconds
        // spritesheet
        public const int kWalkFrame = 0;
        public const int kStandFrame = 0;
        public const int kJumpFrame = 1;
        public const int kFallFrame = 2;
        public const int kCharacterFrame = 0;
        public const int kLookingUpOffset = 3;
        public const int kLookingDownOffset = 6;
        // strings
        public const string kSpriteFilePath = "Sprites/MyChar";
        public const string kCaveFilePath = "Sprites/PrtCave";
    }
}
