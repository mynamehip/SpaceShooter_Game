using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Drone : MonoBehaviour
{
    Rigidbody2D obj;
    float timer;
    float flyTimer;
    float speed;
    GameObject player;
    Player_Control player_Control;
    Vector2 direction;

    public GameObject droneEffect;

    // Start is called before the first frame update
    void Awake()
    {
        obj = GetComponent<Rigidbody2D>();
        timer = 7f;
        flyTimer = 1f;
        speed = 5f;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            player_Control = player.GetComponent<Player_Control>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        flyTimer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
        if (flyTimer > 0f)
        {
            direction = direction * 1.5f;
            obj.position = Vector2.MoveTowards(obj.position, direction, 5f * Time.deltaTime);
        }
        else
        {
            Tackle();
        }
    }

    public void Launch(Vector2 direction)
    {
        this.direction = direction;
    }

    void Tackle()
    {
        if(player_Control != null)
        {
            Vector2 playerPosition = player_Control.GetPosition();
            if (playerPosition != obj.position)
            {
                obj.position = Vector2.MoveTowards(obj.position, playerPosition, speed * Time.deltaTime);
            }
        }
    }

    public void Hitted()
    {
        Instantiate(droneEffect, obj.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Control obj = collision.GetComponent<Player_Control>();
        if (obj != null)
        {
            obj.Hitted();
            Destroy(gameObject);
        }

    }
}
