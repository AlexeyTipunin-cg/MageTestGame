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
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _rotationSpeed = 1;
    private Vector3 _moveDirection;
    private Vector3 _rotationDirection;
    private Vector3 _cameraCharacterDelta;

    private void Awake()
    {
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
        // Debug.Log(_moveDirection * _speed * Time.fixedDeltaTime);

       //  Quaternion.
       // var rotation = Quaternion.RotateTowards(_rigidbody.rotation,
       //      Quaternion.Euler(Vector3.up * _rotationDirection.x * _rotationSpeed * Time.fixedDeltaTime), 360);
       

       var rotation = _rigidbody.rotation *
                      Quaternion.Euler(Vector3.up * _rotationDirection.x * _rotationSpeed * Time.fixedDeltaTime);
       _rigidbody.MoveRotation(rotation);
       _rigidbody.MovePosition(_rigidbody.position + rotation * _moveDirection * (_speed * Time.fixedDeltaTime));
       
       // _rigidbody.velocity = 
       
            
        // Debug.Log("Character velocity" + _rigidbody.velocity);
    }

    private void LateUpdate()
    {
        var transform1 = _camera.transform;
        var position = _rigidbody.position;
        var pos = new Vector3(position.x + _cameraCharacterDelta.x, transform1.position.y, position.z + _cameraCharacterDelta.z);
        transform1.position = pos;
    }
}