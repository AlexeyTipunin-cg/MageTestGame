using UnityEngine;

namespace Assets.Scripts.Player
{
    public class MoveState : BaseState
    {
        private Vector3 _previousPosition;
        private float _verticalDirection;
        private Vector2 _moveDirection;
        private Vector2 _rotationDirection;
        private CreatureConfig _creatureConfig;

        public MoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _creatureConfig = stateMachine.Player.WizardConfig;
        }

        public override void AddInputCallbacks()
        {
            _stateMachine.Player.Input.OnMove += OnLeftStick;
        }

        public override void Enter()
        {
            base.Enter();

            _previousPosition = _stateMachine.Player.RigidBody.position;
            _stateMachine.AnimtaionController.RunAnimation();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void OnLeftStick(Vector2 vector, bool isMoving)
        {
            if (!isMoving)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }

            _verticalDirection = vector.y;
            _moveDirection = Vector3.forward * vector;
            _rotationDirection = vector;
        }
        public override void FixedUpdate()
        {
            _previousPosition = _stateMachine.Player.RigidBody.position;

            Vector3 lookDirection = new Vector3(_rotationDirection.x, 0, _rotationDirection.y);
            var rot = Quaternion.LookRotation(lookDirection);
            _stateMachine.Player.RigidBody.rotation = rot;

            if (_stateMachine.Player.WallDetection.BumpInWall)
            {
                return;
            }

            Vector3 newPosition = _previousPosition + Vector3.ClampMagnitude(lookDirection * _creatureConfig.movementSpeed, _creatureConfig.movementSpeed) * Time.fixedDeltaTime;

            bool insideScene = _stateMachine.Player.SceneLimits.isInsideScene(newPosition);

            if (!insideScene)
            {
                return;
            }

            _stateMachine.Player.RigidBody.position = newPosition;

            _stateMachine.Player.UpdateCameraPosition();
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
