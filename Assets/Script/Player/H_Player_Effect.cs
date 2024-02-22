using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Efect : MonoBehaviour
{
    GameObject game_control;
    Game_Control gc;
    MainButton_Control button;

    AudioSource audioSource;
    Animator animator;
    public AudioClip destroyedSound;
    float animationDestroyTimer;
    float pauseGameTimer;
    
    void Start()
    {
        game_control = GameObject.Find("GameController");
        gc = game_control.GetComponent<Game_Control>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animationDestroyTimer = 0.3f;
        pauseGameTimer = 1f;
        animator.SetTrigger("explo");
        audioSource.PlayOneShot(destroyedSound);
    }

    void Update()
    {
        animationDestroyTimer -= Time.deltaTime;
        if (animationDestroyTimer <= 0)
        {
            animator.SetTrigger("endexplo");
        }
        pauseGameTimer -= Time.deltaTime;
        if(pauseGameTimer <= 0)
        {
            gc.isPausing = true;
            button = game_control.GetComponent<MainButton_Control>();
            button.OpenChooseScence();
        }
    }
}
