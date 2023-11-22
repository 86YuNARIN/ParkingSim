using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject movePositionTransform;
    //[SerializeField] private float countdownTime = 30f;

    private NavMeshAgent navMeshAgent;
    private List<GameObject> availableParkingSpaces = new List<GameObject>();
    private float remainingDistance; // Declare remainingDistance here
    private bool? isParked = null;
    private float timer;
    private GameObject previousParkingSpace;


    void Start()
    {
        int carLayer = LayerMask.NameToLayer("Car");
        // Ignore collisions between objects in the "Car" layer
        Physics.IgnoreLayerCollision(carLayer, carLayer);
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        //parkingLots = GameObject.FindGameObjectsWithTag("ParkingSpace");
        availableParkingSpaces.AddRange(GameObject.FindGameObjectsWithTag("ParkingSpace"));

        GameObject nearestParkingSpace = FindNearestAvailableParkingSpace();
        if (nearestParkingSpace != null)
        {
            // Find the DespawnRoad within the MainRoad instance
            //Transform despawnRoadTransform = mainRoadInstance.transform.Find("Despawn Road");
            //if (mainRoadInstance == null)
            //{
            // Do something with despawnRoadTransform
            // For example, set the destination of the nav mesh agent
            navMeshAgent.SetDestination(nearestParkingSpace.transform.position);
            //navMeshAgent.destination = mainRoadInstance.transform.position;
            navMeshAgent.speed = 300f;
            navMeshAgent.acceleration = 1000f;
            availableParkingSpaces.Remove(nearestParkingSpace);
            nearestParkingSpace.tag = "Untagged";
            isParked = true;
        }
        else
        {
            Debug.LogError("MainRoad instance not found in the scene.");
        }
    }


    GameObject FindNearestAvailableParkingSpace()
    {
        if (availableParkingSpaces.Count == 0)
        {
            // No available parking spaces, head towards the "Despawn" tagged object
            GameObject despawnObject = GameObject.FindGameObjectWithTag("Despawn");
            isParked = false;
            if (despawnObject != null)
            {
                return despawnObject;
            }
            else
            {
                Debug.LogError("Despawn object not found in the scene.");
                return null;
            }
        }

        GameObject nearestSpace = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject space in availableParkingSpaces)
        {
            float distance = Vector3.Distance(currentPos, space.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSpace = space;
            }
        }

        return nearestSpace;
    }


    public void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void MoveToDespawn()
    {
        GameObject despawnObject = GameObject.FindGameObjectWithTag("Despawn");

        if (despawnObject != null)
        {
            navMeshAgent.SetDestination(despawnObject.transform.position);
            // Assuming you want to reset some properties when moving to the despawn point
            //ResetCarProperties(); // Create this function to reset any necessary properties
            Debug.Log("Moving to despawn area."); // Add a debug log here
        }
        else
        {
            Debug.LogError("Despawn object not found in the scene.");
        }
    }

    void Update()
    {
        availableParkingSpaces.AddRange(GameObject.FindGameObjectsWithTag("ParkingSpace"));
        GameObject nearestParkingSpace = FindNearestAvailableParkingSpace();
        if (navMeshAgent.hasPath && (isParked != null))
        {
            // Calculate remaining distance
            remainingDistance = navMeshAgent.remainingDistance;

            // Check if remaining distance is almost zero and destroy the object
            if (remainingDistance < 40.0f)
            {
                float randomDelay = Random.Range(15.0f, 30.0f); // Change values to your preferred range
                Invoke("MoveToDespawn", randomDelay); // Invoke MoveToDespawn method after randomDelay seconds
                //availableParkingSpaces.Add(nearestParkingSpace);
                //nearestParkingSpace.tag = "ParkingSpace";
            }
           
        }

        if (navMeshAgent.hasPath && (isParked == null))
        {

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Despawn"))
        {
            SimManager simManager = FindObjectOfType<SimManager>();
            if (simManager != null)
            {
                simManager.CarDespawned((bool)isParked);
            }
            GameObject.Destroy(gameObject);
        }

        if (other.CompareTag("ParkingSpace"))
        {
            other.tag = "Parked";
        }

        
        
    }
}