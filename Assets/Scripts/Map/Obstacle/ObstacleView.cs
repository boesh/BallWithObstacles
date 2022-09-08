using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.Pool;

namespace Assets.Scripts.Map.Obstacle
{
    public class ObstacleView : MonoBehaviour
    {
        private const string INFECTION_TRIGGER_NAME = "Infection";
        private IFacade _facade;
        [SerializeField]
        private PoolObject _poolObject;
        [SerializeField]
        private Animator _obstacleAnimator;
        private bool _isInfected = false;

        private void OnTriggerEnter(Collider other)
        {
            other.transform.parent.GetComponent<PoolObject>().ReturnToPool();
            StartInfection();
            _isInfected = true;
        }

        private void StartInfection()
        {
            _obstacleAnimator.SetTrigger(INFECTION_TRIGGER_NAME);
        }

        public void SetupView(IFacade facade)
        {
            _facade = facade;
        }

        private void InfectionAnimationCallback()
        {
            _poolObject.ReturnToPool();
            _facade.IngameManager.CheckGameStatus(Vector3.zero);
        }

        public bool IsInfected => _isInfected;
    }
}
