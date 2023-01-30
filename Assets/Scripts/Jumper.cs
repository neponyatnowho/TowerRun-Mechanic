using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] float _startJumpForce = 50;
    [SerializeField] float _multiplierJumpForce = 20;
    [SerializeField] PlayerTower _playerTower;
    
    private Rigidbody _rigitbody;
    private float _jumpForce;
    private bool _isGrounded;
    private HumanAnimator _mainHuman;

    private void OnEnable()
    {
        _playerTower.PlayerCountInTowerChanged += CalculateForce;
        _mainHuman = _playerTower.MainHumanAnimator;
        _rigitbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _playerTower.PlayerCountInTowerChanged -= CalculateForce;
    }

    private void Jump()
    {
        _mainHuman.JumpAnimation();
        _rigitbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }


    private void CalculateForce(int countOfPeople)
    {
        if(countOfPeople > 1)
            _mainHuman.IdleAnimation();

        _mainHuman = _playerTower.MainHumanAnimator;

        float forceUpgrade = _multiplierJumpForce * (countOfPeople - 1);

        if (countOfPeople > 3)
            forceUpgrade = _multiplierJumpForce * 3;

        _jumpForce = _startJumpForce + forceUpgrade;

    }


    private void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0) && _isGrounded == true)
        {
            Jump();
            _isGrounded = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrounded = true;
            _mainHuman.RunAnimation();
        }
    }
}
