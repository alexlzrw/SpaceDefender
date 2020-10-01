using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FiringSystem {
    public bool isActive = false;
    public bool isDiagonal = false;
    public string systemName;
    public GameObject projectilePrefab;
    public List<Transform> firePoints;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = .15f;

    public float projectileFiringPeriodCounter = 0f;
}