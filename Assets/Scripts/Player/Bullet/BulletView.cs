using UnityEngine;

namespace Assets.Scripts.Player.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private const float BULLET_SPEED = 10f;
        private readonly Vector3 START_SCALE = new Vector3(.1f, .1f, .1f);
        private Vector3 _direction = Vector3.zero;
        private bool _canMove = false;

        public void SetMoveDirection(Vector3 direction)
        {
            _canMove = true;
            _direction = direction;
        }

        public void ResetBullet()
        {
            transform.localScale = START_SCALE;
            _canMove = false;
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                transform.position += _direction * BULLET_SPEED * Time.fixedDeltaTime;
            }
        }
    }
}
