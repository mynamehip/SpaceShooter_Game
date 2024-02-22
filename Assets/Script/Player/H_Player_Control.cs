using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Control : MonoBehaviour
{
    Rigidbody2D obj;

    float speed;
    float rotationAngle;
    Vector2 moveDirection;
    Vector2 newPosition;
    Vector3 mousePosition;

    public GameObject bullet;
    public GameObject playerEffect;

    AudioSource audioSource;
    public AudioClip shootSound;

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        speed = 6f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Launch();
        }
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection.Normalize();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //obj.position = Vector2.MoveTowards(currentPosition, moveDirection, speed * Time.deltaTime);
        //obj.position = Vector2.Lerp(currentPosition, moveDirection, speed);
        Move();
        Rotate();
    }

    void Move()
    {
        newPosition = obj.position;
        newPosition.x = newPosition.x + moveDirection.x * speed * Time.deltaTime;
        newPosition.y = newPosition.y + moveDirection.y * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -8.5f, 8.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4.5f, 4.5f);
        obj.MovePosition(newPosition);
    }

    void Rotate()
    {
        Vector3 rotateDirection = mousePosition - transform.position;
        rotateDirection.Normalize();
        rotationAngle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        rotationAngle -= 90;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    void Launch()
    {
        audioSource.PlayOneShot(shootSound);
        Vector2 bulletSpawnDirection = mousePosition - transform.position;
        bulletSpawnDirection.Normalize();
        GameObject bulletObject = Instantiate(bullet, obj.position + bulletSpawnDirection * 0.5f, Quaternion.Euler(0f, 0f, rotationAngle));
        Projectile projectile = bulletObject.GetComponent<Projectile>();
        projectile.Launch(bulletSpawnDirection, 700);
    }

    public void Hitted()
    {
        Instantiate(playerEffect, obj.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public Vector2 GetPosition()
    {
        return obj.position;
    }
}
