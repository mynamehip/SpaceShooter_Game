using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Control : MonoBehaviour
{
    public bool isPausing;
    MainButton_Control buttonControl;
    float endGameTimer = 1f;
    GameObject player;
    Player_Control playerControl;

    void Start()
    {
        isPausing = false;
        buttonControl = GetComponent<MainButton_Control>();
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<Player_Control>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPausing)
            {
                isPausing = true;
            }
            else
            {
                isPausing = false;
            }
        }

        if (CheckEnemy("Enemy") < 1)
        {
            endGameTimer -= Time.deltaTime;
            if (endGameTimer < 0)
            {
                isPausing = true;
            }
        }

        if (isPausing == true)
        {
            Time.timeScale = 0;
            if(playerControl != null)
            {
                playerControl.enabled = false;
            }
            buttonControl.OpenChooseScence();
        }
        else
        {
            Time.timeScale = 1;
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }
            buttonControl.CloseChooseScence();
        }

        if (buttonControl.isPopupOpening == false)
        {
            isPausing = false;
        }
        else
        {
            isPausing = true;
        }
    }

    private int CheckEnemy(string layerName)
    {
        int count = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(layerName))
            {
                count++;
            }
        }
        return count;
    }
}
