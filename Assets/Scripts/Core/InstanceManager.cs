using UnityEngine;
using Assets.Scripts.Camera;
using Assets.Scripts.Map;
using Assets.Scripts.Map.Door;
using Assets.Scripts.Map.Ground;
using Assets.Scripts.Map.Obstacle;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Ball;
using Assets.Scripts.Pool;
using Assets.Scripts.UI;

namespace Assets.Scripts.Core
{
    public class InstanceManager : MonoBehaviour, IFacade
    {
        [SerializeField]
        private BallView _ballView;
        [SerializeField]
        private MapGroundView _mapView;
        [SerializeField]
        private ObstacleView _obstacleView;
        [SerializeField]
        private DoorView _doorView;
        [SerializeField]
        private UIView _uiView;
        [SerializeField]
        private CameraView _cameraView;
        [SerializeField]
        private PoolManager.PoolPart[] _pools;
        private PlayerManager _playerManager;
        private MapManager _mapManager;
        private InputManager _inputManager;
        private IngameManager _ingameManager;
        private UIManager _uiManager;

        void OnValidate()
        {
            for (int i = 0; i < _pools.Length; ++i)
            {
                _pools[i].name = _pools[i].prefab.name;
            }
        }

        private void Awake()
        {
            PoolManager.Initialize(_pools);
            _inputManager = new InputManager(this);
            _ingameManager = new IngameManager(this);
            _playerManager = new PlayerManager(_ballView, this);
            _mapManager = new MapManager(_mapView.name, _obstacleView.name, _doorView.name, this);
            _uiManager = new UIManager(_uiView, this);
            _inputManager.Initialize();
            _mapManager.Initialize();
            _ingameManager.Initialize();
            _playerManager.Initalize();
            _uiManager.Initialize();
            _cameraView.SetupView(_playerManager.CurrentPlayerTransform);
        }

        private void Update()
        {
            _inputManager.InputsUpdate();
        }

        private void FixedUpdate()
        {
            _inputManager.InputsFixedUpdate();
        }

        private void LateUpdate()
        {
            _ingameManager.IngameLateUpdate();
        }

        public PlayerManager BallManager => _playerManager;

        public MapManager MapManager => _mapManager;

        public InputManager InputManager => _inputManager;

        public IngameManager IngameManager => _ingameManager;
    }
}