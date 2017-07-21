using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] Hazards;
	public Vector3 SpawnPosition;
	public int hazardCount;
	public float SpawnWait;
	public float WaveWait;
	public float PlayerWait;

	public Text ScoreText;
	public Text GameOverText;
	public GameObject restartButton;

	private bool gameOver;

	private int score=0;


	void Start () {
		gameOver = false;
		GameOverText.text = "";
		restartButton.SetActive (false);
		UpdateScore ();

		StartCoroutine (SpawnWave());
	}

	void Update (){
		
	}

	IEnumerator SpawnWave()
	{
		yield return new WaitForSeconds (PlayerWait);

		while (true) {
			for (int i=0 ; i < hazardCount; i++) {
				GameObject Hazard = Hazards[Random.Range(0, Hazards.Length)];
				Vector3 SpawnLocation = new Vector3 (Random.Range (-SpawnPosition.x, SpawnPosition.x), SpawnPosition.y, SpawnPosition.z);
				Quaternion SpawnRotation = Quaternion.identity;
				Instantiate (Hazard, SpawnLocation, SpawnRotation);
				yield return new WaitForSeconds (SpawnWait);
			}
			yield return new WaitForSeconds (WaveWait);

			if (gameOver) {
				restartButton.SetActive (true);
				break;
			}
		}
	}

	void UpdateScore()
	{
		ScoreText.text = "Score : " + score;
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;	
		UpdateScore ();
	}

	public void GameOver()
	{
		GameOverText.text = "GAME OVER!!!";
		gameOver = true;
	}

	public void restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void exit(){
		Application.Quit();
	}
}
