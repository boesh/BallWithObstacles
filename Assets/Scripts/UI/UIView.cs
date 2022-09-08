using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class UIView : MonoBehaviour
    {
        private IFacade _facade;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private GameObject _loseText;

        public void SetupView(IFacade facade)
        {
            _facade = facade;
            _facade.IngameManager.OnPlayerLose += SetLoseView;
            _facade.IngameManager.OnPlayerWon += SetWinView;
            _restartButton.onClick.AddListener(OnResetButtonPressed);
        }

        public void SetWinView()
        {
            _restartButton.gameObject.SetActive(true);
            _winText.SetActive(true);
        }

        public void SetLoseView()
        {
            _restartButton.gameObject.SetActive(true);
            _loseText.SetActive(true);
        }

        public void OnResetButtonPressed()
        {
            _facade?.IngameManager.Restart();
        }
    }
}
