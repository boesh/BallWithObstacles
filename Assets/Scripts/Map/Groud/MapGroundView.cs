using UnityEngine;

namespace Assets.Scripts.Map.Ground
{
    public class MapGroundView : MonoBehaviour
    {
        [SerializeField]
        private Transform _road;
        [SerializeField]
        private Transform _fullArea;

        public void DecreaseRoadSize(float currentPlayerSize)
        {
            _road.localScale = new Vector3(currentPlayerSize / 10f,
                _road.transform.localScale.y, _road.transform.localScale.z);
        }
    }
}
