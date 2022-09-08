using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.Pool;

namespace Assets.Scripts.Player.Bullet
{
    public class BulletController
    {
        private readonly IFacade _facade;
        private const string BULLET_PREFAB_NAME = "Bullet";
        private BulletView _lastBullet;

        public BulletController(IFacade facade)
        {
            _facade = facade;
            _facade.InputManager.OnPlayerPressOnScreenStart += CreateBullet;
            _facade.InputManager.OnPlayerPressOnScreen += IncreaseSize;
            _facade.InputManager.OnPlayerPressOnScreenEnd += MoveBullet;
        }

        private void CreateBullet()
        {
            _lastBullet = PoolManager.GetObject(BULLET_PREFAB_NAME, Vector3.forward, Quaternion.identity).GetComponent<BulletView>();
            _lastBullet.ResetBullet();
        }

        private void IncreaseSize()
        {
            _lastBullet.transform.localScale = new Vector3(_lastBullet.transform.localScale.x + _facade.IngameManager.ChangeSizeTick,
            _lastBullet.transform.localScale.y + _facade.IngameManager.ChangeSizeTick, _lastBullet.transform.localScale.z + _facade.IngameManager.ChangeSizeTick);
        }

        private void MoveBullet(Vector3 direction)
        {
            direction = new Vector3(direction.x, 0f, direction.z);
            _lastBullet.GetComponent<BulletView>().SetMoveDirection(direction.normalized);
        }
    }
}
