using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    [SerializeField] private int parallelLasersLevel = 0;
    [SerializeField] private int diagonalLasersLevel = 0;
    [SerializeField] private int chargedLasersLevel = 0;

    public UpgradeItem[] parallelLasers;
    public UpgradeItem[] diagonalLasers;
    public UpgradeItem[] chargedLasers;

    public TextMeshProUGUI parallelLasersText;
    public TextMeshProUGUI diagonalLasersText;
    public TextMeshProUGUI chargedLasersText;

    public Color notEnoughScoreColor = Color.red;

    private Player player;
    private GameSession gameSession;

    private Color parallelLasersButtonColor;
    private Color diagonalLasersButtonColor;
    private Color chargedLasersButtonColor;

	private void Awake() {
        player = FindObjectOfType<Player>();
        gameSession = FindObjectOfType<GameSession>();
	}

	private void Start() {
        parallelLasersButtonColor = parallelLasersText.GetComponentInParent<Image>().color;
        diagonalLasersButtonColor = diagonalLasersText.GetComponentInParent<Image>().color;
        chargedLasersButtonColor = chargedLasersText.GetComponentInParent<Image>().color;
        ButtonAccessibility();
        UpdateUI();
	}

	private void Update() {
        ButtonAccessibility();
	}

	public void LevelUpParallelLasers() {
        if (!gameSession.Buy(parallelLasers[parallelLasersLevel].ScoreCost)) {
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
    }

    public void LevelUpDiagonalLasers() {
        if (!gameSession.Buy(diagonalLasers[diagonalLasersLevel].ScoreCost)) {
            return;
        }
        player.SetUpgrade(diagonalLasers[diagonalLasersLevel]);

        if (!IsUpgradeMaxLevel(diagonalLasersLevel, diagonalLasers.Length)) {
            diagonalLasersLevel++;
            diagonalLasersText.text = diagonalLasers[diagonalLasersLevel].ScoreCost.ToString();
        } else {
            diagonalLasersText.text = "MAX";
            diagonalLasersText.GetComponentInParent<Image>().color = Color.gray;
            diagonalLasersText.GetComponentInParent<Button>().enabled = false;
        }
    }

    public void LevelUpChargedLasers() {
		if (!gameSession.Buy(chargedLasers[chargedLasersLevel].ScoreCost)) {
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
	}

    private void ButtonAccessibility() {
        int score = gameSession.GetScore();
        if (parallelLasers[parallelLasersLevel].ScoreCost > score) {
            parallelLasersText.GetComponentInParent<Image>().color = notEnoughScoreColor;
        } else {
            parallelLasersText.GetComponentInParent<Image>().color = parallelLasersButtonColor;
        }

        if (diagonalLasers[diagonalLasersLevel].ScoreCost > score) {
            diagonalLasersText.GetComponentInParent<Image>().color = notEnoughScoreColor;
        } else {
            diagonalLasersText.GetComponentInParent<Image>().color = diagonalLasersButtonColor;
        }

        if (chargedLasers[chargedLasersLevel].ScoreCost > score) {
            chargedLasersText.GetComponentInParent<Image>().color = notEnoughScoreColor;
        } else {
            chargedLasersText.GetComponentInParent<Image>().color = parallelLasersButtonColor;
        }
    }

    private bool IsUpgradeMaxLevel(int currentLevel, int maxLevel) {
        return currentLevel >= maxLevel - 1;
	}

    private void UpdateUI() {
        parallelLasersText.text = parallelLasers[parallelLasersLevel].ScoreCost.ToString();
        diagonalLasersText.text = diagonalLasers[diagonalLasersLevel].ScoreCost.ToString();
        chargedLasersText.text = chargedLasers[chargedLasersLevel].ScoreCost.ToString();
    }
}
