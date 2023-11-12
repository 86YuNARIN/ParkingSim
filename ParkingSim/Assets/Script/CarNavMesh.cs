using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject movePositionTransform;

    private NavMeshAgent navMeshAgent;

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
    }

    // Update is called once per frame
    void Update()
    { }
}
