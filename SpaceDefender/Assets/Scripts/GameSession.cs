using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
	int score = 0;

	public ProgressBar progressBar;
	public int MaxScoreLevel = 3000;

    private void Start() {
        
    }

    private void Awake() {
        SetUpSingleton();
        progressBar.MaxScoreInLevel(MaxScoreLevel);
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
		progressBar.SetScore(this.score);
		if (this.score == MaxScoreLevel) {
			FindObjectOfType<Level>().LoadGameOver();
        }
	}

	public void ResetScore() {
		Destroy(gameObject);
	}
}
