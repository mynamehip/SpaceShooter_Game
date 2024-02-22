using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Controller : MonoBehaviour
{
    Rigidbody2D obj;

    float attackTimer;
    float speed;
    int currentHeart;
    int maxHeart = 50;

    float rotationAngle;
    public GameObject bullet;
    public GameObject drone;
    public GameObject bossEffect;
    GameObject player;
    Player_Control player_Control;

    AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip launchDroneSound;

    public ParticleSystem hittedEffect;

    Vector3 playerPosition;
    Vector3 rotateDirection;

    bool isTackle;
    Vector3 targetPosition;

    bool isShooting;
    float shootingTimer;
    int shootingCount;

    bool isLaunchDrone;
    int launchTurn;
    float launchTimer;

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        if (player != null)
        {
            player_Control = player.GetComponent<Player_Control>();
        }
        attackTimer = 2f;
        speed = 8f;
        currentHeart = maxHeart;
        isTackle = false;
        isShooting = false;
        isLaunchDrone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_Control != null)
        {
            playerPosition = player_Control.GetPosition();
        }
        Rotate();
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            NextMove();
            attackTimer = 3f;
        }
    }

    private void FixedUpdate()
    {
        if (isTackle == true)
        {
            Tackle();
        }
        if (isShooting == true)
        {
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                Shooting();
                shootingTimer = 0.3f;
            }
        }
        if (isLaunchDrone == true)
        {
            launchTimer -= Time.deltaTime;
            if (launchTimer <= 0)
            {
                LaunchDrone();
                launchTimer = 0.5f;
            }
        }
    }

    void Rotate()
    {
        rotateDirection = playerPosition - transform.position;
        rotateDirection.Normalize();
        rotationAngle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        rotationAngle -= 90;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        Quaternion newQuaternion = Quaternion.Euler(0f, 0f, rotationAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newQuaternion, 60f * Time.deltaTime);
    }

    void Tackle()
    {
        if (targetPosition != transform.position)
        {
            obj.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            isTackle = false;
        }
    }

    void Shooting()
    {
        if (shootingCount > 0)
        {
            audioSource.PlayOneShot(shootSound);
            Vector2 shotDirection = rotateDirection;
            GameObject bulletObject = Instantiate(bullet, obj.position + shotDirection * 0.5f, Quaternion.Euler(0f, 0f, rotationAngle));
            Bullet projectile = bulletObject.GetComponent<Bullet>();
            projectile.Launch(shotDirection, 300);
            shootingCount--;
        }
        else
        {
            isShooting = false;
        }
    }

    void LaunchDrone()
    {
        if(launchTurn > 0)
        {
            Vector3[] shotDirection = new Vector3[3];
            float[] x = new float[3];
            GameObject[] bulletObject = new GameObject[3];
            Drone[] projectile = new Drone[3];
            for (int i = 0; i < 3; i++)
            {
                shotDirection[i] = -rotateDirection;
                if (i == 1)
                {
                    Quaternion rotation1 = Quaternion.Euler(0.0f, 0.0f, 30f);
                    shotDirection[i] = rotation1 * shotDirection[i];
                }
                if (i == 2)
                {
                    Quaternion rotation1 = Quaternion.Euler(0.0f, 0.0f, -30f);
                    shotDirection[i] = rotation1 * shotDirection[i];
                }
                shotDirection[i].Normalize();
                bulletObject[i] = Instantiate(drone, transform.position - shotDirection[i] * (-0.5f), Quaternion.Euler(0f, 0f, rotationAngle));
                projectile[i] = bulletObject[i].GetComponent<Drone>();
                projectile[i].Launch(shotDirection[i]);
                audioSource.PlayOneShot(launchDroneSound);
            }
            launchTurn--;
        }
        else
        {
            isLaunchDrone = false;
        }
    }

    void NextMove()
    {
        int moveType = Random.Range(0, 3);
        switch (moveType)
        {
            case 0:
                targetPosition = playerPosition;
                isTackle = true;
                break;
            case 1:
                isShooting = true;
                shootingTimer = 0.3f;
                shootingCount = 7;
                break;
            case 2:
                if(obj.simulated == true)
                {
                    isLaunchDrone = true;
                    launchTurn = 3;
                    launchTimer = 0.75f;
                }
                break;
        }
    }

    public void Hitted(Vector2 position)
    {
        Instantiate(hittedEffect, position, Quaternion.identity);
        currentHeart--;
        HeartBar.instance.SetValue(currentHeart / (float)maxHeart);
        if(currentHeart <= 0)
        {
            Instantiate(bossEffect, obj.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Control p = collision.GetComponent<Player_Control>();
        if(p != null){
            player_Control.Hitted();
        }
    }
}
