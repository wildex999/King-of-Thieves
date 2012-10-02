using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local
{
    public static class master {
        private static Color clearColor = new Color(105, 105, 105, 255);// Color.CornflowerBlue;
        
        private static Stack<GameState> stack = new Stack<GameState>();
        private static LinkedList<GameState> overlays = new LinkedList<GameState>();

        public static void Push(GameState gameState)
        {
            stack.Push(gameState);
        }
        public static GameState Peek()
        {
            return stack.Peek();
        }
        public static GameState Pop()
        {
            return stack.Pop();
        }

        /// <summary>
        /// Master Draw call.
        /// This should be the first and only interface from the main game
        ///     "Draw" loop for this instance of the VGE.
        /// </summary>
        /// <param name="spriteBatch">The global-parameter sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            //no matter what, if draw is called, we are drawing the top stack item.
            stack.Peek().Draw(spriteBatch);

            //it's an overlay. we are able to draw more than one layer if there's anything below
            if (stack.Peek().IsOverlay)
            {
                if (overlays.Count != 0)
                {
                    //**Note that this code does not take into account culling.
                    overlays.First().Draw(spriteBatch);
                }
            }
            else //it's not an overlay. 
            //since we are already drawing the top stack item, we dont need to do anything else.
            { }  //this is just here in case it is useful in the future.
        }

        /// <summary>
        /// Master Update call. 
        /// This should be the first and only interface from the main game 
        ///     "Update" loop for this instance of the VGE.
        /// </summary>
        /// <param name="gameTime">The time snapshot.</param>
        public static void Update(GameTime gameTime)
        {
            //global events
            //CGlobalEvents.GFrameTrigger.Update();

            //Input
            //Input.Update(gameTime);

            //only updating the top item, whether it is an overlay or just a regular state
            //overlays not supported yet
            stack.Peek().Update(gameTime);

            //NOTE: Only do this for the frame event!!!
            //CGlobalEvents.GFrameTrigger.getEvent(0).triggered = false;

        }
        private static void PopToList()
        {
            if (stack.Count != 0)
            {
                //Debug.Out("Master::StoreTop(): Stack is not empty. Popping stack to list.");
                overlays.AddFirst(stack.Pop());
            }
            else
            {
                //Debug.Out("Master::StoreTop(): ERROR Stack is empty. Cannot pop stack.");
            }

        }
        private static void PushReturnToStack()
        {
            if (overlays.Count != 0)
            {
                //Debug.Out("Master::ReturnToStack(): List is not empty. Pushing first item of list to stack.");
                stack.Push(overlays.First());
                overlays.RemoveFirst();
            }
            else
            {
                //Debug.Out("Master::ReturnToStack(): ERROR List is empty. Unable to push any object to stack.");
            }
        }
        private static void CleanList()
        {
            overlays.Clear();
        }
        public static int GetListLength()
        {
            return overlays.Count;
        }
        public static int GetStackLength()
        {
            return stack.Count;
        }
        public static Color GetClearColor()
        {
            return clearColor;
        }

        internal static void SetClearColor(Color color)
        {
            clearColor = color;
        }
    }
}
