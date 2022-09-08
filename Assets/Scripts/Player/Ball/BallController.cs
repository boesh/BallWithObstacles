using UnityEngine;
using Assets.Scripts.Core;

namespace Assets.Scripts.Player.Ball
{
    public class BallController
    {
        private readonly IFacade _facade;
        private BallView _playerBall;

        public BallController(BallView playerBall, IFacade facade)
        {
            _facade = facade;
            _playerBall = playerBall;
            _facade.InputManager.OnPlayerPressOnScreen += DecreaseSize;
            _facade.IngameManager.OnPlayerGotFreeRoad += MoveToNextPoint;
        }

        private void MoveToNextPoint()
        {
            _playerBall.MoveToPoint();
            _facade.IngameManager.OnIngameLateUpdate += OnIngameLateUpdate;
        }

        private void OnIngameLateUpdate()
        {
            if (_playerBall.transform.position.z > _facade.MapManager.LastGroundView.transform.position.z - 5f)
            {
                _facade.IngameManager.OnPlayerWonOnDoorstep?.Invoke();
            }
            if (_playerBall.transform.position.z > _facade.MapManager.LastGroundView.transform.position.z + 5f)
            {
                _facade.IngameManager.PlayerWon();
                _facade.IngameManager.OnIngameLateUpdate -= OnIngameLateUpdate;
            }
        }

        private void DecreaseSize()
        {
            _playerBall.transform.localScale = Vector3.one * _facade.IngameManager.CurrentPlayerSize;
        }
    }
}
