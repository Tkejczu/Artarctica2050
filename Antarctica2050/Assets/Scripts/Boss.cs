using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float speed;
    public bool canShoot;
    public float health;
    public int bossScoreValue;
    PolygonCollider2D col;

    public Transform[] firstAttackPattern;
    public Transform[] secondAttackPattern;
    public Transform[] thirdAttackPattern;
    public Transform[] fourthAttackPattern;
    private Transform[] currentAttackPattern;

    public GameObject bossBullet;

    private AudioSource attackAudio;
    private AudioSource explosionAudio;
    private AudioSource hitAudio;

    private void Awake()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        attackAudio = audios[0];
        explosionAudio = audios[1];
        hitAudio = audios[2];
        col = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        if (canShoot)
        {
            InvokeRepeating("ShotBullets", 2f, 2f);
        }
    }
 

    void PickRandomPattern()
    {
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                currentAttackPattern = firstAttackPattern;
                break;
            case 1:
                currentAttackPattern = secondAttackPattern;
                break;
            case 2:
                currentAttackPattern = thirdAttackPattern;
                break;
            case 3:
                currentAttackPattern = fourthAttackPattern;
                break;
        }
    }

    void ShotBullets()
    {
        PickRandomPattern();
        foreach (var source in currentAttackPattern)
        {
            GameObject bullet = Instantiate(bossBullet, source.position, Quaternion.identity);
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
        Score.scoreValue += bossScoreValue;
        explosionAudio.Play();
        Destroy(gameObject);
        SceneManager.LoadScene("Win");
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                hitAudio.Play();
                health--;
            }

            if (health <= 0)
            {
                if (canShoot)
                {
                    canShoot = false;
                    CancelInvoke("ShotBullets");
                }
                Die();
            }
        }
    }
}
