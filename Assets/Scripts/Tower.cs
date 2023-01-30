using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Vector2Int _humanInTowerRange;
    [SerializeField] Human[] _humansTemplate;
    [SerializeField] float _breakForce;
    [SerializeField] Transform _ExplosionPointTransform;

    private List<Human> _humansInTower;

    private void Start()
    {
        _humansInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);

        HumanSpawn(humanInTowerCount);
    }
    private void HumanSpawn(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humansTemplate[Random.Range(0, _humansTemplate.Length)];

            _humansInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));

            _humansInTower[i].transform.localPosition = new Vector3(0, _humansInTower[i].transform.localPosition.y, 0);
            _humansInTower[i].transform.rotation = transform.rotation;

            spawnPoint = _humansInTower[i].FixPointPosition.position;
        }
    }

    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humansInTower.Count; i++)
        {
            float distanceBetweenPoints = CheckDistanceY(distanceChecker, _humansInTower[i].FixPointPosition);

            if (distanceBetweenPoints < fixationMaxDistance)
            {
                List<Human> collectedHumans = _humansInTower.GetRange(0, i + 1);
                _humansInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }
        Brake();
        return null;
    }

    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }

    public void Brake()
    {
        foreach (var human in _humansInTower)
        {
            human.Dead();
        }
    }

}
