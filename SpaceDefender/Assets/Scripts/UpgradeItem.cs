using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Item", menuName = "Upgrade Item", order = 51)]
public class UpgradeItem : ScriptableObject {
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int scoreCost;
    [SerializeField] private string firePointsName;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = .15f;

    public GameObject LaserPrefab {
        get {
            return laserPrefab;
		}
	}

    public int ScoreCost {
        get {
            return scoreCost;
		}
	}

    public string FirePointsName {
        get {
            return firePointsName;
        }
	}

    public float ProjectileSpeed {
        get {
            return projectileSpeed;
		}
	}

    public float ProjectileFiringPeriod {
        get {
            return projectileFiringPeriod;
		}
	}
}
