using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Slider slider;
    GameSession gameSession;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();

        slider.maxValue = gameSession.timeToLevel;
    }

    private void Update() {
        slider.value = gameSession.timeInSeconds;
    }

}
