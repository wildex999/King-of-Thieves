using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;
using King_of_Thieves.Menu;

namespace King_of_Thieves.usr.local
{
    class DevMenu : MenuReadyGameState
    {
        private Gears.Navigation.Menu _menu;

        public DevMenu()
        {
            MenuUserControl[] menuItems = new MenuUserControl[2];

            menuItems[0] = new GameTestMenuOption();
            menuItems[1] = new MapEditorMenuOption();
            _menu = new Gears.Navigation.Menu("Transmission Development Menu", menuItems);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _menu.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _menu.Draw(spriteBatch);
        }
    }
}
