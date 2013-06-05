using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Input;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Player
{
    class CPlayer : CActor
    {
        private bool _swordReleased = true;
        private bool _rollReleased = true;
        private static Vector2 _readableCoords = new Vector2();

        public CPlayer() :
            base()
        {

            _name = "Player";
            _position = Vector2.Zero;
            //resource init
            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);

            
            image = _imageIndex["PlayerWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite("Player:WalkDown", Graphics.CTextures.textures["Player:WalkDown"]));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite("Player:WalkLeft", Graphics.CTextures.textures["Player:WalkLeft"]));
            _imageIndex.Add("PlayerWalkRight", new Graphics.CSprite("Player:WalkLeft", Graphics.CTextures.textures["Player:WalkLeft"],null,true));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite("Player:WalkUp", Graphics.CTextures.textures["Player:WalkUp"]));

            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite("Player:IdleDown", Graphics.CTextures.textures["Player:IdleDown"]));
            _imageIndex.Add("PlayerIdleUp", new Graphics.CSprite("Player:IdleUp", Graphics.CTextures.textures["Player:IdleUp"]));
            _imageIndex.Add("PlayerIdleLeft", new Graphics.CSprite("Player:IdleLeft", Graphics.CTextures.textures["Player:IdleLeft"]));
            _imageIndex.Add("PlayerIdleRight", new Graphics.CSprite("Player:IdleLeft", Graphics.CTextures.textures["Player:IdleLeft"],null,true));

            _imageIndex.Add("PlayerSwingUp", new Graphics.CSprite("Player:SwingUp", Graphics.CTextures.textures["Player:SwingUp"]));
            _imageIndex.Add("PlayerSwingDown", new Graphics.CSprite("Player:SwingDown", Graphics.CTextures.textures["Player:SwingDown"]));
            _imageIndex.Add("PlayerSwingRight", new Graphics.CSprite("Player:SwingLeft", Graphics.CTextures.textures["Player:SwingLeft"],null, true));
            _imageIndex.Add("PlayerSwingLeft", new Graphics.CSprite("Player:SwingLeft", Graphics.CTextures.textures["Player:SwingLeft"]));

            _imageIndex.Add("PlayerRollDown", new Graphics.CSprite("Player:RollDown", Graphics.CTextures.textures["Player:RollDown"]));
            _imageIndex.Add("PlayerRollUp", new Graphics.CSprite("Player:RollUp", Graphics.CTextures.textures["Player:RollUp"]));
            _imageIndex.Add("PlayerRollLeft", new Graphics.CSprite("Player:RollLeft", Graphics.CTextures.textures["Player:RollLeft"]));
            _imageIndex.Add("PlayerRollRight", new Graphics.CSprite("Player:RollLeft", Graphics.CTextures.textures["Player:RollLeft"], null, true));

        }

        public override void collide(object sender, CActor collider)
        {
            if (collider.GetType() == typeof(CSolidTile) || collider.GetType() == typeof(Items.decoration.CPot)) //this is gonna need to be a bit more elegant
            {
                solidCollide(collider);
            }
        }

        private void solidCollide(CActor collider)
        {
            //Calculate How much to move to get out of collision moving towards last collisionless point
			CHitBox otherbox = collider.hitBox;
			
			//Calculate how far in we went
			float distx = (collider.position.X + otherbox.center.X) - (position.X + hitBox.center.X);
			distx = (float)Math.Sqrt(distx * distx);
			float disty = (position.Y + hitBox.center.Y) - (collider.position.Y + otherbox.center.Y);
			disty = (float)Math.Sqrt(disty * disty);
			
			float lenx = hitBox.halfWidth + otherbox.halfWidth;
			float leny = hitBox.halfHeight + otherbox.halfHeight;
			
			int px = 1;
			int py = 1;
			
			if (collider.position.X < position.X)
				px = -1;
			if (collider.position.Y < position.Y+(hitBox.halfHeight*2))
				py = -1;
			
			float penx = px*(distx - lenx);
			float peny = py*(disty - leny);
			//Resolve closest to previous position
			float diffx = (position.X + penx) - _oldPosition.X;
			diffx *= diffx;
			float diffy = (position.Y + peny) - _oldPosition.Y;
			diffy *= diffy;

            if (diffx < diffy)
                _position.X += penx; //TODO: dont make a new vector every time
            else if (diffx > diffy)
                _position.Y += peny; //Same here
            else
                position = new Vector2(position.X + penx, position.Y + peny); //Corner cases
        }

        public override void create(object sender)
        {
            throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case "Swinging":
                      _state = "Idle";
                    break;

                case "Rolling":
                    _state = "Idle";
                    break;
            }

            
        }

        public override void frame(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if (_state == "Idle" || _state == "Moving")
            {
                //Store this so we can type less
                CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
                if (input.keysPressed.Contains(Keys.End))
                {
                    Graphics.CGraphics.changeResolution(320, 240);
                    Master.Pop();
                }

                if (input.keysPressed.Contains(Keys.A))
                {
                    _position.X -= 1;
                    image = _imageIndex["PlayerWalkLeft"];
                    _direction = DIRECTION.LEFT;
                    _state = "Moving";
                }

                if (input.keysPressed.Contains(Keys.D))
                {
                    _position.X += 1;
                    image = _imageIndex["PlayerWalkRight"];
                    _direction = DIRECTION.RIGHT;
                    _state = "Moving";
                }

                if (input.keysPressed.Contains(Keys.W))
                {
                    _position.Y -= 1;
                    image = _imageIndex["PlayerWalkUp"];
                    _direction = DIRECTION.UP;
                    _state = "Moving";
                }

                if (input.keysPressed.Contains(Keys.S))
                {
                    _position.Y += 1;
                    image = _imageIndex["PlayerWalkDown"];
                    _direction = DIRECTION.DOWN;
                    _state = "Moving";
                }

                if (input.keysPressed.Contains(Keys.LeftShift) && _state == "Moving")
                {
                    _state = "Rolling";
                    _rollReleased = false;
                    //get the FUCK out of this
                    return;
                }

                if (input.keysPressed.Contains(Keys.Space))
                {
                    _state = "Swinging";
                    _swordReleased = false;
                }
            }
        }

        public override void keyRelease(object sender)
        {
            if (!(Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
            {
                if (_state == "Moving")
                    _state = "Idle";
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {

                case "Rolling":
                    if (!_rollReleased)
                    {
                        _rollReleased = true;
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:Attack3"]);
                    }
                        switch (_direction)
                        {

                            case DIRECTION.DOWN:
                                swapImage("PlayerRollDown");
                                _position.Y += 2;
                                break;

                            case DIRECTION.UP:
                                swapImage("PlayerRollUp");
                                _position.Y -= 2;
                                break;

                            case DIRECTION.LEFT:
                                swapImage("PlayerRollLeft");
                                _position.X -= 2;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage("PlayerRollRight");
                                _position.X += 2;
                                break;
                        }
                    
                    break;

                case "Swinging":
                    if (!_swordReleased)
                    {
                        _swordReleased = true;
                        Vector2 swordPos = Vector2.Zero;
                        Random random = new Random();
                        int attackSound = random.Next(0, 3);

                        Sound.CSound[] temp = new Sound.CSound[4];

                        temp[0] = CMasterControl.audioPlayer.soundBank["Player:Attack1"];
                        temp[1] = CMasterControl.audioPlayer.soundBank["Player:Attack2"];
                        temp[2] = CMasterControl.audioPlayer.soundBank["Player:Attack3"];
                        temp[3] = CMasterControl.audioPlayer.soundBank["Player:Attack4"];

                        CMasterControl.audioPlayer.addSfx(temp[attackSound]);
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:SwordSlash"]);

                        switch (_direction)
                        {
                            case DIRECTION.UP:
                                swapImage("PlayerSwingUp");
                                swordPos.X = _position.X - 13;
                                swordPos.Y = _position.Y - 13;
                                break;

                            case DIRECTION.LEFT:
                                swapImage("PlayerSwingLeft");
                                swordPos.X = _position.X - 18;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage("PlayerSwingRight");
                                swordPos.X = _position.X - 12;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.DOWN:
                                swapImage("PlayerSwingDown");
                                swordPos.X = _position.X - 17;
                                swordPos.Y = _position.Y - 13;
                                break;
                        }

                        _triggerUserEvent(0, "sword", _direction.ToString(), swordPos.X.ToString(), swordPos.Y.ToString());
                    }

                    break;
                case "Idle":
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            swapImage("PlayerIdleDown", false);
                            break;

                        case DIRECTION.UP:
                            swapImage("PlayerIdleUp", false);
                            break;

                        case DIRECTION.LEFT:
                            swapImage("PlayerIdleLeft", false);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage("PlayerIdleRight", false);
                            break;
                    }

                    break;
            }
            _readableCoords = _position;
        }

        public override void drawMe()
        {
            base.drawMe();
        }

        public static float glblX
        {
            get
            {
                return _readableCoords.X;
            }
        }

        public static float glblY
        {
            get
            {
                return _readableCoords.Y;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.NPC.Enemies.Keese.CKeese));
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
            _collidables.Add(typeof(Actors.Items.decoration.CPot));
        }
    }
}
