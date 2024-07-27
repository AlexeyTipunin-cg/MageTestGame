using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AttackController _attackController;
    [SerializeField] private WizardConfig _wizardConfig;
    
    private Vector3 _moveDirection;
    private Vector3 _rotationDirection;
    private Vector3 _cameraCharacterDelta;

    private bool _tochWall;

    public PlayerModel PlayerModel { get; set; }

    private void Awake()
    {
        PlayerModel = new PlayerModel(100);
        _cameraCharacterDelta = gameObject.transform.position + _camera.transform.position;
    }

    private void Update()
    {
        _moveDirection = Vector3.forward * Input.GetAxis("Vertical");
        _rotationDirection = Vector3.right * Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.X))
        {
            _attackController.Attack();
        }
        // Debug.Log("_moveDirection" + _moveDirection);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * 2);
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