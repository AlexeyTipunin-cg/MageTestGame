using System;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using DefaultNamespace;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using UniRx;

public class PlayerInputController : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Rigidbody _rigidbody;

    private CreatureConfig _wizardConfig;
    private Camera _camera;
    private Vector3 _moveDirection;
    private Vector3 _rotationDirection;
    private Vector3 _cameraCharacterDelta;
    public event Action OnAttack;
    public event Action OnNextSkill;
    public event Action OnPreviousSkill;

    private ISceneLimits _sceneLimits;
    private bool _isGameOver;

    private float _verticalDirection;

    [Inject]
    private void Init(PlayerModel playerModel)
    {
        playerModel.isDead.Subscribe(isDead =>
        {
            _isGameOver = isDead;
        });
    }

    private void Update()
    {
        if (_isGameOver) { return; }

        if (Input.GetKeyDown(KeyCode.X))
        {
            OnAttack?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnPreviousSkill?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnNextSkill?.Invoke();
        }
    }
}