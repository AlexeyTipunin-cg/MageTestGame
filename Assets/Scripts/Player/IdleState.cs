using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void AddInputCallbacks()
        {
            _stateMachine.Player.Input.OnMove += OnLeftStick;

        }

        private void OnLeftStick(Vector2 vector, bool isMoving)
        {
            if (isMoving)
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
            }
        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.AnimtaionController.IdleAnimation();
        }

        public override void FixedUpdate()
        {

        }

        public override void RemoveInputCallbacks()
        {
            _stateMachine.Player.Input.OnMove -= OnLeftStick;

        }

        public override void Update()
        {

        }
    }
}
