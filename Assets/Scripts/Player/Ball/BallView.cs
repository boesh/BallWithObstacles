using UnityEngine;

namespace Assets.Scripts.Player.Ball
{
    public class BallView : MonoBehaviour
    {
        private const string MOVE_TRIGGER_NAME = "Move";
        [SerializeField]
        private Animator _animator;

        public void MoveToPoint()
        {
            _animator.SetTrigger(MOVE_TRIGGER_NAME);
        }
    }
}
