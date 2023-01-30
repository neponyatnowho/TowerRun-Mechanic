using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;
    

    private List<Human> _humans;
    private HumanAnimator _mainHumanAnimator;

    public event UnityAction<int> PlayerCountInTowerChanged;
    public HumanAnimator MainHumanAnimator => _mainHumanAnimator;

    private void OnEnable()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
        _mainHumanAnimator = _humans[0].GetComponent<HumanAnimator>();
        StructurizeTower();
    }
    private void InsertHuman(Human collectedHuman)
    {
        Destroy(collectedHuman.GetComponent<Rigidbody>());
        _humans.Insert(0, collectedHuman);
        collectedHuman.transform.parent = transform;
        collectedHuman.transform.localRotation = Quaternion.identity;
    }

    private void StructurizeTower()
    {
        Vector3 resetXZ = new Vector3(0, _humans[0].gameObject.transform.localPosition.y, 0);
        _humans[0].gameObject.transform.localPosition = resetXZ;

        for (int i = 1; i <= _humans.Count - 1; i++)
        {
            _humans[i].gameObject.transform.position = _humans[i - 1].FixPointPosition.position;
        }
        _mainHumanAnimator = _humans[0].GetComponent<HumanAnimator>();

        DisplacaCheckers();
        PlayerCountInTowerChanged?.Invoke(_humans.Count);

    }

    private void DisplacaCheckers()
    {
        _distanceChecker.position = _humans[0].CheckPointPlacePosition.position;
        _checkCollider.center = _distanceChecker.localPosition;
    }

    private void ObstacleHit(Human hidedHuman)
    {
        if(_humans.Count > 1)
        {
            _humans.Remove(hidedHuman);
            StructurizeTower();
            hidedHuman.Dead();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            if (collisionTower != null)
            {
                List<Human> collectedHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

                if (collectedHumans != null)
                {

                    for (int i = collectedHumans.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHumans[i];
                        InsertHuman(insertHuman);
                    }
                    StructurizeTower();
                }
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Human hidedHuman = obstacle.ColisedHuman();
            if (hidedHuman != null)
                ObstacleHit(hidedHuman);
        }
    }
}

