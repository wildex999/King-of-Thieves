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

        Map.CLayer[] layers = new Map.CLayer[1];

        public EditorMode()
        {
            _componentEditor.Visible = true;
            _tileEditor.Visible = true;

            _controlManager.root = new Actors.Controllers.CEditorInputController();

            layers = new Map.CLayer[1];
            //layers[0] = new Map.CLayer();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //draw the selected rectangle in the top left for now
            spriteBatch.Draw(_tileEditor.sourceSet, Vector2.Zero, _selectedTiles[0], Color.White);

            //draw the tiles
            //layers[0].drawLayer();
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

            if (((Actors.Controllers.CEditorInputController)_controlManager.root).dropTile)
            {
                
                Vector2 mouseCoords = new Vector2((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseX,
                                                  (Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseY);

                Map.CTile temp = new Map.CTile(new Vector2(_selectedTiles[0].X, _selectedTiles[0].Y), mouseCoords, _tileEditor.Controls["cmbTexture"].Text);
                //((Actors.Controllers.CEditorInputController)_controlManager.root).dropTile = false;
                //Map.CTile temp = new Map.CTile(new Vector2(
                //temp.COORDS
                //layers[0].addTile(temp);
            }
            
        }
    }
}
