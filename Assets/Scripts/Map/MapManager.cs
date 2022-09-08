using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.Map.Ground;

namespace Assets.Scripts.Map
{
    public class MapManager
    {
        private readonly IFacade _facade;
        private readonly MapController _mapController;

        public MapManager(string mapAreaPrefabName, string obstaclePrefabName, string doorPrefabName, IFacade facade)
        {
            _facade = facade;
            _mapController = new MapController(mapAreaPrefabName, obstaclePrefabName, doorPrefabName, facade);
        }

        public void Initialize()
        {
            GenerateStartMapAreas();
        }

        private void GenerateMapArea(Vector3 position)
        {
            _mapController.GenerateMapArea(position);
        }

        private void GenerateStartMapAreas()
        {
            for (int i = 0; i < _facade.IngameManager.AreasOnLevelCount; ++i)
            {
                GenerateMapArea(new Vector3(0f, 0f, i * 10f));
                if (i > 0)
                {
                    _mapController.GenerateObstaclesOnRoad(10f * i);
                }
                if (i == _facade.IngameManager.AreasOnLevelCount - 1)
                {
                    _mapController.GenerateDoors(new Vector3(0f, 1.5f, i * 10f + 5f));
                }
            }
        }

        public MapGroundView LastGroundView => _mapController.LastGroundView;
    }
}
