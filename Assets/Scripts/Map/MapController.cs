using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Map.Door;
using Assets.Scripts.Map.Ground;
using Assets.Scripts.Map.Obstacle;
using Assets.Scripts.Pool;

namespace Assets.Scripts.Map
{
    public class MapController
    {
        private readonly IFacade _facade;
        private List<MapGroundView> _currentAreas;
        private List<ObstacleView> _currentObstacles;
        private string _areaPrefabName;
        private string _obstaclePrefabName;
        private string _doorPrefabName;

        public MapController(string mapAreaPrefabName, string obstaclePrefabName, string doorPrefabName, IFacade facade)
        {
            _facade = facade;
            _currentAreas = new List<MapGroundView>();
            _currentObstacles = new List<ObstacleView>();
            _areaPrefabName = mapAreaPrefabName;
            _obstaclePrefabName = obstaclePrefabName;
            _doorPrefabName = doorPrefabName;
            _facade.InputManager.OnPlayerPressOnScreen += DecreaseRoadSize;
            _facade.IngameManager.OnCheckGameStatus += CheckRoad;
        }

        public void GenerateMapArea(Vector3 position)
        {
            _currentAreas.Add(PoolManager.GetObject(_areaPrefabName, position, Quaternion.identity).GetComponent<MapGroundView>());
        }

        public void GenerateObstaclesOnRoad(float positionZ)
        {
            float randXMin = -1f;
            float randZMin = -5f + positionZ;
            while (randZMin < 4f + positionZ)
            {
                var resultX = UnityEngine.Random.Range(randXMin, 1f);
                randZMin = UnityEngine.Random.Range(randZMin + 1f, randZMin + 2f);
                ObstacleView obstacleView = PoolManager.GetObject(_obstaclePrefabName, new Vector3(resultX, 1f, randZMin), 
                    Quaternion.identity).GetComponent<ObstacleView>();
                obstacleView.SetupView(_facade);
                _currentObstacles.Add(obstacleView);
            }
        }

        public void GenerateDoors(Vector3 position)
        {
            DoorView doorView = PoolManager.GetObject(_doorPrefabName, position, Quaternion.identity).GetComponent<DoorView>();
            doorView.SetupView(_facade);
        }

        private void DecreaseRoadSize()
        {
            foreach (var area in _currentAreas)
            {
                area.DecreaseRoadSize(_facade.IngameManager.CurrentPlayerSize);
            }
        }

        private bool CheckRoad(float currentSize)
        {
            foreach (ObstacleView obs in _currentObstacles)
            {
                if (!obs.IsInfected && obs.transform.position.x > -currentSize / 2f - .5f && obs.transform.position.x < currentSize / 2f + .5f)
                {
                    return false;
                }
            }
            return true;
        }

        public MapGroundView LastGroundView
        {
            get
            {
                if (_currentAreas == null || _currentAreas.Count < 1)
                {
                    return null;
                }
                else
                {
                    return _currentAreas[_currentAreas.Count - 1];
                }
            }
        }
    }
}
