using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IPlayerInput
    {
        event Action OnAttack;
        event Action OnNextSkill;
        event Action OnPreviousSkill;
    }
}