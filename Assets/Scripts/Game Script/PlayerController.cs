using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    PlayerInput _playerInput;
    Vector2 _inputVector;
    Vector3 _moveVector;
    float _rotateSpeed = 180f;
    Vector3 _gravityVector = new Vector3(0, -9.81f, 0);
    [SerializeField] private float playerSpeed = 5f;

    private bool _canMove = true;
    // private bool _canCastAbility = true;

    public static event Action playerChopTree;
    public static event Action playerWalking;
    public static event Action playerIdling;
    public static event Action<int> UseAbility;

    private bool _isSpin = false;


    private void Start()
    {
        playerChopTree += DisableMovement;
    }

    private void OnMovement(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
        _moveVector.x = _inputVector.x;
        _moveVector.z = _inputVector.y;
    }

    private void OnChopTree()
    {
        playerChopTree?.Invoke();
    }

    private void OnFirstAbilityUse()
    {
        CastAbility(1);
    }

    private void OnSecondAbilityUse()
    {
        CastAbility(2);
    }

    private void CastAbility(int abilityIndex)
    {
        UseAbility?.Invoke(abilityIndex);
    }

    private void Update()
    {
        if (!_canMove) return;
        Move();
        RotateTowardsVector();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _controller.Move(_gravityVector * Time.deltaTime);
    }

    private void Move()
    {
        _controller.Move(playerSpeed * _moveVector * Time.deltaTime);
        if (_isSpin)
        {
            return;
        }

        playerWalking?.Invoke();
        if (_inputVector == Vector2.zero)
        {
            playerIdling?.Invoke();
        }
    }

    private void RotateTowardsVector()
    {
        if (_isSpin) return;
        var xzDirection = new Vector3(_moveVector.x, 0, _moveVector.z);
        if (xzDirection.magnitude == 0) return;

        var rotation = Quaternion.LookRotation(xzDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed);
    }

    public void ApplyAbilityEffect(int abilityIndex)
    {
        switch (abilityIndex)
        {
            case 1:
                StartCoroutine(Sprint());
                break;
            case 2:
                StartCoroutine(Spin());
                break;
        }
    }

    private IEnumerator Sprint()
    {
        playerSpeed *= 2;
        yield return new WaitForSeconds(3f);
        RestartPlayerValues();
    }

    private IEnumerator Spin()
    {
        _isSpin = true;
        yield return new WaitForSeconds(3f);
        _isSpin = false;
    }

    private void RestartPlayerValues()
    {
        playerSpeed = 5f;
    }

    public void DisableMovement()
    {
        _canMove = false;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }
}