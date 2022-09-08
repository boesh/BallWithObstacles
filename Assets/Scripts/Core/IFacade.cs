using Assets.Scripts.Map;
using Assets.Scripts.Player;

namespace Assets.Scripts.Core
{
    public interface IFacade
    {
        public PlayerManager BallManager
        {
            get;
        }

        public MapManager MapManager
        {
            get;
        }

        public InputManager InputManager
        {
            get;
        }

        public IngameManager IngameManager
        {
            get;
        }
    }
}
