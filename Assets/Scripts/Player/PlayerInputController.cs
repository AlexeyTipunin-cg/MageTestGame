using System;
using Assets.Scripts.Player;
using Skills;
using UnityEngine;

public class PlayerInputController : IPlayerInput, IDisposable
{
    private PlayerInput _playerInput;

    public event Action<SkillType> OnAttack;
    public event Action OnNextSkill;
    public event Action OnPreviousSkill;

    public event Action<Vector2, bool> OnMove;

    public PlayerInputController()
    {
        _playerInput = new PlayerInput();
        EnableInput();
    }

    public void Dispose()
    {
        DisableInput();
    }

    private void EnableInput()
    {
        _playerInput.Enable();
        _playerInput.Movement.Position.performed += OnLeftStick;
        _playerInput.Movement.Position.canceled += OnLeftCancel;

        _playerInput.Movement.Skill_1.performed += OnSkill1;
        _playerInput.Movement.Skill_2.performed += OnSkill2;
        _playerInput.Movement.Skill_3.performed += OnSkill3;
    }

    private void DisableInput()
    {
        _playerInput.Movement.Position.performed -= OnLeftStick;
        _playerInput.Movement.Position.canceled -= OnLeftCancel;

        _playerInput.Movement.Skill_1.performed -= OnSkill1;
        _playerInput.Movement.Skill_2.performed -= OnSkill2;
        _playerInput.Movement.Skill_3.performed -= OnSkill3;
        _playerInput.Disable();
    }

    private void OnLeftStick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        OnMove?.Invoke(value, true);
    }

    private void OnLeftCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        OnMove?.Invoke(Vector2.zero, false);
    }

    private void OnSkill3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(SkillType.Heal);
    }

    private void OnSkill2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(SkillType.Aoe);
    }

    private void OnSkill1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(SkillType.Strike);

    }
}