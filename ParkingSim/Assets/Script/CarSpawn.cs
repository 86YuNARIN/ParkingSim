using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    [SerializeField] SimManager _simManager;
    [SerializeField] GameObject[] _spawnPoints;
    [SerializeField] GameObject _cars;

    // TODO change into dynamic spawn rate {get set}
    float _spawnTimer = 2f;
    float _spawnRateIncrease = 2f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNextCar());
        StartCoroutine(SpawnRateChanger());
    }

    IEnumerator SpawnNextCar()
    {
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        Instantiate(_cars , _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_spawnTimer);
               
        StartCoroutine(SpawnNextCar());
                  
    }

    IEnumerator SpawnRateChanger()
    {
        yield return new WaitForSeconds(_spawnRateIncrease);

        if (_spawnTimer >= 0.5f)
        {
            _spawnTimer -= .2f;
        }

        StartCoroutine(SpawnRateChanger());
    }
}
