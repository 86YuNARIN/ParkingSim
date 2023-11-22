using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    private int _currentHour = 0;
    private int _successfulPark = 0; 
    private int _unSuccessfulPark = 0;

    public int CurrentHour
    {
        get { return _currentHour/30; } //assume 30frame per minit
        set { _currentHour = value; }
    }

    public int SuccessfulPark
    {
        get { return _successfulPark; }
        set { _successfulPark = value; }
    }
    public int UnSuccessfulPark
    {
        get { return _unSuccessfulPark; }
        set { _unSuccessfulPark = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // You can initialize CurrentHour to a specific value if needed
        // _currentHour = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the current hour each frame
        _currentHour++;
        //Debug.Log("Simulation hours: " + CurrentHour);

        // Check if the simulation should stop when CurrentHour reaches 2400
        if (CurrentHour >= 1440)
        {
            // Implement any logic you need for stopping the simulation
            // For example, you can pause the game, display a summary, or exit the application
            Debug.Log("Simulation completed at 2400 hours. Stopping simulation.");
            Time.timeScale = 0f; // Pause the game
            // Alternatively, you can implement other actions to conclude the simulation.
        }
    }
}
