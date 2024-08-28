using Gameplay.Player.FSM.States;
using Player.Movement.FSM.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Player.FSM.Behaviours
{
    public class WalkIdleIBehaviour : MonoBehaviour, IBehaviour
    {
       [SerializeField]private WalkIdle walkIdle;
        
        private void Start()
        {
            walkIdle ??= GetComponent<WalkIdle>();
        }

        public bool CheckTransitionIsApproved(IBehaviour newBehaviour)
        {
            return walkIdle.CheckStateTransition(newBehaviour.GetBehaviourState());
        }

        public void Enter()
        {
            walkIdle.OnEnter();
        }

        public void OnBehaviourUpdate()
        {
            walkIdle.OnUpdate();
        }

        public void OnBehaviourFixedUpdate()
        {
            walkIdle.OnFixedUpdate();
        }

        public void OnBehaviourLateUpdate()
        {
            walkIdle.OnLateUpdate();
        }

        public void Exit()
        {
            walkIdle.OnEnd();
        }

        public State GetBehaviourState()
        {
            return walkIdle;
        }

        /// <summary>
        /// Sets the movement direction for the walk idle state.
        /// </summary>
        /// <param name="newDirection">The new direction to be set.</param>
        public void SetDirection(Vector2 newDirection)
        {
            walkIdle.SetDirection(newDirection);
        }
    }
}
