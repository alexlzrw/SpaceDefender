using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[CreateAssetMenu(fileName = "New Upgrade Item", menuName = "Upgrade Item", order = 51)]
[System.Serializable]
public class UpgradeItem {
    [SerializeField] private string itemName;
    [SerializeField] private int currentLevel;
    [SerializeField] private GameObject[] laserPrefab;
    [SerializeField] private int[] scoreCost;
    [SerializeField] private string[] firePointNames;
    [SerializeField] private float[] projectileSpeed;
    [SerializeField] private float[] projectileFiringPeriod;
    [SerializeField] private Color enoughScoreColor;
    [SerializeField] private Color notEnoughScoreColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public string ItemName {
        get {
            return itemName;
		}
	}

    public int CurrentLevel {
        get {
            return currentLevel;
		}
	}

    public int NextLevel {
        get {
            return currentLevel + 1;
		}
	}

    public GameObject NextLevelLaserPrefab {
        get {
            return laserPrefab[currentLevel + 1];
		}
	}

    public int MaxLevel {
        get {
            return scoreCost.Length;
		}
	}

    public string NextLevelFirePointNames {
        get {
            return firePointNames[currentLevel + 1];
		}
	}

    public float NextLevelProjectileSpeed {
        get {
            return projectileSpeed[currentLevel + 1];
		}
	}

    public float NextLevelProjectileFiringPeriod {
        get {
            return projectileFiringPeriod[currentLevel + 1];
		}
	}

    public Color EnoughScoreColor {
        get {
            return enoughScoreColor;
		}
	}

    public Color NotEnoughScoreColor {
        get {
            return notEnoughScoreColor;
		}
	}

    public Color InactiveColor {
        get {
            return inactiveColor;
		}
	}

    public Button UpgradeButton {
        get {
            return upgradeButton.GetComponent<Button>();
		}
	}

    public Image ButtonBackImage {
        get {
            return upgradeButton.GetComponent<Image>();
		}
	}

    public TextMeshProUGUI ButtonText {
        get {
            return buttonText;
		}
	}

    public int NextLevelScoreCost {
        get {
            return scoreCost[currentLevel + 1];
        }
    }

    public void IncrementCurrentLevel() {
        currentLevel++;
	}
}
