using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 10f;
    public float padding = 1f;
    public int health = 100;

    [Header("Projectile")]
    public GameObject laserPrefab;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = 0.15f;

    public AudioClip deathSound;
    [Range(0, 1)] public float deathSoundVolume = 0.75f;

    public AudioClip shootSound;
    [Range(0, 1)] public float shootSoundVolume = 0.25f;

    float xMin, xMax;
    float yMin, yMax;

    Coroutine firingCoroutine;

    private void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        xMax = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;
        yMin = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;
        yMax = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
		ProcessHit(damageDealer);
	}

	private void ProcessHit(DamageDealer damageDealer) {
		health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0) {
            health = 0;
            Die();
		}
	}

    private void Die() {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    public int GetHealth() {
        return health;
	}
}