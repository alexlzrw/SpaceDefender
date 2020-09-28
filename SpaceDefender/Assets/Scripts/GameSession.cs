using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
	int score = 0;


    public float timeToLevel = 100f;
    public float timeInSeconds = 0f;

    private void Start() {
        
    }

    private void Update() {
        TimeToLevelEnd();
    }

    public void TimeToLevelEnd() {
        timeInSeconds += Time.deltaTime;

        if (timeInSeconds >= timeToLevel) {
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    private void Awake() {
        SetUpSingleton();
	}

    private void SetUpSingleton() {
        int numberGameSessions = FindObjectsOfType(GetType()).Length;
        if (numberGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() {
        return score;	
	}


	public void AddScore(int score) {
		this.score += score;
	}

	public void ResetScore() {
		Destroy(gameObject);
	}
}
