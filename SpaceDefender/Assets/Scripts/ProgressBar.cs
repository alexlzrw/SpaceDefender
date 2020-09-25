using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Slider slider;

    public void SetScore(float number) {
        slider.value = number;
    }

    public void MaxScoreInLevel(float number) {
        slider.value = number;
        slider.maxValue = number;
    }


   
}
