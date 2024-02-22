using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Player_Control obj = collision.GetComponent<Player_Control>();
        if (obj != null) {
            obj.Hitted();
            Destroy(gameObject);
        }
        
    }
}
