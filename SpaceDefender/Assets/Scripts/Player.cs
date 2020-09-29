﻿using System;
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
    public GameObject laserPrefab;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = .15f;
    public float delayFiring = 1f;

    [Range(0f, 1f)]
    public float slow = 0.5f;





    private Vector3 offset;

    private void Start() {
        needToGo = true;
        Time.timeScale = slow;
    }

    private void Awake() {
        Fire();
    }

    
    void Update() {
        MoveToPoint();
    }

    private void Fire() {  
        StartCoroutine(FireContinuously());
        
    }

    IEnumerator FireContinuously()
    {
        yield return new WaitForSeconds(delayFiring);
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            FindObjectOfType<AudioManager>().Play("PlayerShoot");

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Time.timeScale = 1f;
        } 
        
    }

    private void OnMouseUp() {
        if (Input.GetMouseButtonUp(0)) {
            Time.timeScale = slow;
        }
    }


    void OnMouseDrag() {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }


    private void MoveToPoint() {
        if (needToGo) {
            transform.position = Vector2.MoveTowards(transform.position, myPoint.transform.position, 5f * Time.deltaTime);
            
        }

        if (Vector2.Distance(transform.position,myPoint.transform.position) < 0.01){
            needToGo = false;
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
}