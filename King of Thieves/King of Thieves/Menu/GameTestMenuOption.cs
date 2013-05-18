using Gears.Navigation;

namespace King_of_Thieves.Menu
{
    public sealed class GameTestMenuOption : MenuUserControl
    {
        public GameTestMenuOption() :
            base("Game Test")
        {

        }

        public override void ThrowPushEvent()
        {
            Gears.Cloud.Master.Push(new usr.local.PlayableState());
        }
    }
}
