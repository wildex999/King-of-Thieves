using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Chuchus
{
    public class CGreenChuChu : CBaseChuChu
    {

        public CGreenChuChu(int sight, float fov, int foh, params dropRate[] drops)
            : base(sight, fov, foh, drops)
        {
            _position.X = 200;
            _position.Y = 200;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("chuChuWobble", new Graphics.CSprite("chuChu:Wobble", Graphics.CTextures.textures["chuChu:Wobble"]));
            _imageIndex.Add("chuChuPopUp", new Graphics.CSprite("chuChu:PopUp", Graphics.CTextures.textures["chuChu:PopUp"]));
            _imageIndex.Add("chuChuIdle", new Graphics.CSprite("chuChu:Idle", Graphics.CTextures.textures["chuChu:Idle"]));
            _imageIndex.Add("chuChuHop", new Graphics.CSprite("chuChu:Hop", Graphics.CTextures.textures["chuChu:Hop"]));
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
