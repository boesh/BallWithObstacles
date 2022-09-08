using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class UIManager
    {
        private readonly IFacade _facade;
        private UIView _view;
        public UIManager(UIView view, IFacade facade)
        {
            _facade = facade;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetupView(_facade);
        }
    }
}
