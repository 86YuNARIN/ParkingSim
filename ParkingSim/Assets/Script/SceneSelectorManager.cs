using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectorManager : MonoBehaviour
{
    public void toScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void toScene2()
    {
        SceneManager.LoadScene(2);
    }
}
