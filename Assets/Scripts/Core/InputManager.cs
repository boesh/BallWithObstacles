using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class InputManager
    {
        private readonly IFacade _facade;
        public event Action OnPlayerPressOnScreenStart;
        public event Action OnPlayerPressOnScreen;
        public event Action<Vector3> OnPlayerPressOnScreenEnd;
        private Vector3 _clickedPos;
        private bool _isPlayerInputsEnabled = false;
        private InputState _inputState = InputState.Empty;

        enum InputState
        {
            Empty,
            Start,
            InProgress,
            End
        }

        public InputManager(IFacade facade)
        {
            _facade = facade;
            _isPlayerInputsEnabled = true;
        }

        public void Initialize()
        {
            _facade.IngameManager.OnPlayerGotFreeRoad += DisablePlayerInputs;
        }

        public void InputsUpdate()
        {
            if (_isPlayerInputsEnabled)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;

                    if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        _clickedPos = hit.point;
                    }
                    OnPlayerPressOnScreenStart?.Invoke();
                }
                else if (Input.GetMouseButton(0))
                {
                    _inputState = InputState.InProgress;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnPlayerPressOnScreenEnd?.Invoke(_clickedPos);
                }
                else
                {
                    _inputState = InputState.Empty;
                }
            }
        }

        public void InputsFixedUpdate()
        {
            switch (_inputState) 
            {
                case InputState.Start:
                    break;
                case InputState.InProgress:
                    OnPlayerPressOnScreen?.Invoke();
                    break;
                case InputState.End:
                    break;
            }
        }

        private void DisablePlayerInputs()
        {
            _isPlayerInputsEnabled = false;
        }
    }
}