using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Item", menuName = "Upgrade Item", order = 51)]
public class UpgradeItem : ScriptableObject {
    [SerializeField] private string itemName;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int scoreCost;
    [SerializeField] private string[] firePointsNames;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = .15f;

    public string ItemName {
        get {
            return itemName;
		}
	}

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

    public string[] FirePointsNames {
        get {
            return firePointsNames;
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
