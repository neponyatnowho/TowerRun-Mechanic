using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixPointPosition;
    [SerializeField] private Transform _checkPointPlacePosition;

    [SerializeField] private float _deadForce = 300f;

    public Transform FixPointPosition => _fixPointPosition;
    public Transform CheckPointPlacePosition => _checkPointPlacePosition;

    public void Dead()
    {
        transform.parent = null;

        if(TryGetComponent(out Rigidbody rigitbody))
        {
            rigitbody.isKinematic = false;
            rigitbody.AddExplosionForce(_deadForce, transform.position, 5f);
        }

        GetComponent<HumanAnimator>().DiedAnimation();
    }

}
