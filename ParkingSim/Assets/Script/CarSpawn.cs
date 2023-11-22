using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    private SimManager _simManager;
    [SerializeField] GameObject[] _spawnPoints;
    [SerializeField] GameObject[] _cars;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign the SimManager reference
        _simManager = FindObjectOfType<SimManager>();

        StartCoroutine(SpawnNextCar());
        StartCoroutine(SpawnRateChanger());
    }

    IEnumerator SpawnNextCar()
    {
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        int nextCarModel = Random.Range(0, _cars.Length);

        Vector3 spawnPosition = _spawnPoints[nextSpawnLocation].transform.position;
        

        GameObject newCar = Instantiate(_cars[nextCarModel], _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        if (newCar.CompareTag("Car"))
    {
        Debug.Log("Hey");
        Vector3 newPos = newCar.transform.position;
        newPos.y = 23f; // Set the Y position to 23 for objects with the "Car" tag
        newCar.transform.position = newPos;
    }
        newCar.layer = LayerMask.NameToLayer("Car");

        Rigidbody rigidbody = newCar.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
        rigidbody = newCar.AddComponent<Rigidbody>();
        rigidbody.useGravity = false; // Disable gravity for the rigidbody
        rigidbody.angularDrag = 0f; // Set angular drag to 0

        rigidbody.drag = 5f; // Adjust linear drag value (experiment with values)
        rigidbody.angularDrag = 5f; // Adjust angular drag value (experiment with values)
        // Freeze rotation along certain axes if needed
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        rigidbody.centerOfMass = Vector3.zero; // Set center of mass manually (optional)
        rigidbody.inertiaTensor = Vector3.zero; // Set inertia tensor manually (optional)
        rigidbody.inertiaTensorRotation = Quaternion.identity; // Set inertia tensor rotation manually (optio
        }
        // Add a BoxCollider component to the instantiated car
        BoxCollider collider = newCar.GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = newCar.AddComponent<BoxCollider>();
            collider.size = new Vector3(1.72f, 0.54f, 2.87f);
            collider.isTrigger = false; // Set the collider as a trigger for collision detection
        }

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
    private float peakHourSpawnRate = 2f;
    private float normalHourSpawnRate = 1.5f;
    private float lessBusyHourSpawnRate = 1f;

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
