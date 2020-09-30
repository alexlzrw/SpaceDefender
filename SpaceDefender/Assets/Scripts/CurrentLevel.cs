using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentLevel : MonoBehaviour {

    TextMeshProUGUI currentLevel;
    GameSession gameSession;

    void Start() {
        currentLevel = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update() {
        currentLevel.text = gameSession.GetLevel().ToString();
    }
}
