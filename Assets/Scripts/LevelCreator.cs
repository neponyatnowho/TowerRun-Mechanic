using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;
    [SerializeField] private List<Obstacle> _obstacles;

    private void Start()
    {
        GenerateLevel();    
    }

    private void GenerateLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distantBetweenTower = roadLength / _humanTowerCount;

        float towerDistanceTrawed = 0f;
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            towerDistanceTrawed += distantBetweenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(towerDistanceTrawed, EndOfPathInstruction.Stop);
            Vector3 nexpoint = _pathCreator.path.GetPointAtDistance(towerDistanceTrawed + .5f, EndOfPathInstruction.Stop);

            Tower tower = Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
            tower.transform.LookAt(nexpoint);


            if(i > 1 && i < _humanTowerCount - 1)
            {
                int random = Random.Range(0, _obstacles.Count);

                spawnPoint = _pathCreator.path.GetPointAtDistance(towerDistanceTrawed + (distantBetweenTower / 2), EndOfPathInstruction.Stop);
                spawnPoint.y = _obstacles[random].transform.localScale.y / 2f;

                Obstacle obstacle =  Instantiate(_obstacles[random], spawnPoint, Quaternion.identity);
                Vector3 obsticlePoint = _pathCreator.path.GetPointAtDistance(towerDistanceTrawed + (distantBetweenTower / 2) + 0.1f, EndOfPathInstruction.Stop);
                obsticlePoint.y = obstacle.transform.position.y;
                obstacle.transform.LookAt(obsticlePoint);
            }
        }
    }
}
