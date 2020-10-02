using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    [SerializeField] private int parallelLasersIndex = 0;
    [SerializeField] private int diagonalLasersIndex = 1;
    [SerializeField] private int chargedLasersIndex = 2;

    public UpgradeItem[] upgradeItems;

    private Player player;
    private GameSession gameSession;

	private void Awake() {
        player = FindObjectOfType<Player>();
        gameSession = FindObjectOfType<GameSession>();
	}

	private void Start() {
        UpdateUI();
	}

	private void Update() {
        UpdateUI();
	}

    public void LevelUpByIndex(int index) {
        if (!gameSession.Buy(upgradeItems[index].NextLevelScoreCost)) {
            return;
        }
        player.SetUpgrade(upgradeItems[index]);

        if (upgradeItems[index].CurrentLevel < upgradeItems[index].MaxLevel - 1) {
            upgradeItems[index].IncrementCurrentLevel();
        }

        UpdateUI();
    }

    private void UpdateUI() {
        foreach (UpgradeItem upgradeItem in upgradeItems) {
            if (upgradeItem.CurrentLevel < upgradeItem.MaxLevel - 1) {
                upgradeItem.ButtonText.text = upgradeItem.NextLevelScoreCost.ToString();

                if (upgradeItem.NextLevelScoreCost > gameSession.GetScore()) {
                    upgradeItem.ButtonBackImage.color = upgradeItem.NotEnoughScoreColor;
				} else {
                    upgradeItem.ButtonBackImage.color = upgradeItem.EnoughScoreColor;
				}
			} else {
                upgradeItem.ButtonText.text = "MAX";
                upgradeItem.ButtonBackImage.color = upgradeItem.InactiveColor;
                upgradeItem.UpgradeButton.enabled = false;
			}
		}
    }
}
