using Skills;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IPlayerInput
    {
        event Action<Vector2, bool> OnMove;
        event Action<SkillType> OnAttack;
        event Action OnNextSkill;
        event Action OnPreviousSkill;
    }
}