using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pendulum2D
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private GameController gameController;

        private PlayerInput input;
        private InputAction tapAction;
        private InputAction spaceAction;

        private bool IsTapped => tapAction.ReadValue<float>() > 0f;
        private bool IsSpacePresed => spaceAction.ReadValue<float>() > 0f;

        private void Start()
        {
            input = GetComponent<PlayerInput>();
            tapAction = input.actions["Tap"];
            spaceAction = input.actions["Space"];
        }

        void Update()
        {
            if (IsTapped || IsSpacePresed)
            {
                gameController.ReleaseCircleFromPendulum();
            }
        }
    }
}
