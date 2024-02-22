using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Control : MonoBehaviour
{
    public bool isPausing;
    
    void Start()
    {
        isPausing = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPausing)
            {
                isPausing = true;
            }
            else
            {
                isPausing = false;
            }
        }
        if(isPausing == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
