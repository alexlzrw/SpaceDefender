using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float padding = 1f;

    float xMin, xMax;
    float yMin, yMax;

    private void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
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
}