using UnityEngine;
using Assets.Scripts.Core;

namespace Assets.Scripts.Map.Door
{
    public class DoorView : MonoBehaviour
    {
        private const string OPEN_TRIGGER_NAME = "Open";
        [SerializeField]
        private Animator _animator;

        public void SetupView(IFacade facade)
        {
            facade.IngameManager.OnPlayerWonOnDoorstep += EnableOpenDoorTrigger;
        }

        private void EnableOpenDoorTrigger()
        {
            _animator.SetTrigger(OPEN_TRIGGER_NAME);
        }
    }
}
