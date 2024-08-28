using System;
using Gameplay.Player.FSM;
using Player.Movement.FSM;
using UnityEngine;

namespace Player.PlayerCam
{
    public class PlayerCamManager : MonoBehaviour
    {
        public Action OnCameraRotation;

        [Header("Input Reader")] 
        [Tooltip("Reference to the InputReaderFsm scriptable object.")] 
        [SerializeField] private InputReaderFsm inputReader;

        private Gameplay.Player.PlayerCam.PlayerCam _playerCam;

        private void Start()
        {
            inputReader.OnMouseCam += MoveMouseCam;
            inputReader.OnJoystickCam += MoveJoystickCam;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _playerCam = GetComponentInChildren<Gameplay.Player.PlayerCam.PlayerCam>();
        }

        /// <summary>
        /// Updates the camera angle based on mouse input.
        /// </summary>
        /// <param name="angle">The angle to move the camera.</param>
        private void MoveMouseCam(Vector2 angle)
        {
            _playerCam.UpdateMouseCamera(angle);
            OnCameraRotation.Invoke();
        }

        /// <summary>
        /// Updates the camera angle based on joystick input.
        /// </summary>
        /// <param name="angle">The angle to move the camera.</param>
        private void MoveJoystickCam(Vector2 angle)
        {
            _playerCam.UpdateJoystickCamera(angle);
            OnCameraRotation.Invoke();
        }
    }
}