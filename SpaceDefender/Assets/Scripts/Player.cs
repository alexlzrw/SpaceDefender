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

    [Header("Firing Systems")]
    public FiringSystem[] firingSystems;

    [Header("Other")]
    [Range(0f, 1f)]
    public float slow = 0.5f;
    public GameObject upgradeCanvas;
    public float delayFiring = 1f;

    private Vector3 offset;

    private void Start() {
        needToGo = true;
    }

    private void Update() {
        MoveToPoint();
        FireContinuously();
    }

    private void FireContinuously() {
        foreach (FiringSystem firingSystem in firingSystems) {
            if (firingSystem.isActive) {
                if (firingSystem.projectileFiringPeriodCounter >= firingSystem.projectileFiringPeriod) {
                    foreach (Transform firePoint in firingSystem.firePoints) {
                        if (!firingSystem.isDiagonal) {
                            GameObject laser = Instantiate(firingSystem.projectilePrefab, firePoint.position, Quaternion.identity);
                            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, firingSystem.projectileSpeed);
                        } else {
                            if (firePoint.name == "Left") {
                                GameObject laser = Instantiate(firingSystem.projectilePrefab, firePoint.position, Quaternion.AngleAxis(-15, Vector3.back));
                                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-firingSystem.projectileSpeed / 3f, firingSystem.projectileSpeed);
                            } else if (firePoint.name == "Right") {
                                GameObject laser = Instantiate(firingSystem.projectilePrefab, firePoint.position, Quaternion.AngleAxis(15, Vector3.back));
                                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(firingSystem.projectileSpeed / 3f, firingSystem.projectileSpeed);
                            }
                        }
                        FindObjectOfType<AudioManager>().Play("PlayerShoot");
                    }
                    firingSystem.projectileFiringPeriodCounter = 0f;
                } else {
                    firingSystem.projectileFiringPeriodCounter += Time.deltaTime;
                }
            }
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            needToGo = false;
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
        Time.timeScale = 1f;
    }

    private void MoveToPoint() {
        if (needToGo) {
            transform.position = Vector2.MoveTowards(transform.position, myPoint.transform.position, 5f * Time.deltaTime);
        }
        if (transform.position == myPoint.transform.position) {
            needToGo = false;
            Time.timeScale = slow;
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
        foreach (FiringSystem firingSystem in firingSystems) {
            if (firingSystem.systemName == upgradeItem.ItemName) {
                firingSystem.isActive = true;
                firingSystem.projectilePrefab = upgradeItem.LaserPrefab;

                firingSystem.firePoints.Clear();
                foreach (string firePointName in upgradeItem.FirePointsNames) {
                    Transform newFirePoints = GameObject.Find(firePointName).transform;
                    if (newFirePoints.childCount > 0) {
                        foreach (Transform newFirePoint in newFirePoints) {
                            firingSystem.firePoints.Add(newFirePoint);
                        }
                    } else {
                        firingSystem.firePoints.Add(newFirePoints);
                    }
                }

                firingSystem.projectileSpeed = upgradeItem.ProjectileSpeed;
                firingSystem.projectileFiringPeriod = upgradeItem.ProjectileFiringPeriod;
            }
		}
	}
}