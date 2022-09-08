using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class IngameManager
    {
        private const float LOSE_PLAYER_SIZE = .4f;
        public readonly float ChangeSizeTick = .005f;
        private readonly IFacade _facade;
        public readonly int AreasOnLevelCount = 5;
        public Predicate<float> OnCheckGameStatus;
        public Action OnPlayerWonOnDoorstep;
        public event Action OnPlayerLose;
        public event Action OnPlayerGotFreeRoad;
        public event Action OnPlayerWon;
        public event Action OnIngameLateUpdate;
        private float _currentPlayerSize = 1f;

        public IngameManager(IFacade facade)
        {
            _facade = facade;
        }

        public void Initialize()
        {
            _facade.InputManager.OnPlayerPressOnScreenEnd += CheckGameStatus;
            _facade.InputManager.OnPlayerPressOnScreen += ChangePlayerSize;
        }

        private void ChangePlayerSize()
        {
            _currentPlayerSize -= ChangeSizeTick;
            if (_currentPlayerSize < LOSE_PLAYER_SIZE)
            {
                PlayerLose();
            }
        }

        public void CheckGameStatus(Vector3 startPos)
        {
            if (OnCheckGameStatus?.Invoke(_facade.BallManager.CurrentPlayerBallSize) == true)
            {
                OnPlayerGotFreeRoad?.Invoke();
            }
        }

        public void IngameLateUpdate()
        {
            OnIngameLateUpdate?.Invoke();
        }

        public void PlayerWon()
        {
            Time.timeScale = 0f;
            OnPlayerWon?.Invoke();
        }

        public void PlayerLose()
        {
            Time.timeScale = 0f;
            OnPlayerLose?.Invoke();
        }
       
        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public float CurrentPlayerSize => _currentPlayerSize;
    }
}
