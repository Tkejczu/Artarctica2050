using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool canShoot;
    private bool canMove = true;
    public float health;
    public int enemyScoreValue;
    Animator anim;
    PolygonCollider2D col;
    public float boundaryY = -4.5f;
    public float fireRate = 2.5f;

    public Transform[] bulletSource;
    public GameObject enemyBullet;

    private AudioSource attackAudio;
    private AudioSource explosionAudio;
    private AudioSource hitAudio;

    private void Awake()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        attackAudio = audios[0];
        explosionAudio = audios[1];
        hitAudio = audios[2];
        anim = GetComponent<Animator>();
        col = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        if (canShoot)
        {
            InvokeRepeating("ShotBullets", fireRate, fireRate);
        }
    }

    
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(canMove)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y -= speed * Time.deltaTime;
            transform.position = currentPosition;

            if (currentPosition.y < boundaryY)
            {
                Destroy(gameObject);
            }
        }
    }

    void ShotBullets()
    {
        foreach (var source in bulletSource)
        {
            GameObject bullet = Instantiate(enemyBullet, source.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().isEnemyBullet = true;

            if (canShoot)
            {
                attackAudio.Play();
            }
        }
    }

    void Die()
    {
        col.enabled = !col.enabled;
        Score.scoreValue += enemyScoreValue;
        explosionAudio.Play();
        anim.Play("Destroy");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                hitAudio.Play();
                health--;
            }

            if (health <= 0)
            {
                canMove = false;

                if (canShoot)
                {
                    canShoot = false;
                    CancelInvoke("ShotBullets");
                }   
                Die();
            }
        }
        if(target.CompareTag("Player"))
        {
            Die();
        }    
    }
}
