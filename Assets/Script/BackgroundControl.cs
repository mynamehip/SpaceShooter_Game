using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public GameObject background;
    float repeatSize;
    bool isCreated = false;
    
    void Start()
    {
        repeatSize = GetComponent<BoxCollider2D>().size.y;
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = currentPosition;
        nextPosition.y -= 0.1f;
        transform.position = Vector2.Lerp(currentPosition, nextPosition, 0.1f);
        if(currentPosition.y <= 0 && isCreated == false)
        {
            isCreated = true;
            Vector2 spawnPosition = currentPosition;
            spawnPosition.y = spawnPosition.y + repeatSize / 2 - 0.01f;
            Instantiate(background, spawnPosition, Quaternion.identity);
        }
        if(-currentPosition.y > repeatSize/2)
        {
            Destroy(gameObject);
        }
    }
}
