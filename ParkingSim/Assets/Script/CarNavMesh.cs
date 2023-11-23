using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject movePositionTransform;
    [SerializeField] private float minDistance = 20f; // Set your desired minimum distance between cars here
    //[SerializeField] private float countdownTime = 30f;

    private NavMeshAgent navMeshAgent;
    private List<GameObject> availableParkingSpaces = new List<GameObject>();
    private float remainingDistance; // Declare remainingDistance here
    private bool? isParked = null;
    private float timer;
    private GameObject previousParkingSpace;

    void Start()
    {
        Collider carCollider = GetComponent<Collider>();
        if (carCollider != null)
        {
            carCollider.isTrigger = true; // Set the collider as a trigger to detect nearby cars
        }
        int carLayer = LayerMask.NameToLayer("Car");
        // Ignore collisions between objects in the "Car" layer
        Physics.IgnoreLayerCollision(carLayer, carLayer);
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        //parkingLots = GameObject.FindGameObjectsWithTag("ParkingSpace");
        availableParkingSpaces.AddRange(GameObject.FindGameObjectsWithTag("ParkingSpace"));

        GameObject nearestParkingSpace = FindNearestAvailableParkingSpace();
        GameObject despawnObject = GameObject.FindGameObjectWithTag("Despawn");
        if (nearestParkingSpace != null)
        {
            navMeshAgent.SetDestination(nearestParkingSpace.transform.position);
            //navMeshAgent.destination = mainRoadInstance.transform.position;
            navMeshAgent.speed = 300f;
            navMeshAgent.acceleration = 1000f;
            //availableParkingSpaces.Remove(nearestParkingSpace);
            nearestParkingSpace.tag = "Untagged";
            isParked = true;

            
        }
        else
        {
            navMeshAgent.SetDestination(despawnObject.transform.position);
            navMeshAgent.speed = 300f;
            navMeshAgent.acceleration = 1000f;
            isParked = false;
            Debug.Log("Moving to despawn area."); // Add a debug log here
            
        }
    }

    GameObject FindNearestAvailableParkingSpace()
    {
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
                //   Debug.Log("Hello haha");
                // Change values to your preferred range
                float randomDelay = Random.Range(100f, 200f); 
                Invoke("MoveToDespawn", randomDelay); // Invoke MoveToDespawn method after randomDelay seconds
                
                //availableParkingSpaces.Add(nearestParkingSpace);
                //nearestParkingSpace.tag = "ParkingSpace";
            }
        }

        if (navMeshAgent.hasPath && (isParked == null))
        {

        }
    }

        void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Vector3 direction = transform.position - other.transform.position;
            float distance = direction.magnitude;

            if (distance < minDistance)
            {
                // Calculate a new position to steer away from the nearby car
                Vector3 newPosition = transform.position + direction.normalized * minDistance;
                navMeshAgent.SetDestination(newPosition);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        //  StartCoroutine(DelayedTagChange2(other, 0.01f)); // Change 1.5f to your desired delay time
        

        if (other.CompareTag("Despawn"))
        {
            SimManager simManager = FindObjectOfType<SimManager>();
            if (simManager != null)
            {
                simManager.CarDespawned((bool)isParked);
            }
            GameObject.Destroy(gameObject);
        }
        // else if (other.CompareTag("Parking Space"))
        // {
        //     StartCoroutine(DelayedTagChange2(other, 15f)); // Change 1.5f to your desired delay time
        // }

        
        
    }

// private IEnumerator DelayedTagChange(Collider other, float delay)
// {
//     yield return new WaitForSeconds(delay);

//     // Check if the collider's tag is "Untagged" before changing it
//     if (other.CompareTag("Untagged"))
//     {
//         Debug.Log("Nice33");
//         other.tag = "ParkingSpace";
//     }
// }

// private IEnumerator DelayedTagChange2(Collider other, float delay)
// {
//     yield return new WaitForSeconds(delay);

//     // Check if the collider's tag is "Untagged" before changing it
//     if (other.CompareTag("ParkingSpace"))
//     {
//         Debug.Log("Nice66");
//         other.tag = "Untagged";
//     }
//     else
//     {
//         Debug.Log("Wrong");
//     }
// }

// private void OnTriggerExit(Collider other)
// {
//     // Call the coroutine to delay the tag change
//     StartCoroutine(DelayedTagChange(other, 20f)); // Change 1.5f to your desired delay time
// }



}