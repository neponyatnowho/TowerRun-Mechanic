using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;
    [SerializeField] private float _smoth = 0.125f;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private float _zPositionMultiplate;

    private void OnEnable()
    {
        _playerTower.PlayerCountInTowerChanged += ChangeZPosition;
    }

    private void OnDisable()
    {
        _playerTower.PlayerCountInTowerChanged -= ChangeZPosition;
    }

    private void LateUpdate()
    {
        ChangePosition();
    }

    public void ChangePosition()
    {
        Vector3 desiredPosition = _playerTower.transform.TransformPoint(offsetPosition);
        desiredPosition.y /= 2f; 
        Vector3 smothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoth);
        transform.position = smothedPosition;
        Vector3 desiredRotation = _playerTower.transform.position;
        desiredRotation.y /= 2f;
        transform.LookAt(desiredRotation);
    }

    private void ChangeZPosition(int CountPeople)
    {
        offsetPosition.z = _zPositionMultiplate * CountPeople;
    }
}
