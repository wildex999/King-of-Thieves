using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorInputController : CActor
    {
        public bool _shutDown = false;
        private bool _dropTile = false;

        public override void keyDown(object sender)
        {
            if ((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Back))
            {
                _shutDown = true;
            }

        }

        public override void mouseClick(object sender)
        {
            _dropTile = true;
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public bool dropTile
        {
            get
            {
                bool temp = _dropTile;
                _dropTile = false;
                return temp;
            }
        }
    }
}
