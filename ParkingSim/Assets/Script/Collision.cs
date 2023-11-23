using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSeparation : MonoBehaviour
{
    public float separationDistance = 10f;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, separationDistance);
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && col.CompareTag("Car"))
            {
                Vector3 dirToCar = col.transform.position - transform.position;
                float distance = dirToCar.magnitude;
                if (distance < separationDistance)
                {
                    float moveAwayDistance = separationDistance - distance;
                    Vector3 moveDir = -dirToCar.normalized * moveAwayDistance;
                    transform.position += moveDir;
                }
            }
        }
    }
}
