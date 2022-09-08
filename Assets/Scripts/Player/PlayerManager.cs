using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.Player.Bullet;
using Assets.Scripts.Player.Ball;

namespace Assets.Scripts.Player
{
    public class PlayerManager
    {
        private readonly IFacade _facade;
        private BallController _ballController;
        private BulletController _bulletController;

        private BallView _ballView;

        public PlayerManager(BallView playerView, IFacade facade)
        {
            _facade = facade;
            _ballView = InstanceManager.Instantiate(playerView);
        }

        public void Initalize()
        {
            _ballController = new BallController(_ballView, _facade);
            _bulletController = new BulletController(_facade);
        }

        public Transform CurrentPlayerTransform => _ballView.transform;

        public float CurrentPlayerBallSize => _ballView.transform.localScale.x;
    }
}
