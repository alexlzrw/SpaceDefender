﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        }

        UpdateUI();
    }

    //public void LevelUpChargedLasers() {
    //    if (!FindObjectOfType<GameSession>().Buy(chargedLasers[chargedLasersLevel].ScoreCost)) {
    //        return;
    //    }
    //    player.SetUpgrade(chargedLasers[chargedLasersLevel]);

    //    if (!IsUpgradeMaxLevel(chargedLasersLevel, chargedLasers.Length)) {
    //        chargedLasersLevel++;
    //    }

    //    UpdateUI();
    //}

    private bool IsUpgradeMaxLevel(int currentLevel, int maxLevel) {
        return currentLevel >= maxLevel - 1;
	}

    private void UpdateUI() {
        parallelLasersText.text = parallelLasers[parallelLasersLevel].ScoreCost.ToString();
        //chargedLasersText.text = chargedLasers[chargedLasersLevel].ScoreCost.ToString();
    }
}
