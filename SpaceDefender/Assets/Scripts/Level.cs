using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	public float delayInSeconds = 2f;

    public void LoadGameOver() {
		StartCoroutine(WaitAndLoad());
	}

	public void LoadGameScene() {
		SceneManager.LoadScene("Game Scene");
		FindObjectOfType<GameSession>().ResetScore();
	}

	public void LoadStartMenu() {
		SceneManager.LoadScene("Start Menu");
	}

	public void QuitGame() {
		Application.Quit();
	}

	IEnumerator WaitAndLoad() {
		yield return new WaitForSeconds(delayInSeconds);
		SceneManager.LoadScene("Game Over");
	}
}
