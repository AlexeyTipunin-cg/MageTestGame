using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class DeadState : BaseState
    {
        public DeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _stateMachine.AnimtaionController.DieAnimation();
        }
        public override void AddInputCallbacks()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void RemoveInputCallbacks()
        {
        }

        public override void Update()
        {
        }
    }
}
