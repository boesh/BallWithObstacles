using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        private Transform _target;
        
        public void SetupView(Transform target)
        {
            _target = target;
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, _target.position.z - 3.5f), 0.1f);
        }
    }
}
