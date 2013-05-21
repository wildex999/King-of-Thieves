using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorOpen : CActor
    {
        public CEditorOpen()
        {
            _imageIndex.Add("iconOpen", new Graphics.CSprite("editor:icons:open", Graphics.CTextures.textures["editor:icons:open"]));
            _position = new Vector2(25, 4);

            swapImage("iconOpen", false);

            _hitBox = new Collision.CHitBox(25, 4, 16, 16);
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public override void click(object sender)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = ".xml files (*.xml) | *.xml";
            ofd.Title = "Open Map File";
            
            ofd.ShowDialog();


            ofd = null;
        }

        public override void drawMe()
        {
            base.drawMe();
        }
        
    }
}
