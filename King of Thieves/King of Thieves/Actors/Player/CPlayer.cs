using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.Player
{
    class CPlayer : CActor
    {
        private bool _swordReleased = true;

        public CPlayer() :
            base()
        {

            _name = "Player";
            _position = Vector2.Zero;
            //resource init

            
            image = _imageIndex["PlayerWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkDown")));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkLeft")));
            _imageIndex.Add("PlayerWalkRight", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkLeft"),null,true));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkUp")));

            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleDown")));
            _imageIndex.Add("PlayerIdleUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleUp")));
            _imageIndex.Add("PlayerIdleLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleLeft")));
            _imageIndex.Add("PlayerIdleRight", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleLeft"),null,true));

            _imageIndex.Add("PlayerSwingUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:SwingUp")));
            _imageIndex.Add("PlayerSwingDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:SwingDown")));
            _imageIndex.Add("PlayerSwingRight", new Graphics.CSprite(Graphics.CTextures.texture("Player:SwingLeft"),null, true));
            _imageIndex.Add("PlayerSwingLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:SwingLeft")));
        }

        public override void collide(object sender, object collider)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case "Swinging":
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
                

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    _position.X -= 1;
                    image = _imageIndex["PlayerWalkLeft"];
                    _direction = DIRECTION.LEFT;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    _position.X += 1;
                    image = _imageIndex["PlayerWalkRight"];
                    _direction = DIRECTION.RIGHT;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.W))
                {
                    _position.Y -= 1;
                    image = _imageIndex["PlayerWalkUp"];
                    _direction = DIRECTION.UP;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.S))
                {
                    _position.Y += 1;
                    image = _imageIndex["PlayerWalkDown"];
                    _direction = DIRECTION.DOWN;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    Vector2 swordPos = Vector2.Zero;
                    
                    _state = "Swinging";
                    _swordReleased = false;
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
        }

        public override void drawMe()
        {
            base.drawMe();
        }



        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
