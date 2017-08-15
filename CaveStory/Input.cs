using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveStory
{
    /// <summary>
    /// using keyboardstate functions for this, will be much different from what the 
    /// series does as monogame/xna has all this functionality built in.
    /// </summary>
    public class Input
    {
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;

        public Input()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public void BeginInputFrame()
        {
            keyboardState = Keyboard.GetState();
        }
        public void EndInputFrame()
        {
            prevKeyboardState = keyboardState;
        }

        public bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public bool KeyHeld(Keys key)
        {
            return keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyDown(key);
        }
    }
}
