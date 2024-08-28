using System;
using Player.Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.FSM
{
    public class InputReaderFsm : MonoBehaviour
    {
        public Action<Vector2> OnMove;
        public Action OnJump;
        public Action OnShoot;
        public Action<Vector2> OnMouseCam;
        public Action<Vector2> OnJoystickCam;

        public LauncherGun launcherGun;
        public Grab grab;

        /// <summary>
        /// Maneja la entrada de salto.
        /// </summary>
        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnJump.Invoke();
            }
        }

        /// <summary>
        /// Maneja la entrada de movimiento.
        /// </summary>
        public void HandleMoveInput(InputAction.CallbackContext context)
        {
            OnMove.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Maneja la entrada de cámara (ratón o joystick).
        /// </summary>
        public void HandleCamInput(InputAction.CallbackContext context)
        {
            if (context.control == Mouse.current)
            {
                if (context.performed)
                {
                    OnMouseCam.Invoke(context.ReadValue<Vector2>());
                }
                else
                {
                    OnMouseCam.Invoke(Vector2.zero);
                }
            }
            else
            {
                OnJoystickCam.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void HandleShootInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                launcherGun.ResetValue();
            }
            
            if (context.canceled)
            {
                launcherGun.isUpgrading = false;
                launcherGun.isShooting = true;
            }
        }
        
        public void HandleGrabInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                grab.TryGrab();
            }
            if (context.canceled)
            {
                grab._attempToCatch = false;
            }
        }

    }
}