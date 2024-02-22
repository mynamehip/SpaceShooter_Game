using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton_Control : MonoBehaviour
{
    public GameObject scencePopup;

    private void Awake()
    {
        //CloseChooseScence();
    }

    public void CloseChooseScence()
    {
        scencePopup.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelBoss()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
