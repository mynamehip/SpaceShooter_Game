using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D obj;
    float timer;
    // Start is called before the first frame update
    void Awake()
    {
        obj = GetComponent<Rigidbody2D>();
        timer = 2f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float bulletSpeed)
    {
        obj.AddForce(direction * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_Control enemy = collision.GetComponent<Enemy_Control>();
        if(enemy != null)
        {
            enemy.Hitted();
        }

        Boss_Controller boss = collision.GetComponent<Boss_Controller>();
        if (boss != null)
        {
            boss.Hitted(obj.position);
        }

        Drone drone = collision.GetComponent<Drone>();
        if (drone != null)
        {
            drone.Hitted();
        }

        Destroy(gameObject);
    }
}
