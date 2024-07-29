using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerInputController : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Rigidbody _rigidbody;
    
    private WizardConfig _wizardConfig;
    private Camera _camera;
    private Vector3 _moveDirection;
    private Vector3 _rotationDirection;
    private Vector3 _cameraCharacterDelta;
    public event Action OnAttack;
    public event Action OnNextSkill;
    public event Action OnPreviousSkill;
    public Vector3 GetPosition()
    {
        return transform.position;
    }


    [Inject]
    private void Init(Camera cameraInjected, WizardConfig wizardConfig)
    {
        _camera = cameraInjected;
        _cameraCharacterDelta = gameObject.transform.position + _camera.transform.position;
        _wizardConfig = wizardConfig;
    }

    private void Update()
    {
        _moveDirection = Vector3.forward * Input.GetAxis("Vertical");
        _rotationDirection = Vector3.right * Input.GetAxis("Horizontal");
        
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

    private void FixedUpdate()
    {
        var rotation = _rigidbody.rotation;
        if (!Mathf.Approximately(_rotationDirection.x, 0))
        {
            int direction = _rotationDirection.x > 0 ? 1 : -1;

            rotation *= Quaternion.Euler(Vector3.up * direction * _wizardConfig.rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(rotation);
        }

        bool wall = Physics.Raycast(transform.position + Vector3.up * 0.5f, rotation * _moveDirection,
            out RaycastHit hit, 1.5f, 1 << (int)GameLayers.Walls);

        if (!wall)
        {
            _rigidbody.MovePosition(_rigidbody.position +
                                    rotation * _moveDirection * (_wizardConfig.movementSpeed * Time.fixedDeltaTime));
        }
    }

    private void LateUpdate()
    {
        var transform1 = _camera.transform;
        var position = _rigidbody.position;
        var pos = new Vector3(position.x + _cameraCharacterDelta.x, transform1.position.y,
            position.z + _cameraCharacterDelta.z);
        transform1.position = pos;
    }
}