using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    public class Jump
    {
        private int timeRemaining;
        private bool active;
        public bool Active
        {
            get { return active; }
        }

        public Jump()
        {
            timeRemaining = 0;
            active = false;
        }

        public void Reset()
        {
            timeRemaining = Constants.kJumpTime;
            Reactivate();
        }
        public void Reactivate()
        {
            active = timeRemaining > 0; // if time remaining on jump is higher than 0, jump is still active
        }
        public void Deactivate()
        {
            active = false;
        }

        public void Update(GameTime gameTime)
        {
            int elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            if (active)
            {
                timeRemaining -= elapsedTime;
                if (timeRemaining <= 0)
                {
                    active = false;
                }
            }
        }
    }
}
