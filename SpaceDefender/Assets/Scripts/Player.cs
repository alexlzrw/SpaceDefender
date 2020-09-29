using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    public float moveSpeed = 10f;
    public float padding = 1f;
    public int health = 100;
    public GameObject myPoint;
    private bool needToGo = false;

    [Header("Projectile")]
    public Transform firePoints;
    public GameObject laserPrefab;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = .15f;
    public float delayFiring = 1f;

    [Header("Other")]
    [Range(0f, 1f)]
    public float slow = 0.5f;
    public GameObject upgradeCanvas;

    private Vector3 offset;

    private void Start() {
        needToGo = true;
        Time.timeScale = slow;
    }

    private void Awake() {
        Fire();
    }


    private void Update() {
        MoveToPoint();
    }

    private void Fire() {
        StartCoroutine(FireContinuously());
    }

    private IEnumerator FireContinuously() {
        yield return new WaitForSeconds(delayFiring);
        while (true) {
            foreach (Transform firePoint in firePoints) {
                if (firePoint.gameObject.activeSelf) {
                    if (firePoint.childCount > 0) {
                        foreach (Transform childFirePoint in firePoint) {
                            GameObject laser = Instantiate(laserPrefab, childFirePoint.position, Quaternion.identity);
                            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
                        }
                        FindObjectOfType<AudioManager>().Play("PlayerShoot");
                    } else {
                        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
                        FindObjectOfType<AudioManager>().Play("PlayerShoot");
                    }
                }
            }

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Time.timeScale = 1f;

            HideUpgradeCanvas();
        }
    }

    private void OnMouseUp() {
        if (Input.GetMouseButtonUp(0)) {
            Time.timeScale = slow;

            ShowUpgradeCanvas();
        }
    }


    private void OnMouseDrag() {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }


    private void MoveToPoint() {
        if (needToGo) {
            transform.position = Vector2.MoveTowards(transform.position, myPoint.transform.position, 5f * Time.deltaTime);

        }

        if (Vector2.Distance(transform.position, myPoint.transform.position) < 0.01) {
            needToGo = false;
        }
    }

    private void ShowUpgradeCanvas() {
        if (!upgradeCanvas.activeSelf) {
            upgradeCanvas.SetActive(true);
		}
	}

    private void HideUpgradeCanvas() {
        if (upgradeCanvas.activeSelf) {
            upgradeCanvas.SetActive(false);
        }
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
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Destroy(gameObject);
        
    }

    public int GetHealth() {
        return health;
	}

    public void SetUpgrade(UpgradeItem upgradeItem) {
        laserPrefab = upgradeItem.LaserPrefab;
        // Must be in separate method with disabling old OneShot point
        foreach (Transform firePoint in firePoints) {
            if (firePoint.name == upgradeItem.FirePointsName) {
                firePoint.gameObject.SetActive(true);
            }
            // SHOULD BE A BETTER WAY TO DISABlE OneShot POINT
            if (firePoint.name == "OneShot") {
                firePoint.gameObject.SetActive(false);
            }
        }
        projectileSpeed = upgradeItem.ProjectileSpeed;
        projectileFiringPeriod = upgradeItem.ProjectileFiringPeriod;
    }
}