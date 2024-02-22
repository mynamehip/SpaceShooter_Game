using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Control : MonoBehaviour
{
    Rigidbody2D obj;

    float timer;
    float speed;
    public float changeTime = 2.0f;
    int direction = 1;
    float moveTimer;

    Vector2 currentPosition;
    public GameObject bullet;
    public GameObject enemyEffect;

    AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentPosition = obj.position;
        timer = Random.Range(1f, 3f);
        speed = 2f;
        moveTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Launch();
        }

        moveTimer -= Time.deltaTime;
        if (moveTimer < 0)
        {
            direction = -direction;
            moveTimer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = obj.position;
        position.x = position.x + Time.deltaTime * speed * direction;
        obj.MovePosition(position);
    }

    void Launch()
    {
        audioSource.PlayOneShot(shootSound);
        Vector2 hi = currentPosition;
        hi.y -= 10f;
        hi -= currentPosition;
        hi.Normalize();
        GameObject bulletObject = Instantiate(bullet, obj.position - Vector2.up * 0.5f, Quaternion.identity);
        Bullet projectile = bulletObject.GetComponent<Bullet>();
        projectile.Launch(hi, 400);
        timer = Random.Range(2f, 5f);
    }

    public void Hitted()
    {
        Instantiate(enemyEffect, obj.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Control player = collision.GetComponent<Player_Control>();
        if (player != null)
        {
            player.Hitted();
        }
    }
}
