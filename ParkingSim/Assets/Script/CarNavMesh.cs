using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject movePositionTransform;

    private NavMeshAgent navMeshAgent;

    public float remainingDistance;

    void Start()
    {
        // Assuming you have already dragged the MainRoad instance into the Unity Editor
        GameObject mainRoadInstance = GameObject.Find("Despawn"); // Change "MainRoad" to the actual name of your instance

        if (mainRoadInstance != null)
        {
            // Find the DespawnRoad within the MainRoad instance
            //Transform despawnRoadTransform = mainRoadInstance.transform.Find("Despawn Road");

            //if (mainRoadInstance == null)
            //{
            // Do something with despawnRoadTransform
            // For example, set the destination of the nav mesh agent
            navMeshAgent.destination = mainRoadInstance.transform.position;

            navMeshAgent.speed = 300f;
            navMeshAgent.acceleration = 1000f;
            //}
            //else
            //{
            //    Debug.LogError("DespawnRoad not found in MainRoad instance."); 
            //}
        }
        else
        {
            Debug.LogError("MainRoad instance not found in the scene.");
        }
    }

    public void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        // If the NavMeshAgent component is not attached, add it
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.hasPath)
        {
            // Calculate remaining distance
            remainingDistance = navMeshAgent.remainingDistance;

            // Display remaining distance (you can replace this with your preferred way of displaying the information)
            Debug.Log("Remaining Distance: " + remainingDistance);

            // Check if remaining distance is almost zero and destroy the object
            if (remainingDistance < 40.0f)
            {
                Destroy(gameObject);
            }
        }
    }




}