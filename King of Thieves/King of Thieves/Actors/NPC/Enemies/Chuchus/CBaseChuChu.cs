using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Enemies.Chuchus
{
    //this class should implement everything that's consistent across all chu chus
    public abstract class CBaseChuChu : CBaseEnemy
    {
        //chuchu states
        //wobble: move around
        //popup: come out of the goo pile and start wobbling towards the player
        //attack: jump at the player


        public CBaseChuChu(int sight, float fov, int foh, params dropRate[] drops)
            : base(drops)
        {
            _lineOfSight = sight;
            _visionRange = fov;
            _hearingRadius = foh;
            _state = "idle";
            image = _imageIndex["chuChuIdle"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
        }

        protected override void idle()
        {
            base.idle();

            if (!_huntPlayer)
                return;


            //70% chance of the chu chu popping up to chase the player
            //if (_randNum.NextDouble() <= .7)
            //{
                swapImage("chuChuPopUp");
                _state = "popup";
            //}
        }
        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case "attack":
                    _state = "wobble";
                    swapImage("chuChuWobble", false);
                    break;

                case "popup":
                    _state = "wobble";
                    swapImage("chuChuWobble", false);
                    break;
            }
        }

        public override void timer0(object sender, System.Timers.ElapsedEventArgs e)
        {
            base.timer0(sender, e);
            swapImage("chuChuIdle", false);
            _state = "idle";
        }

        protected override void chase()
        {
            //moveToPoint((int)Player.CPlayer.glblX, (int)Player.CPlayer.glblY, 3);
            moveToPoint((int)Player.CPlayer.glblX, (int)Player.CPlayer.glblY, .25);
            if (MathExt.MathExt.distance(_position, new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)) > _hearingRadius)
            {
                _state = "idle";
                swapImage("chuChuIdle");

                //80% chance of attacking
                    if (_randNum.NextDouble() <= .8)
                    {
                       _state = "attack";
                       swapImage("chuChuHop", false);
                    }

            }

            //base.chase();
        }

        //chu chus are generally retarded and will only have the hop attack
        //override this in the child classes if other functionality is needed
        protected virtual void attack()
        {
            moveToPoint((int)Player.CPlayer.glblX, (int)Player.CPlayer.glblY, 1);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case "wobble":
                    chase();
                    break;

                case "attack":
                    attack();
                    break;

                case "popup":
                    break;

                case "idle":
                    idle();
                    break;

            }
        }
    }
}
