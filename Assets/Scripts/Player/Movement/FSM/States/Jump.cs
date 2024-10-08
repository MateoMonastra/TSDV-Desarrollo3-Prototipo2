using System;
using System.Collections;
using Player;
using Player.Jump;
using UnityEngine;

namespace Gameplay.Player.FSM.States
{
    public class Jump : State
    {
        public Action Jumped;

        [Tooltip("Model defining jump parameters")]
        [SerializeField] private JumpModel model;

        [Header("References")]

        private GroundCheck _groundCheck;
        private bool _canJump;
        private Rigidbody _rb;

        public override void OnEnter()
        {
            _rb = GetComponentInChildren<Rigidbody>();
            _groundCheck = GetComponent<GroundCheck>();
            
            Reset();
  
            StartCoroutine(OnJump());
        }

        public override void OnUpdate()
        { 
        }

        /// <summary>
        /// Coroutine that handles the jump mechanics.
        /// </summary>
        private IEnumerator OnJump()
        {
            if (!_canJump)
                yield break;

            if (!_groundCheck.IsOnGround() || _groundCheck.IsOnGround())
            {
                _canJump = false;

                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

                yield return new WaitForFixedUpdate();
                _rb.AddForce(transform.up * model.jumpForce, ForceMode.Impulse);
                Jumped.Invoke();
                yield return null;
                Invoke(nameof(Reset), model.JumpCooldown);
            }
        }

        /// <summary>
        /// Resets the ability to jump.
        /// </summary>
        private void Reset()
        {
            _canJump = true;
        }
    }
}
