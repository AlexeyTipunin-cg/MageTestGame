using System;
using Assets.Scripts.Damage;
using Assets.Scripts.Player;
using Enemy;
using UnityEngine;

namespace Skills
{
    public class StrikeSkillView : SkillView
    {
        [SerializeField]private DamageComponent _damageComponent;

        public override void Init(PlayerModel playerModel, SkillModel model)
        {
            base.Init(playerModel, model);
            _damageComponent.Init(_config.damage);
        }
    }
}