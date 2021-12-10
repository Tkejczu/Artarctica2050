using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float deactivateTimer;
    public float boundaryY = -4.5f;

    [HideInInspector]
    public bool isEnemyBullet = false;
    void Start()
    {
        if (isEnemyBullet)
        {
            speed *= -1f;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += speed * Time.deltaTime;
        transform.position = currentPosition;
        if (currentPosition.y < boundaryY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player") && isEnemyBullet == false)
        {
            return;
        }
        if(target.CompareTag("Bullet") || target.CompareTag("Enemy") || target.CompareTag("Player") || target.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
