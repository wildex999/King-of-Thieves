using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local
{
    class EditorMode : Gears.Navigation.MenuReadyGameState
    {
        private Forms.Map_Editor.EditorComponents _componentEditor = new Forms.Map_Editor.EditorComponents();
        private Forms.Map_Editor.EditorTiles _tileEditor = new Forms.Map_Editor.EditorTiles();
        private Actors.CComponent _controlManager = new Actors.CComponent();
        private Rectangle[] _selectedTiles = new Rectangle[4];

        Gears.Cartography.layer[] layers;

        public EditorMode()
        {
            _componentEditor.Visible = true;
            _tileEditor.Visible = true;

            _controlManager.root = new Actors.Controllers.CEditorInputController();

            layers = new Gears.Cartography.layer[1];
            layers[0] = new Gears.Cartography.layer();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //draw the selected rectangle in the top left for now
            spriteBatch.Draw(_tileEditor.sourceSet, Vector2.Zero, _selectedTiles[0], Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _controlManager.Update(gameTime);

            if (((Actors.Controllers.CEditorInputController)_controlManager.root)._shutDown)
            {
                _componentEditor.Visible = false;
                _tileEditor.Visible = false;
                Gears.Cloud.Master.Pop();
            }

            _selectedTiles[0] = _tileEditor.tileRect;
            
        }
    }
}
