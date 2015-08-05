using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText clockText;
	
	bool gameOver;
	bool finish;
	bool isPause;
	public GameObject startScreen;
	private bool restart;
	private int score;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		finish = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());


	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		clockText.text = "Time: " + Time.time;
		Pause ();

	



	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (0, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver || finish)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void Pause ()
	{	if (Input.GetKeyDown (KeyCode.Escape)) {
						isPause = !isPause;
						if (isPause && !gameOver) {
								Time.timeScale = 0.1f;
								startScreen.SetActive(true);
								gameOverText.text = "Paused";
						}

						if (!isPause && !gameOver) {
								Time.timeScale = 1;
								startScreen.SetActive(false);
								gameOverText.text = "";
						}
				} else {
						return;
				}
	
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{	
		new WaitForSeconds (2);
		gameOverText.text = "You Dead";
		gameOver = true;
	}

	public void Finish ()
	{	

		new WaitForSeconds (1);
		gameOverText.text = "Level Complete";
		gameOver = true;
	} 
			

}