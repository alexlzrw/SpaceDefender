using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Slider slider;

    public void SetScore(int score) {
        slider.value = score;
    }

    public void MaxScoreInLevel(int score) {
        slider.value = score;
        slider.maxValue = score;
    }

   
}
