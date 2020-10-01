using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScoreDisplay : MonoBehaviour {

    TextMeshProUGUI bestScoreText;
    GameSession gameSession;

    void Start() {
        bestScoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update() {
        bestScoreText.text = gameSession.BestScore().ToString("000 000 000");
    }
}
