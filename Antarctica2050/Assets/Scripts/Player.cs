using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float initialSpeed;
    float currentSpeed;
    public static int currentHealth;
    public static int initialHealth = 3;
    public static int initialPowerUps = 2;
    public static int currentPowerUps;

    public float minY, maxY;
    public float minX, maxX;

    public float attackCooldownTime;
    private float currentAttackCooldownTime;
    private bool canAttack;
    private bool canMove = true;
    private bool isImmune = false;

    [SerializeField]
    private GameObject power;

    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Transform bulletSource;

    private AudioSource attackAudio;
    private AudioSource deathAudio;
    private AudioSource powerAudio;
    private AudioSource hitAudio;

    Animator anim;
    PolygonCollider2D col;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        AudioSource[] audios = GetComponents<AudioSource>();
        attackAudio = audios[0];
        deathAudio = audios[1];
        powerAudio = audios[2];
        hitAudio = audios[3];
        col = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        currentAttackCooldownTime = attackCooldownTime;
        currentHealth = initialHealth;
        currentSpeed = initialSpeed;
        currentPowerUps = initialPowerUps;
    }

    void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            MovePlayer();
            Attack();
            StartCoroutine(ImmunityPower());
        }
    }

    void MovePlayer()
    {
        if (canMove)
        {
            if ((Input.GetKey(KeyCode.LeftShift)))
            {
                currentSpeed = initialSpeed * 0.5f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentSpeed = initialSpeed;
            }
            //Vertical movement and vertical bounds
            if (Input.GetAxisRaw("Vertical") > 0f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y += currentSpeed * Time.deltaTime;

                if (currentPosition.y > maxY)
                {
                    currentPosition.y = maxY;
                }

                transform.position = currentPosition;
            }

            else if (Input.GetAxisRaw("Vertical") < 0f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y -= currentSpeed * Time.deltaTime;

                if (currentPosition.y < minY)
                {
                    currentPosition.y = minY;
                }

                transform.position = currentPosition;
            }
            //Horizontal movement and horizontal bounds
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.x += currentSpeed * Time.deltaTime;

                if (currentPosition.x > maxX)
                {
                    currentPosition.x = maxX;
                }

                transform.position = currentPosition;
            }

            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.x -= currentSpeed * Time.deltaTime;

                if (currentPosition.x < minX)
                {
                    currentPosition.x = minX;
                }

                transform.position = currentPosition;
            }
            
        }
    }

    void Attack()
    {
        attackCooldownTime += Time.deltaTime;
        if (attackCooldownTime > currentAttackCooldownTime)
        {
            canAttack = true;
        }

        if (Input.GetMouseButton(0) && canAttack)
        {
            canAttack = false;
            attackCooldownTime = 0f;
            Instantiate(playerBullet, bulletSource.position, Quaternion.identity);

            attackAudio.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Bullet") || target.CompareTag("Enemy"))
        {
            if(target.name.Contains("PlayerBullet"))
            {
                return;
            }
            if (currentHealth > 0 && isImmune == false)
            {
                hitAudio.Play();
                currentHealth--;
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        col.enabled = !col.enabled;
        canMove = false;
        canAttack = false;
        deathAudio.Play();
        anim.Play("PlayerExplosion");
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator ImmunityPower()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isImmune == false && currentPowerUps > 0)
        {
            currentPowerUps--;
            isImmune = true;
            power.SetActive(true);
            powerAudio.Play();
            yield return new WaitForSeconds(5f);

            if (isImmune)
            {
                powerAudio.Stop();
                isImmune = false;
                power.SetActive(false);
            }
        }
        
    }
}
