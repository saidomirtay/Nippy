using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private TMP_Text highScoreOptionsText;
	private AudioManager audioManager;

	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

	public void PlayGame()
	{
		audioManager.Play("click");
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		audioManager.Play("click");
		Application.Quit();
	}

	private void Start()
	{
		GameManager.highScore = PlayerPrefs.GetInt("HighScore");
		highScoreOptionsText.text = "high score: " + GameManager.highScore;
		audioManager.Play("music");
	}
}
