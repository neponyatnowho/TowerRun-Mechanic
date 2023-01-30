using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _pathCreator;

    private Rigidbody _rigitbody;
    private float _distantTraveled = 0f;


    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody>();

        _rigitbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distantTraveled));
    }

    private void FixedUpdate()
    {
        _distantTraveled += Time.deltaTime * _speed;

        Vector3 nexpoint = _pathCreator.path.GetPointAtDistance( _distantTraveled, EndOfPathInstruction.Stop);
        nexpoint.y = transform.position.y;

        transform.LookAt(nexpoint);
        _rigitbody.MovePosition(nexpoint);
    }
}
