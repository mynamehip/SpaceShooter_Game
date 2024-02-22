using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Effect : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    public AudioClip destroyedSound;
    float lifeTime = 3f;
    float playSoundAgain = 0.3f;
    int count = 1;
    float animationDestroyTimer = 0.3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(destroyedSound);
        animator = GetComponent<Animator>();
        animator.SetTrigger("explo");
        count++;
    }

    void Update()
    {
        animationDestroyTimer -= Time.deltaTime;
        if (animationDestroyTimer <= 0)
        {
            animator.SetTrigger("endexplo");
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        playSoundAgain -= Time.deltaTime;
        if(playSoundAgain <= 0)
        {
            audioSource.PlayOneShot(destroyedSound);
            playSoundAgain = 10f;
        }
    }
}
