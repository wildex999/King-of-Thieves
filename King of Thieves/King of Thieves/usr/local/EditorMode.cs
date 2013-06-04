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
        private Rectangle[,] _selectedTiles = new Rectangle[2,2];
        private Graphics.CSprite _currentTileSet = null;

        Map.CLayer[] layers = new Map.CLayer[1];

        public EditorMode()
        {
            _componentEditor.Visible = true;
            _tileEditor.Visible = true;

            _controlManager.root = new Actors.Controllers.CEditorInputController();
            _controlManager.actors.Add("btnNew", new Actors.Controllers.CEditorNew());
            _controlManager.actors.Add("btnOpen", new Actors.Controllers.CEditorOpen());
            _controlManager.actors.Add("btnSave", new Actors.Controllers.CEditorSave());

            layers = new Map.CLayer[1];
            Graphics.CSprite startingTexture = new Graphics.CSprite(_tileEditor.defaultTileSet, Graphics.CTextures.textures[_tileEditor.defaultTileSet]);
            layers[0] = new Map.CLayer(ref startingTexture);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            float cursorX = (float)Math.Floor(((double)((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseX / Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameWidth)) * 
                                                        Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameWidth);

            float cursorY = (float)Math.Floor(((double)((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseY / Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameHeight)) * 
                                                        Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameHeight);

            Vector2 cursorTile = new Vector2(cursorX,cursorY);
            //draw the interface
            Vector2 sampleTileSize = new Vector2(_selectedTiles[0,0].Width, _selectedTiles[0,0].Height);
                for (int i = 0; i < _selectedTiles.GetUpperBound(0) + 1; i++)
                    for (int j = 0; j < _selectedTiles.GetUpperBound(1) + 1; j++)
                    {
                        spriteBatch.Draw(_tileEditor.sourceSet, cursorTile + new Vector2(i * sampleTileSize.X, j * sampleTileSize.Y), _selectedTiles[i, j], Color.White);
                    }
            

            

            //draw the selected rectangle in the top left for now
            spriteBatch.Draw(_tileEditor.sourceSet, new Vector2(5, 25), _selectedTiles[0,0], Color.White);

            _controlManager.Draw(spriteBatch);

            //draw the tiles
            layers[0].drawLayer(true);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _controlManager.Update(gameTime);

            if (_tileEditor.selectorChange)
                _currentTileSet = new Graphics.CSprite(_tileEditor.Controls["cmbTexture"].Text, Graphics.CTextures.textures[_tileEditor.Controls["cmbTexture"].Text]);

            if (((Actors.Controllers.CEditorInputController)_controlManager.root)._shutDown)
            {
                _componentEditor.Visible = false;
                _tileEditor.Visible = false;
                Gears.Cloud.Master.Pop();
            }

            _selectedTiles = _tileEditor.tileRect;

            if (((Actors.Controllers.CEditorNew)_controlManager.actors["btnNew"]).createNew)
            {
                layers = null;
                layers = new Map.CLayer[1];
            }

            if (_tileEditor.tileSetChanged)
            {
                //apply the current tileSet to all tiles that use it
                foreach (Map.CLayer layer in layers)
                {
                    layer.updateTileSet(_currentTileSet);
                }
            }

            if (((Actors.Controllers.CEditorInputController)_controlManager.root).dropTile)
            {
                
                Vector2 mouseCoords = new Vector2((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseX,
                                                  (Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseY);
                string tileSet = "";

                if (mouseCoords.Y <= 64)
                    return;

                if (_tileEditor.Controls["cmbTexture"].Text == _tileEditor.defaultTileSet)
                    tileSet = null;
                else
                    tileSet = _tileEditor.Controls["cmbTexture"].Text;

                Map.CTile[,] temp = new Map.CTile[_selectedTiles.GetUpperBound(0) + 1, _selectedTiles.GetUpperBound(1) + 1];

                for (int i = 0; i < _selectedTiles.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < _selectedTiles.GetUpperBound(1) + 1; j++)
                    {
                        temp[i, j] = new Map.CTile(new Vector2(_selectedTiles[i, j].X, _selectedTiles[i, j].Y), mouseCoords + new Vector2(i * _selectedTiles[0, 0].Width, j * _selectedTiles[0, 0].Height), tileSet);
                        layers[0].addTile(temp[i, j]);
                    }
                }



                if (!layers[0].otherImages.ContainsKey(_currentTileSet.atlasName))
                    layers[0].otherImages.Add(_currentTileSet.atlasName, _currentTileSet);

                
            }
            
        }
    }
}
