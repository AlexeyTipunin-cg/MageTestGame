using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IPlayerInput
{
    event Action OnAttack;
    event Action OnNextSkill;
    event Action OnPreviousSkill;
    Vector3 GetPosition();
}