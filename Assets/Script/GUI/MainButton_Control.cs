using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton_Control : MonoBehaviour
{
    public GameObject scencePopup;
    Game_Control control;
    public bool isPopupOpening;
    int currentScence;

    private void Start()
    {
        currentScence = SceneManager.GetActiveScene().buildIndex;
        control = GetComponent<Game_Control>();
        isPopupOpening = false;
    }

    public void OpenChooseScence()
    {
        scencePopup.SetActive(true);
        isPopupOpening = true;
    }

    public void CloseChooseScence()
    {
        scencePopup.SetActive(false);
        isPopupOpening = false;
        control.isPausing = false;
    }       

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelBoss()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(currentScence);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
