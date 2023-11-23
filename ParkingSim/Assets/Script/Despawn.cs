using UnityEngine;
using UnityEngine.AI;

public class DestroyOnTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        /*// Check if the entering object has a specific tag (you can customize this)
        if (collision.collider.CompareTag("Car"))
        {
            // Deactivate the entering object (make it disappear)
            collision.gameObject.SetActive(false);
        }*/
        Debug.Log("Hello");
        GameObject.Destroy(gameObject);
    }

    /*private void OnTriggerEnter(Collision collision)
    {
        // Check if the entering object has a specific tag (you can customize this)
        if (collision.collider.CompareTag("Car"))
        {
            // Deactivate the entering object (make it disappear)
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a specific tag (you can customize this)
        if (collision.collider.CompareTag("Player"))
        {
            // Deactivate the entering object (make it disappear)
            collision.gameObject.SetActive(false);
        }
    }*/
}
