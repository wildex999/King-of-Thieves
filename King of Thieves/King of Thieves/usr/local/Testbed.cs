using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.usr.local
{
    public class Testbed : MenuReadyGameState
    {
        private CComponent compTest;
        private CComponent menuComo;
        Actors.Player.CPlayer[] perfTest;
        CComponent perfComp = new CComponent();
        CComponent npcTester = new CComponent();
        Actors.NPC.Enemies.Chuchus.CGreenChuChu green = new Actors.NPC.Enemies.Chuchus.CGreenChuChu(10, 45.0f, 120);
        //private Zone testingzone;
        public Testbed(ref CComponent comp, ref CComponent menu)
        {
            MenuText = "KoT Testbed";
            compTest = comp;
            menuComo = menu;

            Initialize();
        }
        private void Initialize()
        {
            //testingzone = new Zone();
            //perfTest = new Actors.Player.CPlayer[700];
            //for (int i = 0; i < 700; i++)
            //    perfTest[i] = new Actors.Player.CPlayer();

            //perfComp.root = perfTest[0];
            //for (int i = 1; i < 700; i++)
            //    perfComp.actors.Add("performance" + i, perfTest[i]);

            npcTester.root = green;
            
            
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            compTest.Draw(null);
            npcTester.Draw(null);
            //perfComp.Draw(null);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Input.CInput.update();

            //compTest.root.position = new Vector2(Input.CInput.mouseX, Input.CInput.mouseY);
            compTest.Update(gameTime);
            npcTester.Update(gameTime);

            //perfComp.Update(gameTime);
            //menuComo.updateActors(gameTime);

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).getInputRelease(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["lttp_heart"]);
            }

        }

        /// <summary>
        /// Contains logic that should be fired every time the state becomes active.
        /// This should fire especially in cases where the state had become inactive
        ///     and then regains activity once again.
        /// </summary>
        private void ActivateState()
        {
            _StateIsActive = true;
            //Input.ClearEventHandler();
            //Input.EnableInput();
            //zone1.Activate();
        }
    }
}
