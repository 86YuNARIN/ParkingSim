using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public GameObject [] carPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, carPrefab.Length);
            Vector3 randomSpawnPosition = new Vector3(34, 0, -25);//34.6, 0, -25.26
            Instantiate(carPrefab[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}