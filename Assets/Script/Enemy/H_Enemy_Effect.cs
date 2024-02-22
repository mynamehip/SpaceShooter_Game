using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Effect : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    public AudioClip destroyedSound;
    float animationDestroyTimer;
    float lifeTime = 3f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animationDestroyTimer = 0.3f;
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
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
