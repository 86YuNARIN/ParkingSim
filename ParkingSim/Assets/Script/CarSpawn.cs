using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    [SerializeField] SimManager _simManager;
    [SerializeField] GameObject[] _spawnPoints;
    [SerializeField] GameObject[] _cars;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNextCar());
        StartCoroutine(SpawnRateChanger());
    }

    IEnumerator SpawnNextCar()
    {
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        int nextCarModel = Random.Range(0, _cars.Length);
        Instantiate(_cars[nextCarModel], _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);

        float nextSpawnDelay = GetNextSpawnDelay();
        yield return new WaitForSeconds(nextSpawnDelay);

        StartCoroutine(SpawnNextCar());
    }
    IEnumerator SpawnRateChanger()
    {
        float nextSpawnDelay = GetNextSpawnDelay();
        yield return new WaitForSeconds(nextSpawnDelay);

        // Implement logic to change spawn rate if needed

        StartCoroutine(SpawnRateChanger());
    }

    float GetNextSpawnDelay()
    {
        // Use the Poisson distribution with the current spawn rate
        float lambda = GetSpawnRate();
        float randomValue = Random.value;
        float nextSpawnDelay = -Mathf.Log(randomValue) / lambda;

        return nextSpawnDelay;
    }

    #region GetSpawnRate
    // Define different spawn rates for peak, normal, and less busy hours
    private float peakHourSpawnRate = 0.1f;
    private float normalHourSpawnRate = 0.05f;
    private float lessBusyHourSpawnRate = 0.02f;

    // Example method to get the spawn rate based on the current hour or simulation state
    public float GetSpawnRate()
    {
        int currentHour = GetCurrentHour(); // Implement a method to get the current hour or simulation state

        // Adjust spawn rate based on different hours or states
        if (IsPeakHour(currentHour))
        {
            return peakHourSpawnRate;
        }
        else if (IsNormalHour(currentHour))
        {
            return normalHourSpawnRate;
        }
        else
        {
            return lessBusyHourSpawnRate;
        }
    }

    // Example method to get the current hour (replace this with your actual logic)
    private int GetCurrentHour()
    {
        // Return the current hour from SimManager
        return _simManager.CurrentHour;
    }

    // Example method to check if it's peak hour (replace this with your actual logic)
    private bool IsPeakHour(int hour)
    {
        // Implement the logic to check if it's peak hour
        // For simplicity, consider peak hours from 8 AM to 10 AM in this example.
        return hour >= 8 && hour < 10;
    }

    // Example method to check if it's a normal hour (replace this with your actual logic)
    private bool IsNormalHour(int hour)
    {
        // Implement the logic to check if it's a normal hour
        // For simplicity, consider normal hours from 10 AM to 6 PM in this example.
        return hour >= 10 && hour < 18;
    }
    #endregion
}
