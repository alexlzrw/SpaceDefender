using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    [SerializeField] private int parallelLasersLevel = 0;
    [SerializeField] private int diagonalLasersLevel = 0;
    [SerializeField] private int chargedLasersLevel = 0;
    [SerializeField] private int parallelChargedLasersLevel = 0;

    public UpgradeItem[] parallelLasers;
    public UpgradeItem[] diagonalLasers;
    public UpgradeItem[] chargedLasers;
    public UpgradeItem[] parallelChargedLasers;

    public TextMeshProUGUI parallelLasersText;
    public TextMeshProUGUI diagonalLasersText;
    public TextMeshProUGUI chargedLasersText;
    public TextMeshProUGUI parallelChargedLasersText;

    Player player;

	private void Awake() {
        player = FindObjectOfType<Player>();
	}

	private void Start() {
        UpdateUI();
	}

	public void LevelUpParallelLasers() {
        if (!FindObjectOfType<GameSession>().Buy(parallelLasers[parallelLasersLevel].ScoreCost)) {
            return;
		}
        player.SetUpgrade(parallelLasers[parallelLasersLevel]);

        if (!IsUpgradeMaxLevel(parallelLasersLevel, parallelLasers.Length)) {
            parallelLasersLevel++;
            parallelLasersText.text = parallelLasers[parallelLasersLevel].ScoreCost.ToString();
        } else {
            parallelLasersText.text = "MAX";
            parallelLasersText.GetComponentInParent<Image>().color = Color.gray;
            parallelLasersText.GetComponentInParent<Button>().enabled = false;
        }

        //UpdateUI();
    }

	public void LevelUpChargedLasers() {
		if (!FindObjectOfType<GameSession>().Buy(chargedLasers[chargedLasersLevel].ScoreCost)) {
			return;
		}
		player.SetUpgrade(chargedLasers[chargedLasersLevel]);

		if (!IsUpgradeMaxLevel(chargedLasersLevel, chargedLasers.Length)) {
			chargedLasersLevel++;
            chargedLasersText.text = chargedLasers[chargedLasersLevel].ScoreCost.ToString();
        } else {
            chargedLasersText.text = "MAX";
            chargedLasersText.GetComponentInParent<Image>().color = Color.gray;
            chargedLasersText.GetComponentInParent<Button>().enabled = false;
        }

		//UpdateUI();
	}

    public void LevelUpDiagonalLasers() {
        if (!FindObjectOfType<GameSession>().Buy(diagonalLasers[diagonalLasersLevel].ScoreCost)) {
            return;
        }
        player.SetUpgrade(diagonalLasers[diagonalLasersLevel]);

        if (!IsUpgradeMaxLevel(diagonalLasersLevel, diagonalLasers.Length)) {
            diagonalLasersLevel++;
            diagonalLasersText.text = diagonalLasers[diagonalLasersLevel].ScoreCost.ToString();
        }
        else {
            diagonalLasersText.text = "MAX";
            diagonalLasersText.GetComponentInParent<Image>().color = Color.gray;
            diagonalLasersText.GetComponentInParent<Button>().enabled = false;
        }

        //UpdateUI();
    }

    private bool IsUpgradeMaxLevel(int currentLevel, int maxLevel) {
        return currentLevel >= maxLevel - 1;
	}

    private void UpdateUI() {
        parallelLasersText.text = parallelLasers[parallelLasersLevel].ScoreCost.ToString();
        chargedLasersText.text = chargedLasers[chargedLasersLevel].ScoreCost.ToString();
        diagonalLasersText.text = diagonalLasers[diagonalLasersLevel].ScoreCost.ToString();
    }
}
