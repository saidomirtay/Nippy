using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject joystick;
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text highScoreText;
	private AudioManager audioManager;
	public static int highScore = 0;
	public int score;
	bool isGameEnded = false;

	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}
	public void EndGame()
	{
		if (!isGameEnded)
		{
			isGameEnded = true;
			joystick.SetActive(false);
			gameOverMenu.SetActive(true);
			SaveHighScore();
			audioManager.Play("game over");
		}
	}

	public void AddScore()
	{
		score++;
	}

	public void SaveHighScore()
	{
		if (score > highScore)
		{
			highScoreText.gameObject.SetActive(true);
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
			audioManager.Play("high score beat");
		}
	}

	public void Restart()
	{
		audioManager.Play("click");
		highScoreText.gameObject.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Home()
	{
		audioManager.Play("click");
		SceneManager.LoadScene(0);
	}

	private void Start()
	{
		highScoreText.gameObject.SetActive(false);
		joystick.SetActive(true);
		highScore = PlayerPrefs.GetInt("HighScore");
		score = 0;
		audioManager.Play("play button click");
	}

	private void Update()
	{
		scoreText.text = "score: " + score;
	}
}
